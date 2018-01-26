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
