namespace Peasy.Exception
{
    public class ConcurrencyException : PeasyException
    {
        public ConcurrencyException(string message)
            : base(message)
        {
        }

        public ConcurrencyException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
