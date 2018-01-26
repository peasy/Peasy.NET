namespace Peasy
{
    public interface ILatencyProneStatusProvider
    {
        bool IsLatencyProne { get; }
    }
}