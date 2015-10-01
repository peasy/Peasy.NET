using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peasy.Exception
{
    public class ConcurrencyException : PeasyException
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
