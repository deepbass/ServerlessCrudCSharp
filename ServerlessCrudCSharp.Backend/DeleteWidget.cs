using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using ServerlessCrudCSharp.Backend.Models;
using ServerlessCrudCSharp.Common;

namespace ServerlessCrudCSharp.Backend
{
    public static class DeleteWidget
    {
        [FunctionName("DeleteWidget")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "widgets/{id}")] HttpRequest req,
            [Table("widgets", Connection = "AzureTableStorage")] CloudTable cloudTable,
            ILogger log,
            string id)
        {
            TableOperation getEntity = TableOperation.Retrieve<WidgetEntity>(id, id);
            var response = await cloudTable.ExecuteAsync(getEntity);
            var result = response.Result as WidgetEntity;
            TableOperation deleteEntity = TableOperation.Delete(result);
            await cloudTable.ExecuteAsync(deleteEntity);

            return new OkResult();
        }
    }
}
