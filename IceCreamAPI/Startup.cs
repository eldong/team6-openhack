using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using IceCreamAPI;


[assembly: FunctionsStartup(typeof(IceCreamAPI.Startup))]
namespace IceCreamAPI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync().GetAwaiter().GetResult());
            builder.Services.AddSingleton<IRatingService,RatingService>();
        }

        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync()
        {
            string databaseName = System.Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME");
            string containerName = System.Environment.GetEnvironmentVariable("COSMOS_DB_CONTAINER_NAME");
            string account = System.Environment.GetEnvironmentVariable("COSMOS_DB_URL");
            string key = System.Environment.GetEnvironmentVariable("COSMOS_DB_ACCOUNT_KEY");
            
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService cosmosDbService =  new CosmosDbService(client, databaseName, containerName); 
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/userid");
            return cosmosDbService;
        }
    }
}