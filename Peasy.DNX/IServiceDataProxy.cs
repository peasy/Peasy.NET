using Peasy.Core;

namespace Peasy
{
    public interface IServiceDataProxy<T, TKey> : IDataProxy<T, TKey>,
                                                  ITransactionSupportStatusProvider,
                                                  ILatencyProneStatusProvider
    {
    }
}
