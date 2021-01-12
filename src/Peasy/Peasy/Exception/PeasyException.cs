namespace Peasy.Exception
{
    using System;

    /// <summary>
    /// An exception that can be thrown or handled when a peasy specific error occurs.
    /// </summary>
    public class PeasyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Peasy.Exception.PeasyException class.
        /// </summary>
        public PeasyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Peasy.Exception.PeasyException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public PeasyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Peasy.Exception.PeasyException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public PeasyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
