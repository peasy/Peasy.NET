using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoebe.Core.Transaction
{
    public interface ITransactionSupportStatusContainer
    {
        bool SupportsTransactions { get; }
    }
}
