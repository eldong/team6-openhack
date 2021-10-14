using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Services;

public class FileBatchContentClient : IFileBatchContentClient
{
        private Container _container;

        public FileBatchContentClient(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }
        public async Task<ItemResponse<FileContent>> WriteFileContentAsync(FileContent file)
        {
            var result = await _container.CreateItemAsync<FileContent>(file, new PartitionKey(file.BatchId));

            return result;
        }
}