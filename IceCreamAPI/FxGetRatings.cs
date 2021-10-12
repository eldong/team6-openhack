using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public static class FxGetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Query["userId"];
            if (String.IsNullOrEmpty(name)) return new BadRequestObjectResult("InvalidInput");

            var ratings = new List<RatingInfo>()
            {
                new RatingInfo()
                {
                Id = "adssad",
                LocationName = "asdsadsa",
                ProductId = "dsada",
                Rating = 5,
                TimeStamp = DateTime.UtcNow,
                UserId = ";aklsjdlkjsadlkjsa",
                UserNotes = "I like it"
                },
                new RatingInfo()
                {
                Id = "asdfadssad",
                LocationName = "addddddddsdsadsa",
                ProductId = "11111dsada",
                Rating = 1,
                TimeStamp = DateTime.UtcNow,
                UserId = ";klsjdlkjsadlkjsa",
                UserNotes = "I disliked it"
                }
            };


            //read ratings from service

            if (ratings == null) return new NotFoundResult();

            return new OkObjectResult(ratings);
        }
    }
}
