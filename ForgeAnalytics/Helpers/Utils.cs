using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForgeAnalytics.Helpers
{
    public class Utils
    {
        public static long epochTime(DateTime now)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(now - sTime).TotalSeconds;
        }
    }
}