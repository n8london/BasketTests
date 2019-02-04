using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTest.Persistence.Exceptions
{
    public class DataStoreException :Exception
    {
        public DataStoreException(string message) : base(message)
        {

        }
    }
}
