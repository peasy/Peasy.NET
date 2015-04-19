using System;

namespace Facile.Core
{
    public interface IRule
    {
        string ErrorMessage { get; }
        IRule IfInvalidThenExecute(Action<IRule> method);
        IRule IfValidThenExecute(Action<IRule> method);
        IRule IfValidThenValidate(IRule rule);
        bool IsValid { get; }
        IRule Validate();
    }
}
