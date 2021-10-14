using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IFileBatchContentClient
    {
        //Task<List<>> GetAllRatingsAsync(string userId);
        //Task<FileBatch> GetFileBatchAsync(string batchid);
        Task<ItemResponse<FileContent>> WriteFileContentAsync(FileContent file);
       // Task<ItemResponse<FileBatch>> UpsertFileBatchAsync(FileBatch batch);
    }
}