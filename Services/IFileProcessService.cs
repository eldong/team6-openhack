using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Models;
namespace Services
{
    public interface IFileProcessService
    {
        Task<FileBatch> GetFileBatchAsync(string batchid);
         Task<ItemResponse<FileBatch>> WriteFileBatchAsync(FileBatch batch);
         Task<ItemResponse<FileBatch>> AddFileToBatchAsync(string batchId, ProcessFile file);
         Task<string> WriteFileContentAsync(FileContent file);
    }
}
