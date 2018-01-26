namespace Peasy
{
    public interface ITransactionSupportStatusProvider
    {
        bool SupportsTransactions { get; }
    }
}