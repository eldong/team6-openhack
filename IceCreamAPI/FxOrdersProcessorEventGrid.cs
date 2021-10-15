// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Services;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using CrossUtils;

namespace IceCreamAPI
{
    public  class FxOrdersProcessorEventGrid
    {
        private readonly IFileProcessService _fileProcessService;

        public FxOrdersProcessorEventGrid(IFileProcessService fileProcessService)
        {
            _fileProcessService = fileProcessService;
        }

        [FunctionName("FxOrdersProcessorEventGrid")]
        public  async Task<IActionResult> Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            
            dynamic data = eventGridEvent.Data;
            var storageUrl = data.url;
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
                var responseFile = new FileContent
                {
                    Content = mergedJson,
                    BatchId = fileBatch.BatchId
                };

                //Insert JSON in Cosmos
                var result = await _fileProcessService.WriteFileContentAsync(responseFile);

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
            foreach(var file in fileBatch.Files)
            {
                switch(file.Type)
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
