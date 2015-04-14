using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Exception
{
    public class DomainObjectNotFoundException : FacileException
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
