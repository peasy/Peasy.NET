using System;
using System.Threading.Tasks;

namespace Peasy
{
    public interface IRule
    {
		string Association { get; set; }
        string ErrorMessage { get; }
        IRule IfInvalidThenExecute(Action<IRule> method);
        IRule IfValidThenExecute(Action<IRule> method);
        IRule IfValidThenValidate(params IRule[] rule);
        bool IsValid { get; }
        IRule Validate();
        Task<IRule> ValidateAsync();
    }
}
