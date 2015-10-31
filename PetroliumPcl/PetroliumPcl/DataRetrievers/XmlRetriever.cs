using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetroliumPcl.DataRetrievers
{
    [XmlType("prices")]
    public class PetroliumXmlRoot
    {
        [XmlElement("item",type:typeof(FuelXmlProxy))]
        public virtual List<FuelXmlProxy> Items { get; set; }
    }

    [XmlType("item")]
    public class FuelXmlProxy:IFuel
    {
        [XmlElement("title_geo")]
        public string NameGe { get; set; }

        [XmlElement("title_eng")]
        public string NameEn { get; set; }

        [XmlElement("title_rus")]
        public string NameRu { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        
    }

    public class XmlRetriever: IDataRetriever
    {
        public object WebClient { get; private set; }

        public IEnumerable<IFuel> RetreiveAll(string url)
        {
            HttpClient httpClient = new HttpClient();

            byte[] data = httpClient.GetByteArrayAsync(url).Result;

            IEnumerable<FuelXmlProxy> datas;
            using (var stream = new MemoryStream(data))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PetroliumXmlRoot));

                var deserialized = (PetroliumXmlRoot)xmlSerializer.Deserialize(stream);
                datas = deserialized.Items;
            }

            return datas;
        }
    }
}
