using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CrossUtils;
using Models;

namespace InternalBusinessUsersBackend
{
    public static class FxOrdersProcessorHttp
    {
        [FunctionName("FxOrdersProcessorHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {           

            string name = req.Query["name"];
            var batchId = OrdersProcessing.BatchId(name);
            FileBatch fileBatch=BatchService.GetBatchAsync(batchId);
            if (fileBatch.Files.Count == 3)
            {
                GetMergedJsonData(fileBatch);
                //GET THE JSON
                //HAVE THE JSON INSERTED IN COSMOS
                //Delete the batch
                return new OkObjectResult(batchId);
            }
            else return new AcceptedResult();
        }

        private static void GetMergedJsonData(FileBatch fileBatch)
        {
            foreach(var file in fileBatch.Files)
            {

            }
        }
    }
}
