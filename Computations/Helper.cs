using System;

namespace OptionPricing.Computations
{
    public class Helper
    {
        //

        //Dates
        public static DateTime unixToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
        public static long dateTimeToUnix(DateTime dt)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unix = (dt.ToUniversalTime() - dtDateTime);
            return Convert.ToInt64(unix.TotalSeconds);
        }
    }
}
