using Microsoft.Azure.Cosmos;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public class RatingService : IRatingService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public RatingService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<RatingInfo> GetRatingInfoAsync(string ratingId)
        {
            var result = await _cosmosDbService.GetRatingInfoAsync(ratingId);
            return result;
        }

        public async Task<List<RatingInfo>> GetAllRatingsAsync(string userId)
        {
            var results = await _cosmosDbService.GetAllRatingsAsync(userId);         
            return results;
        }

        public async Task<ItemResponse<RatingInfo>> WriteRatingAsync(RatingInfo rating)
        {
            var result = await _cosmosDbService.WriteRatingAsync(rating);

            return result;
        }
    }
}