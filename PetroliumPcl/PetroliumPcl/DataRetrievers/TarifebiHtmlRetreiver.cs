using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PetroliumPcl.DataRetrievers
{
    public class FuelWithOctan : IFuel
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

        public string Octan { get; set; }
    }

    public class TarifebiHtmlRetreiver : IDataRetriever
    {
        string _petrolId = String.Empty;

        public TarifebiHtmlRetreiver(string petroliumId)
        {
            _petrolId = petroliumId;
        }

        public IEnumerable<IFuel> RetreiveAll(string url)
        {
            HttpClient httpClient = new HttpClient();

            byte[] dataBuff = httpClient.GetByteArrayAsync(url).Result;

            string htmlString = Encoding.UTF8.GetString(dataBuff,0,dataBuff.Length);

            var resultHtml = WebUtility.HtmlDecode(htmlString);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(resultHtml);
            var smt = htmlDocument.DocumentNode.Descendants().Where(o => o.Name == "div" && o.Attributes["class"] != null && o.Attributes["class"].Value == "container");

            List<FuelWithOctan> _tarifebiFuel = new List<FuelWithOctan>();

            foreach (var item in smt)
            {
                var imgValue = item.Descendants().Where(o => o.Name == "img").SingleOrDefault().Attributes["src"].Value;

                if (imgValue.ToLower().Contains(_petrolId) )
                {
                    var descs = item.Descendants("tr").ToList();
                    descs.RemoveAt(0);//remove title

                    foreach (var subDesc in descs)
                    {
                        var tds = subDesc.Descendants("td");
                        string fuelType = tds.ElementAt(0).InnerText;
                        decimal price = decimal.Parse(tds.ElementAt(1).InnerText, CultureInfo.InvariantCulture);
                        string octan = tds.ElementAt(2).InnerText;

                        _tarifebiFuel.Add(new FuelWithOctan() {
                               NameEn=fuelType,
                               Octan=octan,
                               Price=price
                        });
                    }

                }
            }

            return _tarifebiFuel;
        }
    }
}
