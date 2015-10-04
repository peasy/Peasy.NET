using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peasy.Exception
{
    public class PeasyException : System.Exception
    {
        public PeasyException()
        {
        }

        public PeasyException(string message)
            : base(message)
        {
        }

        public PeasyException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
