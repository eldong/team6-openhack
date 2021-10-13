using System;
using IceCreamAPI.Types;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public interface ICosmosDbService
    {
        Task<List<RatingInfo>> GetAllRatingsAsync(string userId);
        Task<RatingInfo> GetRatingInfoAsync(string ratingid);
        Task<ItemResponse<RatingInfo>> WriteRatingAsync(RatingInfo rating);
    }
}