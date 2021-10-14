using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IFileBatchCosmosClient
    {
        //Task<List<>> GetAllRatingsAsync(string userId);
        Task<FileBatch> GetFileBatchAsync(string batchid);
        Task<ItemResponse<FileBatch>> WriteFileBatchAsync(FileBatch batch);
        Task<ItemResponse<FileBatch>> UpsertFileBatchAsync(FileBatch batch);
    }
}