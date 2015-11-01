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
        IEnumerable<ILocation> Locations { get;}
    }

    public class PetrolCompany : IPetrolium,IPetroliumLocation
    {
        private List<StationLocation> _locations;

        public IEnumerable<IFuel> Fuels
        {
            get; set;
        }

        public string Id
        {
            get; set;
        }

        public IEnumerable<ILocation> Locations
        {
            get
            {
                if (_locations == null)
                    _locations = LocationContext.GetStationLocations(this.Id);

                return _locations;
            }
        }

        public string Name
        {
            get; set;
        }
    }
}
