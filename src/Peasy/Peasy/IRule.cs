using System;
using System.Threading.Tasks;

namespace Peasy
{
    public interface IRule
    {
        string Association { get; }
        string ErrorMessage { get; }
        bool IsValid { get; }
        IRule Validate();
        Task<IRule> ValidateAsync();
    }
}
