using System;
using System.Collections.Generic;
using PetroliumPcl.DataRetrievers;
using PetroliumPcl.Location;
using PetroliumPcl.Petrolium;

namespace PetroliumPcl.Factory
{
    //factory class for creating petrolium from xml api
    public class PetrolCompanyCreator : PetroliumCreator 
    {
        public override IPetrolium FactoryMethod(IDataRetriever dataRetreiver, string apiUrl, string petroliumId, string petroliumName)
        {
            PetrolCompany petrolCompany = new PetrolCompany();
            petrolCompany.Id = petroliumId;
            petrolCompany.Name = petroliumName;
            
            if(dataRetreiver!= null)
            {
                IEnumerable<IFuel> fuelsFromApi = dataRetreiver.RetreiveAll(apiUrl);
                petrolCompany.Fuels = fuelsFromApi;
            }

            petrolCompany.Locations = LocationContext.GetStationLocations(petrolCompany.Id);
            return petrolCompany;
        }
        
    }
}
