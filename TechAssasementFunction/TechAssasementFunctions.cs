using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechAssasementFunction.Helpers;

namespace TechAssasementFunction
{
    public class TechAssasementFunctions
    {
        private readonly string _publicApisUrl;

        public TechAssasementFunctions()
        {
            _publicApisUrl = Environment.GetEnvironmentVariable("PublicApi");
        }

        private static readonly HttpClient _httpClient = new HttpClient();

        [FunctionName("FetchData")]
        public async Task Run([TimerTrigger("* */1 * * *")] TimerInfo myTimer, ILogger log)
        {
            var result = await _httpClient.GetAsync(new Uri(_publicApisUrl));
            var key = Guid.NewGuid().ToString();

            await TableStorageHelper.InsertToTable(result.StatusCode, key);

            if (result.IsSuccessStatusCode)
            {
                var payload = await result.Content.ReadAsStringAsync();
                BlobStorageHelper.SavePayload(payload, blobName: key);
            }
        }


        [FunctionName("GetLogs")]
        public static async Task<IActionResult> GetLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "logs")] HttpRequest req, ILogger log)
        {
            var logs = await TableStorageHelper.GetLogs(req.Query["fromDate"], req.Query["toDate"]);
            return new OkObjectResult(logs);
        }

        [FunctionName("GetPayload")]
        public static async Task<string> GetPayload([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "logs/payload/{id}")] HttpRequest req, string id, ILogger log)
        {
            return await BlobStorageHelper.GetPayload(id);
        }
    }
}