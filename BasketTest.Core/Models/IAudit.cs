using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTest.Core.Models
{
    public interface IAudit
    {
         long CreatedBy { get; set; }

         DateTime? ModifiedDate { get; set; }

         long? ModifiedBy { get; set; }
    }
}
