using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public class FxGetRatings
    {
        private readonly IRatingService _ratingService;

        public FxGetRatings(IRatingService ratingService)
        {
            _ratingService = ratingService;      
        }     

        [FunctionName("GetRatings")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.Query["userId"];
            if (String.IsNullOrEmpty(userId)) return new BadRequestObjectResult("InvalidInput");
 
            var ratings = await _ratingService.GetAllRatingsAsync(userId);

            if (ratings == null || ratings.Count == 0) return new NotFoundResult();

            return new OkObjectResult(ratings);
        }
    }
}
