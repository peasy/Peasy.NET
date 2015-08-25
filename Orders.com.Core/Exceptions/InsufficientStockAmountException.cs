using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Exceptions
{
    public class InsufficientStockAmountException : Exception
    {
        public InsufficientStockAmountException(string message) : base(message)
        {
        }
    }
}
