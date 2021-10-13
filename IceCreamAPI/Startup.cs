using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using IceCreamAPI;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using Azure.Identity;

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


            //var credentials = new DefaultAzureCredential();


            // var azureServiceTokenProvider = new AzureServiceTokenProvider();
            // string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://vault.azure.net");
            // var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService cosmosDbService =  new CosmosDbService(client, databaseName, containerName); 
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            return cosmosDbService;
        }
    }
}