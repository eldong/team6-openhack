using Microsoft.Azure.Cosmos;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        public async Task<List<RatingInfo>> GetAllRatingsAsync(string userId)
        {
             QueryDefinition queryDefinition = new QueryDefinition("select * from ratings r where r.userId = @UserId").WithParameter("@UserId", userId);

            FeedIterator<RatingInfo> queryResultSetIterator = _container.GetItemQueryIterator<RatingInfo>(queryDefinition);

            List<RatingInfo> ratings = new List<RatingInfo>();

            while (queryResultSetIterator.HasMoreResults)
            {
                foreach (var r in await queryResultSetIterator.ReadNextAsync())
                {
                    ratings.Add(r);
                }
            }
            return ratings;
        }

        public async Task<RatingInfo> GetRatingInfoAsync(string ratingid)
        {
            try
            {
                ItemResponse<RatingInfo> response = await this._container.ReadItemAsync<RatingInfo>(ratingid, new PartitionKey(ratingid));
                return response.Resource;
            }
            catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            { 
                return null;
            }
        }

        public async Task<ItemResponse<RatingInfo>> WriteRatingAsync(RatingInfo rating)
        {
             var result = await _container.CreateItemAsync<RatingInfo>(rating, new PartitionKey(rating.Id));

            return result;
        }
    }

}