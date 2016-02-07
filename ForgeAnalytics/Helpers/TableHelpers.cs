using ForgeAnalytics.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ForgeAnalytics.Helpers
{
    public class TableHelpers
    {
        private static TableHelpers instance;

        public static TableHelpers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableHelpers();
                }
                return instance;
            }
        }

        public CloudTableClient client;

        private TableHelpers()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("forgeanalytics_AzureStorageConnectionString"));

            // Create the table client.
            client = storageAccount.CreateCloudTableClient();
        }

        public async Task SaveData(AnalyticsModel model)
        {
            var table = client.GetTableReference(model.Table);
            await table.CreateIfNotExistsAsync();

            DynamicTableEntity dte = new DynamicTableEntity();
            dte.RowKey = Guid.NewGuid().ToString();
            dte.PartitionKey = model.PartitionKey;

            Dictionary<string, EntityProperty> properties = new Dictionary<string, EntityProperty>();
            properties.Add("ClientDateTimeEpoch", new EntityProperty(Utils.epochTime(DateTime.Now)));

            foreach(string key in model.Properties.Keys)
            {
                properties.Add(key, new EntityProperty(model.Properties[key]));
            }

            dte.Properties = properties;
            var insertOperation = TableOperation.Insert(dte);
            await table.ExecuteAsync(insertOperation);
        }
    }
}