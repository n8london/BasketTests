using BasketTest.Core.Interfaces;
using BasketTest.Core.Models;
using BasketTest.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTest.Persistence.DataStore
{
    public class BasketContext: IDataStoreContext, IDisposable
    {
        public BasketContext()
        {
             Baskets = File.ReadAllLines("..\\BasketTest.Persistence\\DataStore\\Baskets.csv")
                                           .Skip(1)
                                           .Select(v => FromCsv(v))
                                           .AsEnumerable();
        }

        public IEnumerable<Basket> Baskets { get; set; }

        public void Dispose()
        {
            Baskets = null;
        }

        public Basket FromCsv(string csvLine)
        {
            var basket = new Basket();
            try
            {
                string[] values = csvLine.Split(',');
                basket.TransactionNumber = Guid.Parse(values[0]);
                basket.NumberOfPassengers = (!string.IsNullOrEmpty(values[1])) ? long.Parse(values[1]) : 0;
                basket.Domain = int.Parse(values[2]);
                basket.AgentId = (!string.IsNullOrEmpty(values[3])) ? long.Parse(values[3]) : (long?)null;
                basket.ReferrerUrl = (!string.IsNullOrEmpty(values[4])) ? values[4].ToString() : string.Empty;
                basket.CreatedDateTime = DateTime.Parse((values[5]));
                basket.UserId = (values[6]).ToString();
                basket.SelectedCurrency = (values[7]).ToString();
                basket.ReservationSystem = (values[8]).ToString();
                basket.CreatedBy = 1;
            }
            catch(Exception ex)
            {
                throw new DataStoreException($"An Error has Occured while retrieving Data: Details:{ex.Message}");
            }
            
            return basket;
        }

    }
}
