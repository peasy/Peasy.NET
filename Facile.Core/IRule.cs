using System;

namespace Facile.Core
{
    public interface IRule
    {
        string ErrorMessage { get; }
        IRule IfInvalidThenExecute(Action<IRule> method);
        IRule IfValidThenExecute(Action<IRule> method);
        IRule IfValidThenValidate(params IRule[] rule);
        bool IsValid { get; }
        bool IsInvalid { get; }
        IRule Validate();
    }
}
