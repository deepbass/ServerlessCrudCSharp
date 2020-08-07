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
using ServerlessCrudCSharp.Common;
using ServerlessCrudCSharp.Backend.Models;
using System.Web.Http;

namespace ServerlessCrudCSharp.Backend
{
    public static class PostWidget
    {
        [FunctionName("PostWidget")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "widgets")] Widget widget,
            [Table("widgets", Connection = "AzureTableStorage")] CloudTable cloudTable,
            ILogger log)
        {
            widget.WidgetId = Guid.NewGuid().ToString();
            var widgetEntity = new WidgetEntity()
            {
                RowKey = widget.WidgetId,
                PartitionKey = widget.WidgetId,
                WidgetId = widget.WidgetId,
                Colour = widget.Colour,
                Name = widget.Name,
                Quantity = widget.Quantity
            };
            TableOperation insertOrMergeOperation = TableOperation.InsertOrReplace(widgetEntity);
            try
            {
                var result = await cloudTable.ExecuteAsync(insertOrMergeOperation);
                if(result.HttpStatusCode >= 400)
                {
                    return new BadRequestErrorMessageResult("Bad request");
                }
            } catch(Exception e)
            {
                log.LogInformation(JsonConvert.SerializeObject(e));
                return new BadRequestErrorMessageResult("Bad request");
            }

            return new OkObjectResult(widget.WidgetId);
        }
    }
}
