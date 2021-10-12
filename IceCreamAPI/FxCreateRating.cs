using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using IceCreamAPI.Types;

namespace IceCreamAPI
{
    public static class FxCreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");          

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();


            CreateRatingPayload data;

            try
            {
                data = JsonConvert.DeserializeObject<CreateRatingPayload>(requestBody);
            }
            catch
            {
                return new BadRequestObjectResult("Invalid Input");
            }


            //Validate userId
            var userId = data.UserId;
            var wc = new WebClient();

            Customer customer;
            try
            {
                var getUserResponse = wc.DownloadString($"https://serverlessohapi.azurewebsites.net/api/GetUser?userId={userId}");
                customer = JsonConvert.DeserializeObject<Customer>(getUserResponse);
            }
            catch
            {
                return new NotFoundObjectResult("Customer not found");
            }



            //Validate productId
            var productId = data.ProductId;

            Product product;
            try
            {
                var getProductResponse = wc.DownloadString($"https://serverlessohapi.azurewebsites.net/api/GetProduct?productId={productId}");
                product = JsonConvert.DeserializeObject<Product>(getProductResponse);
            }
            catch
            {
                return new NotFoundObjectResult("Product not found");
            }


            //Validating the rating value
            if (data.Rating < 0 || data.Rating > 5) return new BadRequestObjectResult("Invalid Rating");


        
            //Creating the rating instance
            var rating = new RatingInfo()
            {                
                Id = Guid.NewGuid().ToString(),
                UserId = customer.UserId,
                ProductId = product.ProductId,
                Rating = data.Rating,
                TimeStamp = DateTime.UtcNow,
                LocationName=data.LocationName,
                UserNotes=data.UserNotes
            };


            //Inserting the rating Instance in the DB
            var ratingService = new RatingService();
            var insertionOk = ratingService.WriteRatingAsync(rating);
            if (!insertionOk) return new StatusCodeResult(510);

            return new OkObjectResult(rating);
        }
    }
}
