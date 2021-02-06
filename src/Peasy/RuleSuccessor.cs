using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Peasy.Tests")]
namespace Peasy
{
    /// <summary>
    /// Represents a concrete implementation of IRuleSuccessor for internal use only
    /// </summary>
    internal class RuleSuccessor<T> : List<T>, IRuleSuccessor<T>
    {
        public RuleSuccessor(IEnumerable<T> rule)
        {
            this.AddRange(rule);
        }

        ///<inheritdoc cref="IRuleSuccessor{T}.Rules"/>
        public IEnumerable<T> Rules
        {
            get { return this.AsEnumerable(); }
        }
    }
}
