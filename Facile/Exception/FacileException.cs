using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Exception
{
    public class FacileException : System.Exception
    {
        public FacileException()
        {
        }

        public FacileException(string message)
            : base(message)
        {
        }

        public FacileException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
