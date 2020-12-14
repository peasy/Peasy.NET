using System.Collections.Generic;
using System.Linq;

namespace Peasy.Core.Tests
{
    public static class TestingExtensions
    {
        public static IEnumerable<IRuleSuccessor> GetSuccessors(this IRule container)
        {
            return (container as IRuleSuccessorsContainer).GetSuccessors();
        }

        public static IRule Second(this IEnumerable<IRule> rules)
        {
            return rules.ElementAt(1);
        }

        public static IRuleSuccessor Second(this IEnumerable<IRuleSuccessor> rules)
        {
            return rules.ElementAt(1);
        }

        public static IRule Third(this IEnumerable<IRule> rules)
        {
            return rules.ElementAt(2);
        }

        public static IRuleSuccessor Third(this IEnumerable<IRuleSuccessor> rules)
        {
            return rules.ElementAt(2);
        }
    }

}
