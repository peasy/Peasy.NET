namespace Peasy
{
    /// <summary>
    /// </summary>
    public class ServiceException : System.Exception
    {
        /// <summary>
        /// </summary>
        public ServiceException()
        {
        }

        /// <summary>
        /// </summary>
        public ServiceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// </summary>
        public ServiceException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
