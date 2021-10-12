using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IceCreamAPI
{
    public static class FxGetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Query["ratingId"];
            if (String.IsNullOrEmpty(name)) return new BadRequestObjectResult("InvalidInput");

            var rating = new RatingInfo()
            {
                Id = "adssad",
                LocationName = "asdsadsa",
                ProductId = "dsada",
                Rating = 5,
                TimeStamp = DateTime.UtcNow,
                UserId = ";aklsjdlkjsadlkjsa",
                UserNotes = "I like it"
            };


            //read rating from service

            if (rating == null) return new NotFoundResult();

            return new OkObjectResult(rating);
        }
    }
}
