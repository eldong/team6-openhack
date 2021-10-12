using System;
using IceCreamAPI.Types;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamAPI
{
    public class RatingService
    {

        public Container _cosmosClient;
        public RatingService()
        {
            
        }

        public void Connect()
        {
            _cosmosClient = new CosmosDbService().Connect();

        }

        // public bool WriteRatingAsync(RatingInfo rating)
        // {
        //     if(Connect())
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        // public RatingInfo GetRatingInfoAsync(string ratingid)
        // {
        //     if(!Connect())
        //     {
        //         throw new Exception();
        //     }

        //     return new RatingInfo
        //     {  
        //        UserId = "some user id",
        //        ProductId = "some product id"
        //     };
        // }

        public async Task<List<RatingInfo>> GetAllRatingsAsync(string userId)
        {
            QueryDefinition queryDefinition = new QueryDefinition("select * from ratings r where r.Userid = @UserId").WithParameter("@UserId", userId);

            FeedIterator<RatingInfo> queryResultSetIterator = _cosmosClient.GetItemQueryIterator<RatingInfo>(queryDefinition);

            List<RatingInfo> ratings = new List<RatingInfo>();

            //var ratings = new List<RatingInfo>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<RatingInfo> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (RatingInfo r in currentResultSet)
                {
                    ratings.Add(r);
                    // Console.WriteLine("\tRead {0}\n", r);
                }
            }

            return ratings;
    
        }
    }

}