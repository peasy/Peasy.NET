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
