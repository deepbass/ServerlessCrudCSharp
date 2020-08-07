using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerlessCrudCSharp.Backend.Models;
using ServerlessCrudCSharp.Common;
using System.Web.Http;

namespace ServerlessCrudCSharp.Backend
{
    public static class GetWidget
    {
        [FunctionName("GetWidget")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "widgets/{id}")] HttpRequest req,
            [Table("widgets", "{id}", "{id}", Connection = "AzureTableStorage")] WidgetEntity widgetEntity,
            ILogger log,
            string id)
        {
            if(widgetEntity != null)
            {
                return new OkObjectResult(new Widget()
                {
                    WidgetId = widgetEntity.WidgetId,
                    Name = widgetEntity.Name,
                    Colour = widgetEntity.Colour,
                    Quantity = widgetEntity.Quantity
                });
            }

            return new BadRequestErrorMessageResult("Bad Request");
        }
    }
}
