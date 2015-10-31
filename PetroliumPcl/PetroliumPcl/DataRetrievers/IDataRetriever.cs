using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroliumPcl.DataRetrievers
{
    public interface IDataRetriever
    {
        IEnumerable<IFuel> RetreiveAll(string url);
    }

    public interface IFuel
    {
        string NameGe { get; set; }
        string NameEn { get; set; }
        string NameRu { get; set; }
        decimal Price { get; set; }
    }
}
