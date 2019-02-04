using BasketTest.Core.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTest.Core.Interfaces
{
    public interface IBasketRepository
    {
        BasketResource GetBasket(Guid TransactionNumber);

        IEnumerable<BasketResource> GetAllBasket(int? domain = null);

    }
}
