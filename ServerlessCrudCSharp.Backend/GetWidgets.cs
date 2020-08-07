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
using System.Collections.Generic;
using ServerlessCrudCSharp.Common;

namespace ServerlessCrudCSharp.Backend
{
    public static class GetWidgets
    {
        [FunctionName("GetWidgets")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "widgets")] HttpRequest req,
            [Table("widgets", Connection = "AzureTableStorage")] CloudTable cloudTable,
            ILogger log)
        {
            TableContinuationToken token = null;
            var widgets = new List<Widget>();
            do
            {
                var queryResult = await cloudTable.ExecuteQuerySegmentedAsync(new TableQuery<WidgetEntity>(), token);
                foreach(WidgetEntity widgetEntity in queryResult.Results)
                {
                    widgets.Add(new Widget()
                    {
                        WidgetId = widgetEntity.WidgetId,
                        Name = widgetEntity.Name,
                        Colour = widgetEntity.Colour,
                        Quantity = widgetEntity.Quantity
                    });
                }
                token = queryResult.ContinuationToken;
            } while (token != null);
            return new OkObjectResult(widgets);
        }
    }
}
