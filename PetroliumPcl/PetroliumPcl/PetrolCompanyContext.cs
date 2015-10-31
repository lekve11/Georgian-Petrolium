using System;
using System.Collections.Generic;
using System.Linq;
using PetroliumPcl.DataRetrievers;
using PetroliumPcl.Factory;
using PetroliumPcl.Petrolium;

namespace PetroliumPcl
{
    public class MediaType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class PetroliumMetaData
    {
        public string PetroliumId { get; set; }

        public string PetroliumName { get; set; }

        public string ApiUrl { get; set; }

        public MediaType MediaType { get; set; }
    }

    public  class PetrolCompanyContext
    {
        private static List<PetroliumMetaData> _metadatas;

        static PetrolCompanyContext(){

            List<MediaType> mediaTypes = new List<MediaType>() {
                 new MediaType() {Id=1,Name="XML" },
                 new MediaType() { Id=2,Name="JSON"},
                 new MediaType() {Id=3,Name="HTML" },//html media type arari mara ravqnat magat movutyan ....
            };

             _metadatas = new List<PetroliumMetaData>() {
                    new PetroliumMetaData() { ApiUrl=@"http://lukoil.ge/xml.php",MediaType=mediaTypes.Where(o=>o.Name.ToLower()=="xml").SingleOrDefault(),PetroliumId="lukoil",PetroliumName="Lukoil" },
                    new PetroliumMetaData() { ApiUrl=@"http://wissol.ge/xml.php",MediaType=mediaTypes.Where(o=>o.Name.ToLower()=="xml").SingleOrDefault(),PetroliumId="wissol",PetroliumName="Wissol" },
                    new PetroliumMetaData() {ApiUrl=@"http://gulf.ge/web_service/?username=ws_usr&password=6K0d7WBroVVAdFV&action=get_current_fuel_prices",MediaType=mediaTypes.Where(o=>o.Name.ToLower()=="json").SingleOrDefault(),PetroliumId="gulf",PetroliumName="Gulf" }

            };
        }

        public static List<PetrolCompany> GetPetroliums() {

            List<PetrolCompany> petroliums = new List<PetrolCompany>();
            petroliums.AddRange(getXmlPetroliums());
            petroliums.AddRange(getJsonPetroliums());

            return petroliums;
        }

        private static List<PetrolCompany> getJsonPetroliums() {

            PetrolCompanyCreator petroliumCreator = new PetrolCompanyCreator();

            var jsonBasedPetroliums = _metadatas.Where(o => o.MediaType.Name.ToLower() == "json").ToList();

            List<PetrolCompany> petrolCompanies = new List<PetrolCompany>();

            foreach (var item in jsonBasedPetroliums)
            {
                if (item.PetroliumId == "gulf")
                    petrolCompanies.Add((PetrolCompany)petroliumCreator.FactoryMethod(new GulfJsonRetreiver(), item.ApiUrl, item.PetroliumId, item.PetroliumName));
                
            }

            return petrolCompanies;
        }

        private static List<PetrolCompany> getXmlPetroliums()
        {
            PetrolCompanyCreator petrolCompanyCreator = new PetrolCompanyCreator();

            var xmlBased = _metadatas.Where(o => o.MediaType.Id == 1).ToList();

            List<PetrolCompany> xmlApiCompanies = new List<PetrolCompany>();

            foreach (var item in xmlBased)
                xmlApiCompanies.Add((PetrolCompany)petrolCompanyCreator.FactoryMethod(new XmlRetriever(), item.ApiUrl, item.PetroliumId, item.PetroliumName));
            

            return xmlApiCompanies;
        }
    }
}
