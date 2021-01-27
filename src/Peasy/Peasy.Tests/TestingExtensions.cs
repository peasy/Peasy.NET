using System.Collections.Generic;
using System.Linq;
using Peasy.Synchronous;

namespace Peasy.Core.Tests
{
    public static class TestingExtensions
    {
        public static IEnumerable<IRuleSuccessor<T>> GetSuccessors<T>(this T container)
        {
            return (container as IRuleSuccessorsContainer<T>).GetSuccessors();
        }

        // public static IEnumerable<IRuleSuccessor<IRule>> GetSuccessors(this IRule container)
        // {
        //     return (container as IRuleSuccessorsContainer<IRule>).GetSuccessors();
        // }

        // public static IEnumerable<IRuleSuccessor<ISynchronousRule>> GetSuccessors(this ISynchronousRule container)
        // {
        //     return (container as IRuleSuccessorsContainer<ISynchronousRule>).GetSuccessors();
        // }

        public static T Second<T>(this IEnumerable<T> rules)
        {
            return rules.ElementAt(1);
        }

        public static IRuleSuccessor<T> Second<T>(this IEnumerable<IRuleSuccessor<T>> rules)
        {
            return rules.ElementAt(1);
        }

        public static T Third<T>(this IEnumerable<T> rules)
        {
            return rules.ElementAt(2);
        }

        public static IRuleSuccessor<T> Third<T>(this IEnumerable<IRuleSuccessor<T>> rules)
        {
            return rules.ElementAt(2);
        }
    }

}
