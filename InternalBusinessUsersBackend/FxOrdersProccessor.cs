// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using CrossUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternalBusinessUsersBackend
{
    public class OrdersProccessor
    {

        private readonly IFileProcessService _fileProcessService;

        public OrdersProccessor(IFileProcessService fileProcessService)
        {
            _fileProcessService = fileProcessService;
        }

        [FunctionName("OrdersProccessor")]
        public async Task<IActionResult> Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {

            log.LogInformation(eventGridEvent.Data.ToString());
            var storageUrl = JsonConvert.DeserializeObject<InsertedFile>(eventGridEvent.Data.ToString()).Url;
            var name = OrdersProcessing.FileNameFromUrl(storageUrl);
            var batchId = OrdersProcessing.BatchIdFromFileName(name);
            var fileType = OrdersProcessing.FileTypeFromFileName(name);
            FileBatch fileBatch = await _fileProcessService.GetFileBatchAsync(batchId);

            //It is a new batch
            if (fileBatch == null)
            {
                await CreateNewBatch(storageUrl, name, batchId, fileType);
                return new AcceptedResult();
            }

            //It is a completed batch
            if (fileBatch.Files.Count == 3)
            {
                var mergedJson = await GetMergedJsonData(fileBatch);

                //Insert JSON in Cosmos
                //_fileProcessService.InsertJson(mergedJson);

                //Delete the batch
                //_fileProcessService.DeleteBatch(batchId);

                return new OkObjectResult(mergedJson);
            }


            return new AcceptedResult();
        }

        private async Task CreateNewBatch(string storageUrl, string name, string batchId, string fileType)
        {
            FileBatch fileBatch = new FileBatch()
            {
                BatchId = batchId,
                Files = new List<ProcessFile>() {
                        new ProcessFile() {
                            StorageUrl=storageUrl,
                        FileName=name,
                        Type = fileType
                        }
                    }
            };
            await _fileProcessService.WriteFileBatchAsync(fileBatch);
        }




        /// <summary>
        /// Sends the file routes and received the merged JSON
        /// </summary>
        /// <param name="fileBatch"></param>
        /// <returns></returns>
        private static async Task<string> GetMergedJsonData(FileBatch fileBatch)
        {

            var orderMap = new OrderMap();
            foreach (var file in fileBatch.Files)
            {
                switch (file.Type)
                {
                    case "OrderHeaderDetails":
                        orderMap.OrderHeaderDetailsCSVUrl = file.StorageUrl;
                        break;
                    case "OrderLineItems":
                        orderMap.OrderLineItemsCSVUrl = file.StorageUrl;
                        break;
                    case "ProductInformation":
                        orderMap.ProductInformationCSVUrl = file.StorageUrl;
                        break;
                }
            }

            var combineOrderEndpoint = Environment.GetEnvironmentVariable("COMBINE_ORDER_ENDPOINT");
            var client = new HttpClient();
            //var content = new StringContent(JsonConvert.SerializeObject(orderMap), Encoding.UTF8, "application/json");
            var result = await client.PostAsJsonAsync(combineOrderEndpoint, orderMap);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
