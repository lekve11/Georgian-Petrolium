using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PetroliumPcl.Location
{
    #region Places proxy
    public class LocationApi
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Geometry
    {
        public LocationApi location { get; set; }
    }

    public class Result
    {
        public Geometry geometry { get; set; }
        public string id { get; set; }
        public string place_id { get; set; }
    }

    public class RootLocation
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

    #endregion

    public class StationLocation
    {
        public string StationId { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class LocationContext
    {
       private static Dictionary<string, string[]> _petroliumLocationUrls = new Dictionary<string, string[]>();

         static LocationContext()
        {
            _petroliumLocationUrls.Add("lukoil", new string[] { @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=lukoil+tbilisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c", @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=lukoil+batumi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
             @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=lukoil+kutaisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",@"https://maps.googleapis.com/maps/api/place/textsearch/json?query=lukoil+khashuri&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c"});
            
            _petroliumLocationUrls.Add("wissol",new string[] { @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=wissol&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c" , @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=wissol+batumi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c", @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=wissol+kutaisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
            @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=wissol+khashuri&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c"});
            _petroliumLocationUrls.Add("gulf",new string[] { @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=gulf+tbilisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c" , @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=gulf+batumi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
            @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=gulf+kutaisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",@"https://maps.googleapis.com/maps/api/place/textsearch/json?query=gulf+khashuri&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c"});

            _petroliumLocationUrls.Add("socar", new string[] { @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=socar+tbilisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
                @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=socar+batumi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
                @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=socar+khashuri&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c"
            });

            _petroliumLocationUrls.Add("rompetrol", new string[] { @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=rompetrol+tbilisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
                @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=rompetrol+batumi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",

            });

            _petroliumLocationUrls.Add("frego", new string[] { @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=frego+tbilisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
                    @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=frego+batumi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
                    @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=frego+kutaisi&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c",
                    @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=frego+kakheti&type=gas_station&key=AIzaSyAgl3JVvIhFLDE6XQE34poFa5PlsFZf56c"
            });
         }

        public static List<StationLocation> GetStationLocations(string petroliumId)
        {
            if (String.IsNullOrEmpty(petroliumId))
                return null;

            var currPetroliumUrls = _petroliumLocationUrls[petroliumId];

            List<StationLocation> locationsBo = new List<StationLocation>();

            HttpClient httpClient = new HttpClient();

            foreach (var item in currPetroliumUrls)
            {
                byte[] dataBuf = httpClient.GetByteArrayAsync(item).Result;

                string jsonString = Encoding.UTF8.GetString(dataBuf, 0, dataBuf.Length);

                RootLocation rootData = JsonConvert.DeserializeObject<RootLocation>(jsonString);

                foreach (var loc in rootData.results)
                {
                    if (!locationsBo.Exists(o => o.StationId == loc.place_id))
                        locationsBo.Add(new StationLocation() {
                                Latitude=loc.geometry.location.lat,
                                Longitude=loc.geometry.location.lng,
                                StationId=loc.place_id
                        });
                }
                  
            }

            return locationsBo;
        }
    }
}
