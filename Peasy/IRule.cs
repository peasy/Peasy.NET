using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    public interface IRule
    {
        IDictionary<string, string> ErrorMessages { get; }
        IRule IfInvalidThenExecute(Action<IRule> method);
        IRule IfValidThenExecute(Action<IRule> method);
        IRule IfValidThenValidate(params IRule[] rule);
        bool IsValid { get; }
        IRule Validate();
        Task<IRule> ValidateAsync();
    }
}
