using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeighborhoodHelpers.UserMicroservice.Entities.DatabaseContext
{
    public interface IUserDbContext
    {
        Task CreateTable(string tableName, string hashKey);

        void InsertItem<T>(T currentState);

        Table GetAllItems(string tableName);

        Task<T> GetItemById<T>(Guid Id);

        Task<List<T>> GetItemByString<T>(string key, string value);
    }
}
