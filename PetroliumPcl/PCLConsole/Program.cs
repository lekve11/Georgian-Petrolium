using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetroliumPcl;
using PetroliumPcl.Petrolium;

namespace PCLConsole
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
        public List<GulfPrices> prices { get; set; }
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


    class Program
    {
        

        static void Main(string[] args)
        {
            List<PetrolCompany> petroliums = PetrolCompanyContext.GetPetroliums();

            foreach (var item in petroliums)
            {
                Console.WriteLine("----" + item.Name + "----");
                foreach (var f in item.Fuels)
                {
                    Console.WriteLine(String.Format("{0} {1} {2}  -> {3}", f.NameEn, f.NameRu, f.NameGe, f.Price));
                }
            }

            PetrolCompany lukoil = petroliums[0];

            var locationDist = lukoil.Locations.Distinct().ToList();

            Console.ReadLine();
        }
    }
}
