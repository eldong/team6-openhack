using System;
using IceCreamAPI.Types;
using System.Collections.Generic;


namespace IceCreamAPI
{
    public class RatingService
    {
        public RatingService()
        {
            
        }

        public bool Connect()
        {
            var cosmosService = new CosmosDbService();

            return cosmosService.Connect();
        }

        public bool WriteRatingAsync(RatingInfo rating)
        {
            if(Connect())
            {
                return true;
            }
            return false;
        }

        public RatingInfo GetRatingInfoAsync(string ratingid)
        {
            if(!Connect())
            {
                throw new Exception();
            }

            return new RatingInfo
            {  
               UserId = "some user id",
               ProductId = "some product id"
            };
        }

        public List<RatingInfo> GetAllRatingsAsync(string userId)
        {
            if(!Connect())
            {
                throw new Exception();
            }

            var ratings = new List<RatingInfo>();

            var rating1 = new RatingInfo 
            {
               UserId = "some user id",
               ProductId = "some product id"

            };

            if(userId == "p1")
            {
              ratings.Add(rating1);
            }
            else
            {
                return new List<RatingInfo>();
            }

            return ratings;
        }
    }

}