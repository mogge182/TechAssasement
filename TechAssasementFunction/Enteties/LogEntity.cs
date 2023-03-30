using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace TechAssasementFunction.Enteties
{
    public class LogEntity : TableEntity
    {
        public int StatusCode { get; set; }

        public LogEntity() { }

        public LogEntity(string rowKey)
        {
            this.PartitionKey = DateTime.Now.ToString("yyyyMMdd");
            this.RowKey = rowKey;
        }
    }
}
