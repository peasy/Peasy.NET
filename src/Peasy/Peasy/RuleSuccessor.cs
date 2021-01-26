using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Peasy.Tests")]
namespace Peasy
{
    /// <summary>
    /// Represents a concrete implementation of IRuleSuccessor for internal use only
    /// </summary>
    internal class RuleSuccessor : List<IRule>, IRuleSuccessor
    {
        public RuleSuccessor(IEnumerable<IRule> rule)
        {
            this.AddRange(rule);
        }

        ///<inheritdoc cref="IRuleSuccessor.Rules"/>
        public IEnumerable<IRule> Rules
        {
            get { return this.AsEnumerable(); }
        }
    }
}
