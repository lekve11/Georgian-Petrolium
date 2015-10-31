using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PetroliumPcl.DataRetrievers
{

    public class GulfPrices
    {
        [JsonProperty("key")]
        public string FuelKey { get; set; }

        [JsonProperty("value")]
        public decimal Price { get; set; }
    }


    public class DataOfPrices
    {
        [JsonProperty("prices")]
        public List<GulfPrices> FuelWithPrices { get; set; }
    }

    public class RootObjectForGulf<T>
    {
        public string status { get; set; }
        public T message { get; set; }
    }

    public class FuelKeys
    {
        public string key { get; set; }
        public string name_ge { get; set; }
        public string name_en { get; set; }
    }

    public class GulfFuel : IFuel
    {
        public string NameEn
        {
            get; set;
        }

        public string NameGe
        {
            get; set;
        }

        public string NameRu
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public string Key { get; set; }
    }


    public class GulfJsonRetreiver : IDataRetriever
    {
        public IEnumerable<IFuel> RetreiveAll(string url)
        {
            HttpClient httpClient = new HttpClient();

            byte[] gulfFuelsBuff = httpClient.GetByteArrayAsync(url).Result;

            string mainJsonString = Encoding.UTF8.GetString(gulfFuelsBuff,0,gulfFuelsBuff.Length);

            byte[] fuelKeysBuff = httpClient.GetByteArrayAsync(@"http://gulf.ge/web_service/?username=ws_usr&password=6K0d7WBroVVAdFV&action=get_fuel_types").Result;

            string keysJsonString = Encoding.UTF8.GetString(fuelKeysBuff,0,fuelKeysBuff.Length);

            var rootForFuels = JsonConvert.DeserializeObject<RootObjectForGulf<DataOfPrices>>(mainJsonString);
            var rootForKeys = JsonConvert.DeserializeObject<RootObjectForGulf<List<FuelKeys>>>(keysJsonString);

            var fuelsBo = from f in rootForFuels.message.FuelWithPrices
                          join k in rootForKeys.message on f.FuelKey equals k.key
                          select new GulfFuel() { Key = k.key, NameEn = k.name_en, NameGe = k.name_ge, NameRu = null, Price = f.Price };

            return fuelsBo;
        }
    }

}
