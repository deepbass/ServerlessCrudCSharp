using Microsoft.WindowsAzure.Storage.Table;

namespace ServerlessCrudCSharp.Backend.Models
{
    public class WidgetEntity : TableEntity
    {
        public WidgetEntity()
        {

        }

        public WidgetEntity(string id)
        {
            RowKey = id;
            PartitionKey = id;
        }

        public string WidgetId { get; set; }
        public string Colour { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
    }
}
