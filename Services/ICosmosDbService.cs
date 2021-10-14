using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface ICosmosDbService
    {
        Task<List<RatingInfo>> GetAllRatingsAsync(string userId);
        Task<RatingInfo> GetRatingInfoAsync(string ratingid);
        Task<ItemResponse<RatingInfo>> WriteRatingAsync(RatingInfo rating);
    }
}