using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peasy
{
    public class ServiceException : System.Exception
    {
        public ServiceException()   
        {
        }

        public ServiceException(string message)
            : base(message)
        {
        }

        public ServiceException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
