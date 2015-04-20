using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public interface IConcurrencyCheckSupporter
    {
        bool VersionEquals(IConcurrencyCheckSupporter concurrencyCheckSupporter);
    }
}
