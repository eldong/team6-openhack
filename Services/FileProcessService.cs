
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Models;

namespace Services
{
    public class FileProcessService : IFileProcessService
    {
        private readonly IFileBatchCosmosClient _cosmosDbService;
        private readonly IFileBatchContentClient _fileContentService;
        public FileProcessService(IFileBatchCosmosClient cosmosDbService, IFileBatchContentClient fileContentService)
        {
            _cosmosDbService = cosmosDbService;
            _fileContentService = fileContentService;
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

        public async Task<string> WriteFileContentAsync(FileContent file)
        {
            var result = await _fileContentService.WriteFileContentAsync(file);

            return result.Resource.BatchId;

        }
    }
}