using System;
using IceCreamAPI.Types;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace IceCreamAPI
{
    public class CosmosDbService
    {
        private Container _container;
        public CosmosDbService()
        {
            
        }

        public Container Connect()
        {
            string databaseName = "products";
            string containerName = "indertest";
            string account = "https://team6-cosmos-db.documents.azure.com:443";
            string key = "soMSFBnsNV5anEdm11Z8NxJahlvTryHnnprRYIvvCjY9rOUVp0FSzdPlOYgkjLysupMxVAXcw1h4FCZHG90NlA==";
            
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            _container =  client.GetContainer(databaseName, containerName); 
            // Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            // await database.Database.CreateContainerIfNotExistsAsync(containerName, "/subscriptionName");

            // return this;

            return this._container;
        }
    }

}