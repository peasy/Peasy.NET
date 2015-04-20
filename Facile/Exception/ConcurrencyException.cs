using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Exception
{
    public class ConcurrencyException : FacileException
    {
        public ConcurrencyException(string message)
            : base(message)
        {
        }

        public ConcurrencyException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
