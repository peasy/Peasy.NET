using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// A validation rule to run againt records being processed.
    /// </summary>
    public abstract class RuleBase : IRule, IRuleSuccessorsContainer
    {
        /// <summary>
        /// The action to perform once when this rule passes validation.
        /// </summary>
        protected Action<IRule> _ifValidThenInvoke;

        /// <summary>
        /// The action to perform once when this rule fails validation.
        /// </summary>
        protected Action<IRule> _ifInvalidThenInvoke;

        /// <summary>
        /// Gets or sets a string that associates this rule with a field. This is helpful for validation errors
        /// </summary>
        public string Association { get; protected set; }

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
        /// Gets or sets the list of <see cref="IRule"/> that should be evaluated upon successful validation.
        /// </summary>
        private List<IRuleSuccessor> Successors { set; get; } = new List<IRuleSuccessor>();

        ///<inheritdoc cref="IRuleSuccessorsContainer.GetSuccessors"/>
        public IEnumerable<IRuleSuccessor> GetSuccessors()
        {
            return Successors;
        }

        /// <summary>
        /// Synchronously validates this rule.
        /// </summary>
        public IRule Execute()
        {
            IsValid = true;
            OnValidate();
            if (IsValid)
            {
                if (Successors != null)
                {
                    foreach (var successor in Successors)
                    {
                        foreach (var rule in successor)
                        {
                            rule.Execute();
                            if (!rule.IsValid)
                            {
                                Invalidate(rule.ErrorMessage, rule.Association);
                                _ifInvalidThenInvoke?.Invoke(this);
                                break; // early exit, don't bother further rule execution
                            }
                        }
                        if (!IsValid) break;
                    }
                }
                _ifValidThenInvoke?.Invoke(this);
            }
            else
            {
                _ifInvalidThenInvoke?.Invoke(this);
            }
            return this;
        }

        /// <summary>
        /// Validates the supplied list of <see cref="IRule"/> upon successful validation.
        /// </summary>
        /// <param name="rules">The <see cref="IRule"/>.</param>
        /// <returns>The supplied <see cref="RuleBase"/>.</returns>
        public RuleBase IfValidThenValidate(params IRule[] rules)
        {
            Successors.Add(new RuleSuccessor(rules));
            return this;
        }

        /// <summary>
        /// Executes the supplied action upon successful validation.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public RuleBase IfValidThenInvoke(Action<IRule> method)
        {
            _ifValidThenInvoke = method;
            return this;
        }

        /// <summary>
        /// Executes the supplied action upon failed validation.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public RuleBase IfInvalidThenInvoke(Action<IRule> method)
        {
            _ifInvalidThenInvoke = method;
            return this;
        }

        /// <summary>
        /// Called when the <see cref="M:Peasy.Rules.RuleBase.Validate()"/> method is called.
        /// </summary>
        protected virtual void OnValidate() {}

        /// <summary>
        /// Called when the <see cref="M:Peasy.Rules.RuleBase.ValidateAsync()"/> method is called.
        /// </summary>
        protected virtual Task OnValidateAsync()
        {
            OnValidate();
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Invalidates the rule
        /// </summary>
        /// <param name="errorMessage">The error message to associate with the broken rule</param>
        protected virtual void Invalidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsValid = false;
        }

        /// <summary>
        /// Invalidates the rule
        /// </summary>
        /// <param name="errorMessage">The error message to associate with the broken rule</param>
        /// <param name="association">Sets the <see cref="Association"/> value></param>
        protected virtual void Invalidate(string errorMessage, string association)
        {
            Association = association;
            Invalidate(errorMessage);
        }

        /// <summary>
        /// Asynchronously validates this rule.
        /// </summary>
        public async Task<IRule> ExecuteAsync()
        {
            IsValid = true;
            await OnValidateAsync();
            if (IsValid)
            {
                if (Successors != null)
                {
                    foreach (var successor in Successors)
                    {
                        foreach (var rule in successor)
                        {
                            await rule.ExecuteAsync();
                            if (!rule.IsValid)
                            {
                                Invalidate(rule.ErrorMessage, rule.Association);
                                _ifInvalidThenInvoke?.Invoke(this);
                                break; // early exit, don't bother further rule execution
                            }
                        }
                        if (!IsValid) break;
                    }
                }
                _ifValidThenInvoke?.Invoke(this);
            }
            else
            {
                _ifInvalidThenInvoke?.Invoke(this);
            }
            return this;
        }
    }
}
