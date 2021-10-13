using System;
using IceCreamAPI.Types;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IceCreamAPI
{
    public interface IRatingService
    {
        //void Connect();
        Task<List<RatingInfo>> GetAllRatingsAsync(string userId);
        Task<RatingInfo> GetRatingInfoAsync(string ratingid);
        Task<ItemResponse<RatingInfo>> WriteRatingAsync(RatingInfo rating);
    }
}