using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerlessCrudCSharp.Common;
using Microsoft.WindowsAzure.Storage.Table;
using ServerlessCrudCSharp.Backend.Models;
using System.Web.Http;

namespace ServerlessCrudCSharp.Backend
{
    public static class PutWidget
    {
        [FunctionName("PutWidget")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "widgets/{id}")] Widget widget,
            [Table("widgets", Connection = "AzureTableStorage")] CloudTable cloudTable,
            ILogger log,
            string id)
        {
            widget.WidgetId = id;
            var widgetEntity = new WidgetEntity()
            {
                RowKey = widget.WidgetId,
                PartitionKey = widget.WidgetId,
                WidgetId = widget.WidgetId,
                Colour = widget.Colour,
                Name = widget.Name,
                Quantity = widget.Quantity
            };
            TableOperation replaceOperation = TableOperation.InsertOrReplace(widgetEntity);
            var result = await cloudTable.ExecuteAsync(replaceOperation);
            if (result.HttpStatusCode >= 400)
            {
                return new BadRequestErrorMessageResult("Bad request");
            }
            return new OkObjectResult(widget.WidgetId);
        }
    }
}
