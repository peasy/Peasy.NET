using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Gets or sets a string that associates this rule with a field. This is helpful for validation errors
        /// </summary>
        string Association { get; }

        /// <summary>
        /// Gets or sets the message to be supplied to caller in the event that no rule dependencies exist via IfValidThenValidate()
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this rule is valid.
        /// </summary>
        /// <value>
        /// <c>True</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }

        /// <summary>
        /// Asynchronously executes this rule.
        /// </summary>
        Task<IRule> ExecuteAsync();
    }
}
