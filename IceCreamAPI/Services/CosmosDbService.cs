using System;
using IceCreamAPI.Types;
using System.Collections.Generic;


namespace IceCreamAPI
{
    public class CosmosDbService
    {
        public CosmosDbService()
        {
            
        }

        public bool Connect()
        {
            return true;
        }

        public bool WriteRatingAsync(RatingInfo rating)
        {
            if(Connect())
            {
                return true;
            }
            return false;
        }

        public RatingInfo GetRatingInfoAsync(string ratingId)
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

        public List<RatingInfo> GetAllRatingAsync()
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

            ratings.Add(rating1);

            return ratings;
        }
    }

}