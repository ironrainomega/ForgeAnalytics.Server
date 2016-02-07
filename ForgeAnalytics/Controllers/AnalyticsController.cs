using ForgeAnalytics.Helpers;
using ForgeAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ForgeAnalytics.Controllers
{
    public class AnalyticsController : ApiController
    {
        public AnalyticsModel Get()
        {
            return new AnalyticsModel() {
                ClientDateTimeEpoch = Utils.epochTime(DateTime.Now),
                PartitionKey = "a1b2c3",
                Properties = new Dictionary<string, string>()
                {
                    {"prop1", "value1" },
                    {"prop2", "value2" }
                }
            };
        }

        // POST: api/Analytics
        public async Task Post([FromBody]AnalyticsModel value)
        {
            Trace.TraceInformation("Incoming connection...");
            await TableHelpers.Instance.SaveData(value);
        }

    }
}
