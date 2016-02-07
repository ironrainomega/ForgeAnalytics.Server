using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForgeAnalytics.Models
{
    public class AnalyticsModel
    {
        public string PartitionKey { get; set; }

        public long? ClientDateTimeEpoch { get; set; }

        public string Table { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}