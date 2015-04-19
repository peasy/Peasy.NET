using System;
using System.Collections.Generic;

namespace Facile.Core
{
    /// <summary>
    /// A validation rule to run againt records being processed.
    /// </summary>
    public abstract class RuleBase : IRule
    {
        /// <summary>
        /// The action to perform once when this rule passes validation.
        /// </summary>
        protected Action<IRule> _ifValidThenExecute;

        /// <summary>
        /// The action to perform once when this rule fails validation.
        /// </summary>
        protected Action<IRule> _ifInvalidThenExecute;

        /// <summary>
        /// Gets or sets the message to be supplied to caller in the event that no rule dependencies exist via IfValidThenValidate()
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this rule is valid.
        /// </summary>
        /// <value>
        /// <c>True</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// Gets or sets the <see cref="RuleBase"/> that should be evaluated after this instance.
        /// </summary>
        /// <value>
        /// The successor <see cref="RuleBase"/>.
        /// </value>
        private IRule Successor { get; set; }

        /// <summary>
        /// Validates this rule.
        /// </summary>
        public IRule Validate()
        {
            IsValid = true;
            OnValidate();
            if (IsValid)
            {
                if (Successor != null)
                {
                    Successor.Validate();
                    IsValid = Successor.IsValid;
                    ErrorMessage = Successor.ErrorMessage;
                }

                if (_ifValidThenExecute != null)
                {
                    _ifValidThenExecute(this);
                    _ifValidThenExecute = null;
                }
            }
            else if (_ifInvalidThenExecute != null)
            {
                _ifInvalidThenExecute(this);
                _ifInvalidThenExecute = null;
            }
            return this;
        }

        /// <summary>
        /// Validates the supplied <see cref="RuleBase"/> if this rule passes validation.
        /// </summary>
        /// <param name="rule">The <see cref="RuleBase"/>.</param>
        /// <returns>The supplied <see cref="RuleBase"/>.</returns>
        public IRule IfValidThenValidate(IRule rule)
        {
            Successor = rule;
            return this;
        }

        /// <summary>
        /// Execute an action if the rule passes validation.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public IRule IfValidThenExecute(Action<IRule> method)
        {
            _ifValidThenExecute = method;
            return this;
        }

        /// <summary>
        /// Execute an action if the rule fails validation.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public IRule IfInvalidThenExecute(Action<IRule> method)
        {
            _ifInvalidThenExecute = method;
            return this;
        }

        /// <summary>
        /// Called when the <see cref="M:Facile.Rules.RuleBase.Validate()"/> method is called.
        /// </summary>
        /// <returns>
        /// <c>True</c> if validation succeeded; otherwise <c>false</c>.
        /// </returns>
        protected abstract void OnValidate();

        protected virtual void Invalidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsValid = false;
        }
    }
}
