using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetroliumPcl.DataRetrievers;
using PetroliumPcl.Petrolium;

namespace PetroliumPcl.Factory
{
   public abstract class PetroliumCreator
   {
        public abstract IPetrolium FactoryMethod(IDataRetriever dataRetreiver,string apiUrl,string petroliumId,string petroliumName);

    }
}
