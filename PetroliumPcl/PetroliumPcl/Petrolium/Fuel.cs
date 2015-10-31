using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetroliumPcl.DataRetrievers;

namespace PetroliumPcl.Petrolium
{
    public class Fuel:IFuel
    {
        public string NameGe { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public decimal Price { get; set; }
    }
}
