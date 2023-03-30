using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TechAssasementFunction.Helpers
{
    public static class BlobStorageHelper
    {
        private static readonly string StorageConnection;

        static BlobStorageHelper()
        {
            StorageConnection = Environment.GetEnvironmentVariable("ConnectionStrings:Blobstorage");
        }

        public static async void SavePayload(string payload, string blobName)
        {
            var blobClient = await GetBlobClient(blobName);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(payload));
            await blobClient.UploadAsync(stream);
        }

        public static async Task<string> GetPayload(string blob)
        {
            var blobClient = await GetBlobClient(blob);

            var response = await blobClient.DownloadAsync();
            using (var ms = new MemoryStream())
            {
                await response.Value.Content.CopyToAsync(ms);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private static async Task<BlobClient> GetBlobClient(string blob)
        {
            var blobServiceClient = new BlobServiceClient(StorageConnection);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("payloads");

            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = blobContainerClient.GetBlobClient(blob);

            return blobClient;
        }
    }
}
