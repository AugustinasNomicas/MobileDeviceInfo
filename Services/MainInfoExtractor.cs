using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MobileDeviceInfo.Services
{
    /// <summary>
    /// Extracts common variables from BuildParams and give them meaningful names. 
    /// Tested on HTC One.
    /// </summary>
    public class MainInfoExtractor
    {
        private readonly DeviceInfoRetriever _deviceInfoRetriever;

        public MainInfoExtractor(DeviceInfoRetriever deviceInfoRetriever)
        {
            _deviceInfoRetriever = deviceInfoRetriever;
        }

        public Dictionary<string, string> ExtractMainInfo(List<string> keys)
        {
            var res = from inf in keys
                      where inf.StartsWith("ro.product")
                      select new KeyValuePair<string, string>(FirstCharToUpper(inf.Substring(11)), _deviceInfoRetriever.GetDeviceInfoValue(inf));


            return res.ToDictionary(k => k.Key, v => v.Value);
        }

        public static string FirstCharToUpper(string input)
        {
            return input.First().ToString(CultureInfo.InvariantCulture).ToUpper() + String.Join("", input.Skip(1));
        }
    }
}
