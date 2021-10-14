using Microsoft.Azure.Cosmos;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IRatingService
    {
        //void Connect();
        Task<List<RatingInfo>> GetAllRatingsAsync(string userId);
        Task<RatingInfo> GetRatingInfoAsync(string ratingid);
        Task<ItemResponse<RatingInfo>> WriteRatingAsync(RatingInfo rating);
    }
}