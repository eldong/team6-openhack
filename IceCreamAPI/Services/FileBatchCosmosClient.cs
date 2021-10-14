using Microsoft.Azure.Cosmos;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public class FileBatchCosmosClient : IFileBatchCosmosClient
    {
        private Container _container;

        public FileBatchCosmosClient(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        // public async Task<List<RatingInfo>> GetAllRatingsAsync(string userId)
        // {
        //      QueryDefinition queryDefinition = new QueryDefinition("select * from ratings r where r.userId = @UserId").WithParameter("@UserId", userId);

        //     FeedIterator<RatingInfo> queryResultSetIterator = _container.GetItemQueryIterator<RatingInfo>(queryDefinition);

        //     List<RatingInfo> ratings = new List<RatingInfo>();

        //     while (queryResultSetIterator.HasMoreResults)
        //     {
        //         foreach (var r in await queryResultSetIterator.ReadNextAsync())
        //         {
        //             ratings.Add(r);
        //         }
        //     }
        //     return ratings;
        // }

        public async Task<FileBatch> GetFileBatchAsync(string batchid)
        {
            try
            {
                ItemResponse<FileBatch> response = await this._container.ReadItemAsync<FileBatch>(batchid, new PartitionKey(batchid));
                return response.Resource;
            }
            catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            { 
                return null;
            }
        }

        public async Task<ItemResponse<FileBatch>> UpsertFileBatchAsync(FileBatch batch)
        {
           var updated = await _container.UpsertItemAsync<FileBatch>(batch, new PartitionKey(batch.BatchId));

           return updated;
        }

        public async Task<ItemResponse<FileBatch>> WriteFileBatchAsync(FileBatch batch)
        {
             var result = await _container.CreateItemAsync<FileBatch>(batch, new PartitionKey(batch.BatchId));

            return result;
        }
    }

}