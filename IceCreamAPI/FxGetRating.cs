using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public class FxGetRating
    {
        private readonly IRatingService _ratingService;

        public FxGetRating(IRatingService ratingService)
        {
            _ratingService = ratingService;      
        }   

        [FunctionName("GetRating")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string ratingId = req.Query["ratingId"];
            if (String.IsNullOrEmpty(ratingId)) return new BadRequestObjectResult("InvalidInput");

            //read rating from service

            var rating = await _ratingService.GetRatingInfoAsync(ratingId);
            if (rating == null) return new NotFoundResult();

            return new OkObjectResult(rating);
        }
    }
}
