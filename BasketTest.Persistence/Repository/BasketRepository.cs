using BasketTest.Core.ApiModels;
using BasketTest.Core.Interfaces;
using BasketTest.Persistence.DataStore;
using BasketTest.Persistence.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BasketTest.Persistence.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDataStoreContext _dataStoreContext;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public BasketRepository(IDataStoreContext dataStoreContext)
        {
            _dataStoreContext = dataStoreContext;
        }

        /// <summary>
        /// Get All Basket from the Store
        /// </summary>
        /// <param name="domain">optional domain filter</param>
        /// <returns>collection of Basket Resource</returns>
        public IEnumerable<BasketResource> GetAllBasket(int? domain = null)
        {
            try
            {
                var baskets = _dataStoreContext.Baskets.OrderByDescending(x => x.CreatedDateTime);
                if(domain.HasValue)
                {
                    baskets = baskets.Where(x => x.Domain == domain.Value).OrderByDescending(x => x.CreatedDateTime);
                }
                if (baskets != null)
                {
                    return baskets.Select(x => new BasketResource
                    {
                        TransactionNumber = x.TransactionNumber,
                        NumberOfPassengers = x.NumberOfPassengers,
                        Domain = x.Domain,
                        AgentId = x.AgentId,
                        ReferrerUrl = x.ReferrerUrl,
                        UserId = x.UserId,
                        SelectedCurrency = x.SelectedCurrency,
                        ReservationSystem = x.ReservationSystem
                    });
                }
            }
            catch (DataStoreException ex)
            {
                logger.Error($"An exception has been thrown while Getting Basket: in method GetBasket: {ex.Message}");
                throw;
            }

            return null;
        }

        public  BasketResource GetBasket(Guid transactionNumber)
        {
            if (transactionNumber == null || transactionNumber==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(transactionNumber));
            }
                
            try
            {
                var basket = _dataStoreContext.Baskets.Where(x => x.TransactionNumber == transactionNumber).FirstOrDefault();
                if(basket != null)
                {
                   var basketResource = new BasketResource
                    {
                        TransactionNumber = basket.TransactionNumber,
                        NumberOfPassengers = basket.NumberOfPassengers,
                        Domain = basket.Domain,
                        AgentId = basket.AgentId,
                        ReferrerUrl = basket.ReferrerUrl,
                        UserId = basket.UserId,
                        SelectedCurrency = basket.SelectedCurrency,
                        ReservationSystem = basket.ReservationSystem

                    };
                    return basketResource;
                }
            }
            catch (DataStoreException ex)
            {
                logger.Error($"An exception has been thrown while Getting Basket: in method GetBasket: {ex.Message}");
                throw;
            }
            return null;
        }
    }
}
