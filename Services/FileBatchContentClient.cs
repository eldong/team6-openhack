using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Services;

public class FileBatchContentClient : IFileBatchContentClient
{
    public Task<ItemResponse<FileContent>> WriteFileContentAsync(FileContent file)
    {
        throw new System.NotImplementedException();
    }
}