using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace InternalBusinessUsersBackend
{
    public static class FxAssembleEmailMessage
    {
        [FunctionName("AssembleEmailMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var mobileSubsKey = System.Environment.GetEnvironmentVariable("MOBILE_SUBS_KEY");
            var getProductsApiEndpoint = System.Environment.GetEnvironmentVariable("GET_PRODUCTS_API_ENDPOINT");

            var wc = new WebClient();
            wc.Headers.Add("Ocp-Apim-Subscription-Key", mobileSubsKey);

            //Getting the products
            List<Product> products;
            var getProductResponse = wc.DownloadString(getProductsApiEndpoint);           
            products = JsonConvert.DeserializeObject<List<Product>>(getProductResponse);


            //Injecting the products in the template
            var mailText=TemplateUtils.InsertProductsInTemplate(products);
            
            return new OkObjectResult(mailText);
        }
    }
}
