using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechAssasementFunction.Enteties;

namespace TechAssasementFunction.Helpers
{
    public static class TableStorageHelper
    {
        private static readonly string StorageConnection;

        static TableStorageHelper()
        {
            StorageConnection = Environment.GetEnvironmentVariable("ConnectionStrings:Blobstorage");
        }

        public static async Task InsertToTable(HttpStatusCode statusCode, string key)
        {
            var table = await GetCloudTable();

            var logEntity = new LogEntity(rowKey: key)
            {
                StatusCode = (int)statusCode
            };

            var insertOperation = TableOperation.Insert(logEntity);
            await table.ExecuteAsync(insertOperation);
        }

        public static async Task<IEnumerable<LogEntity>> GetLogs(string fromDate, string toDate)
        {
            var table = await GetCloudTable();

            var query = new TableQuery<LogEntity>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, DateTime.Parse(fromDate)),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, DateTime.Parse(toDate))
            ));

            var entities = new List<LogEntity>();
            TableContinuationToken token = null;
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(query, token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);
            return entities;
        }

        private static async Task<CloudTable> GetCloudTable()
        {
            var storageAccount = CloudStorageAccount.Parse(StorageConnection);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("techassasementtable");

            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}
