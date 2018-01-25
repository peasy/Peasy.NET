namespace Peasy.Exception
{
    public class DomainObjectNotFoundException : PeasyException
    {
        public DomainObjectNotFoundException(string message)
            : base(message)
        {
        }

        public DomainObjectNotFoundException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
