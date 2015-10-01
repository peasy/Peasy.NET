using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peasy.Exception
{
    public class DomainObjectNotFoundException : PeasyException
    {
        public DomainObjectNotFoundException(string message)
            : base(message)
        {
        }

        public DomainObjectNotFoundException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
