
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Models;

namespace Services
{
    public class FileProcessService : IFileProcessService
    {
        private readonly IFileBatchCosmosClient _cosmosDbService;
        public FileProcessService(IFileBatchCosmosClient cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<ItemResponse<FileBatch>> AddFileToBatchAsync(string batchId, ProcessFile file)
        {
            var fileBatch = await _cosmosDbService.GetFileBatchAsync(batchId);

            fileBatch.Files.Add(file);

            var updated = await _cosmosDbService.UpsertFileBatchAsync(fileBatch);

            return updated;   
        }

        public async Task<FileBatch> GetFileBatchAsync(string batchid)
        {
            var result = await _cosmosDbService.GetFileBatchAsync(batchid);

            return result;
        }

        public async Task<ItemResponse<FileBatch>> WriteFileBatchAsync(FileBatch batch)
        {
             var result = await _cosmosDbService.WriteFileBatchAsync(batch);

            return result;
        }
    }
}