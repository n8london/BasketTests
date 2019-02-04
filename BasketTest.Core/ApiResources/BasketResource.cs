using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTest.Core.ApiModels
{
    public class BasketResource
    {
        public Guid TransactionNumber { get; set; }

        public long? NumberOfPassengers { get; set; }

        public int? Domain { get; set; }

        public long? AgentId { get; set; }

        public string ReferrerUrl { get; set; }

        public string UserId { get; set; }

        public string SelectedCurrency { get; set; }

        public string ReservationSystem { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
