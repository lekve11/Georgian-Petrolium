using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetroliumPcl.DataRetrievers;
using PetroliumPcl.Location;

namespace PetroliumPcl.Petrolium
{
    public interface IPetrolium
    {
        string Id { get; set; }

        string Name { get; set; }

        IEnumerable<IFuel> Fuels { get; set; }
    }

    public interface IPetroliumLocation
    {
        IEnumerable<StationLocation> Locations { get; set; }
    }

    public class PetrolCompany : IPetrolium,IPetroliumLocation
    {
        public IEnumerable<IFuel> Fuels
        {
            get; set;
        }

        public string Id
        {
            get; set;
        }

        public IEnumerable<StationLocation> Locations
        {
            get;set;
        }

        public string Name
        {
            get; set;
        }
    }
}
