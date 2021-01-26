using System;
using System.Collections.Generic;

namespace Peasy.Synchronous
{
    /// <summary>
    /// A validation rule to run againt records being processed.
    /// </summary>
    public abstract class SynchronousRuleBase : ISynchronousRule, IRuleSuccessorsContainer<ISynchronousRule>
    {
        /// <summary>
        /// The action to perform once when this rule passes validation.
        /// </summary>
        protected Action<ISynchronousRule> _ifValidThenInvoke;

        /// <summary>
        /// The action to perform once when this rule fails validation.
        /// </summary>
        protected Action<ISynchronousRule> _ifInvalidThenInvoke;

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
        /// Gets or sets the list of <see cref="ISynchronousRule"/> that should be evaluated upon successful validation.
        /// </summary>
        private List<IRuleSuccessor<ISynchronousRule>> Successors { set; get; } = new List<IRuleSuccessor<ISynchronousRule>>();

        ///<inheritdoc cref="IRuleSuccessorsContainer{T}.GetSuccessors"/>
        public IEnumerable<IRuleSuccessor<ISynchronousRule>> GetSuccessors()
        {
            return Successors;
        }

        /// <summary>
        /// Synchronously validates this rule.
        /// </summary>
        public ISynchronousRule Execute()
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
        /// Validates the supplied list of <see cref="ISynchronousRule"/> upon successful validation.
        /// </summary>
        /// <param name="rules">The <see cref="ISynchronousRule"/>.</param>
        /// <returns>The supplied <see cref="RuleBase"/>.</returns>
        public SynchronousRuleBase IfValidThenValidate(params ISynchronousRule[] rules)
        {
            Successors.Add(new RuleSuccessor<ISynchronousRule>(rules));
            return this;
        }

        /// <summary>
        /// Executes the supplied action upon successful validation.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public SynchronousRuleBase IfValidThenInvoke(Action<ISynchronousRule> method)
        {
            _ifValidThenInvoke = method;
            return this;
        }

        /// <summary>
        /// Executes the supplied action upon failed validation.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public SynchronousRuleBase IfInvalidThenInvoke(Action<ISynchronousRule> method)
        {
            _ifInvalidThenInvoke = method;
            return this;
        }

        /// <summary>
        /// Called when the <see cref="M:Peasy.Rules.RuleBase.Execute()"/> method is called.
        /// </summary>
        protected virtual void OnValidate() {}

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
    }
}
