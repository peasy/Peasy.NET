using System;
using System.Threading.Tasks;

namespace Facile.Core
{
    public interface IRule
    {
        string ErrorMessage { get; }
        IRule IfInvalidThenExecute(Action<IRule> method);
        IRule IfValidThenExecute(Action<IRule> method);
        IRule IfValidThenValidate(params IRule[] rule);
        bool IsValid { get; }
        IRule Validate();
        Task<IRule> ValidateAsync();
    }
}
