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
    public Task<ItemResponse<FileContent>> WriteFileContentAsync(FileContent file)
    {
        throw new System.NotImplementedException();
    }
}