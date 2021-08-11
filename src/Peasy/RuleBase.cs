using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <inheritdoc cref="IRule"/>
    public abstract class RuleBase : IRule, IRuleSuccessorsContainer<IRule>
    {
        /// <summary>
        /// An action to perform if this rule passes validation.
        /// </summary>
        private Func<IRule, Task> _ifValidThenInvokeAsync;

        /// <summary>
        /// An action to perform if this rule fails validation.
        /// </summary>
        private Func<IRule, Task> _ifInvalidThenInvokeAsync;

        /// <inheritdoc cref="IRule.Association"/>
        public string Association { get; protected set; }

        /// <inheritdoc cref="IRule.ErrorMessage"/>
        public string ErrorMessage { get; protected set; }

        /// <inheritdoc cref="IRule.IsValid"/>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// Gets or sets the list of <see cref="IRule"/> that should be evaluated upon successful validation.
        /// </summary>
        private List<IRuleSuccessor<IRule>> Successors { set; get; } = new List<IRuleSuccessor<IRule>>();

        /// <inheritdoc cref="IRule.ExecuteAsync"/>
        public async Task<IRule> ExecuteAsync()
        {
            IsValid = true;
            await OnValidateAsync();
            if (IsValid)
            {
                await (_ifValidThenInvokeAsync?.Invoke(this) ?? Task.FromResult<object>(null));
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
                                break; // early exit, don't bother further rule execution
                            }
                        }
                        if (!IsValid) break;
                    }
                }
            }
            else
            {
                await (_ifInvalidThenInvokeAsync?.Invoke(this) ?? Task.FromResult<object>(null));
            }
            return this;
        }

        /// <summary>
        /// Allows creating an array of <see cref="RuleBase"/> by chaining them together with the '+' (addition) sign.
        /// </summary>
        /// <returns>The supplied <see cref="RuleBase"/>.</returns>
        public static RuleBase operator +(RuleBase a) => a;

        /// <summary>
        /// Creates an array of <see cref="RuleBase"/> by concatentating 2 rules via the '+' (addition) operator.
        /// </summary>
        /// <param name="a">The first rule used in the concatenation operation.</param>
        /// <param name="b">The second rule used in the concatenation operation.</param>
        /// <returns>An array of <see cref="RuleBase"/>.</returns>
        public static RuleBase[] operator +(RuleBase a, RuleBase b) => new [] { a }.Concat(new [] { b }).ToArray();

        /// <summary>
        /// Creates an array of <see cref="RuleBase"/> by concatentating a single rule with a rule array via the '+' (addition) operator.
        /// </summary>
        /// <param name="a">The single rule used in the concatenation operation.</param>
        /// <param name="b">The rule list used in the concatenation operation.</param>
        /// <returns>An array of <see cref="RuleBase"/>.</returns>
        public static RuleBase[] operator +(RuleBase a, RuleBase[] b) => new RuleBase[] { a }.Concat(b).ToArray();

        /// <summary>
        /// Creates an array of <see cref="RuleBase"/> by concatentating a rule array with a single rule via the '+' (addition) operator.
        /// </summary>
        /// <param name="a">The rule list used in the concatenation operation.</param>
        /// <param name="b">The single rule used in the concatenation operation.</param>
        /// <returns>An array of <see cref="RuleBase"/>.</returns>
        public static RuleBase[] operator +(RuleBase[] a, RuleBase b) => a.Concat(new [] { b }).ToArray();

        /// <summary>
        /// Creates an array of <see cref="RuleBase"/> by concatentating 2 rules via the &amp; (ampersand) operator.
        /// </summary>
        /// <param name="a">The first rule used in the concatenation operation.</param>
        /// <param name="b">The second rule used in the concatenation operation.</param>
        /// <returns>An array of <see cref="RuleBase"/>.</returns>
        public static RuleBase[] operator &(RuleBase a, RuleBase b) => new [] { a }.Concat(new [] { b }).ToArray();

        /// <summary>
        /// Creates an array of <see cref="RuleBase"/> by concatentating a single rule with a rule array via the &amp; (ampersand) operator.
        /// </summary>
        /// <param name="a">The single rule used in the concatenation operation.</param>
        /// <param name="b">The rule list used in the concatenation operation.</param>
        /// <returns>An array of <see cref="RuleBase"/>.</returns>
        public static RuleBase[] operator &(RuleBase a, RuleBase[] b) => new RuleBase[] { a }.Concat(b).ToArray();

        /// <summary>
        /// Creates an array of <see cref="RuleBase"/> by concatentating a rule array with a single rule via the &amp; (ampersand) operator.
        /// </summary>
        /// <param name="a">The rule list used in the concatenation operation.</param>
        /// <param name="b">The single rule used in the concatenation operation.</param>
        /// <returns>An array of <see cref="RuleBase"/>.</returns>
        public static RuleBase[] operator &(RuleBase[] a, RuleBase b) => a.Concat(new [] { b }).ToArray();

        /// <summary>
        /// Validates the supplied list of rules upon successful validation of this rule.
        /// </summary>
        /// <param name="rules">The rules to validate.</param>
        /// <returns>A reference to this rule.</returns>
        public RuleBase IfValidThenValidate(params IRule[] rules)
        {
            Successors.Add(new RuleSuccessor<IRule>(rules));
            return this;
        }

        /// <summary>
        /// Asynchonchronously executes the supplied function upon successful validation of this rule.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public RuleBase IfValidThenInvoke(Func<IRule, Task> method)
        {
            _ifValidThenInvokeAsync = method;
            return this;
        }

        /// <summary>
        /// Asynchonchronously executes the supplied function upon failed validation of this rule.
        /// </summary>
        /// <param name="method">The action to perform.</param>
        public RuleBase IfInvalidThenInvoke(Func<IRule, Task> method)
        {
            _ifInvalidThenInvokeAsync = method;
            return this;
        }

        /// <summary>
        /// Performs business or validation logic.
        /// </summary>
        /// <remarks>
        /// <para>This method is called upon invocation of <see cref="RuleBase.ExecuteAsync"/>.</para>
        /// <para>Override this method to perform custom business or validation logic.</para>
        /// </remarks>
        /// <returns>An awaitable task.</returns>
        protected virtual Task OnValidateAsync()
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Invalidates this rule.
        /// </summary>
        /// <remarks>Invoke this method to invalidate this rule.</remarks>
        /// <param name="errorMessage">Sets the <see cref="ErrorMessage"/> value.</param>
        protected virtual void Invalidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsValid = false;
        }

        /// <summary>
        /// Invalidates this rule.
        /// </summary>
        /// <remarks>Invoke this method to invalidate this rule.</remarks>
        /// <param name="errorMessage">Sets the <see cref="ErrorMessage"/> value.</param>
        /// <param name="association">Sets the <see cref="Association"/> value.</param>
        protected virtual void Invalidate(string errorMessage, string association)
        {
            Association = association;
            Invalidate(errorMessage);
        }

        ///<inheritdoc cref="IRuleSuccessorsContainer{T}.GetSuccessors"/>
        public IEnumerable<IRuleSuccessor<IRule>> GetSuccessors()
        {
            return Successors;
        }

        /// <summary>
        /// Helper function that wraps a synchronous validation function into an expressive async api.
        /// </summary>
        /// <remarks>Use this method to improve the readability of your rule validation.</remarks>
        /// <param name="validationFunction">A function that returns a bool when invoked.</param>
        /// <returns>An <see cref="IfValidResponse"/>.</returns>
        protected IfValidResponse If(Func<bool> validationFunction)
        {
            return new IfValidResponse(validationFunction, Invalidate);
        }

        /// <summary>
        /// Helper function that wraps a synchronous validation function into an expressive async api.
        /// </summary>
        /// <remarks>Use this method to improve the readability of your rule validation.</remarks>
        /// <param name="validationFunction">A function that returns a bool when invoked.</param>
        /// <returns>An <see cref="IfNotValidResponse"/>.</returns>
        protected IfNotValidResponse IfNot(Func<bool> validationFunction)
        {
            return new IfNotValidResponse(validationFunction, Invalidate);
        }
    }

    /// <summary>
    /// Represents the result from an <see cref="RuleBase.If"/> operation.
    /// </summary>
    public class IfValidResponse
    {
        private Func<bool> _validationFunction;
        private Action<string> _invalidateFunction;

        /// <summary>
        /// Initializes a new instance of the IfValidResponse class with a function to perform validation against and a function that invalidates the rule.
        /// </summary>
        public IfValidResponse(Func<bool> validationFunction, Action<string> invalidateFunction)
        {
            _validationFunction = validationFunction;
            _invalidateFunction = invalidateFunction;
        }

        /// <summary>
        /// Invalidates this rule with the supplied error message if the validation function evaluates to <see langword="true"/>.
        /// </summary>
        /// <param name="errorMessage">Sets <see cref="RuleBase.ErrorMessage"/>.</param>
        public Task ThenInvalidateWith(string errorMessage)
        {
            if (_validationFunction() == true) _invalidateFunction(errorMessage);
            return Task.FromResult<object>(null);
        }
    }

    /// <summary>
    /// Represents the result from an <see cref="RuleBase.IfNot"/> operation.
    /// </summary>
    public class IfNotValidResponse
    {
        private Func<bool> _validationFunction;
        private Action<string> _invalidateFunction;

        /// <summary>
        /// Initializes a new instance of the IfNotValidResponse class with a function to perform validation against and a function that invalidates the rule.
        /// </summary>
        public IfNotValidResponse(Func<bool> validationFunction, Action<string> invalidateFunction)
        {
            _validationFunction = validationFunction;
            _invalidateFunction = invalidateFunction;
        }

        /// <summary>
        /// Invalidates this rule with the supplied error message if the validation function evaluates to <see langword="false"/>.
        /// </summary>
        /// <param name="errorMessage">Sets <see cref="RuleBase.ErrorMessage"/>.</param>
        public Task ThenInvalidateWith(string errorMessage)
        {
            if (_validationFunction() == false) _invalidateFunction(errorMessage);
            return Task.FromResult<object>(null);
        }
    }
}
