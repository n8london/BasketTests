using BasketTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTest.Core.Interfaces
{
    public interface IDataStoreContext
    {
        IEnumerable<Basket> Baskets { get; set; }
    }
}
