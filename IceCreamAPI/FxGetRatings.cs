using IceCreamAPI.Types;
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
            string userId = req.Query["userId"];
            if (String.IsNullOrEmpty(userId)) return new BadRequestObjectResult("InvalidInput");

           
            //read ratings from service
            var ratingService = new RatingService();
            var ratings = await ratingService.GetAllRatingsAsync(userId);


            if (ratings == null || ratings.Count == 0) return new NotFoundResult();

            return new OkObjectResult(ratings);
        }
    }
}
