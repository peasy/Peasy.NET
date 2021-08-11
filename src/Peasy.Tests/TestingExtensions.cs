using System;
using System.Collections.Generic;
using System.Linq;

namespace Peasy.Core.Tests
{
    public static class TestingExtensions
    {
        public static IEnumerable<IRuleSuccessor<T>> GetSuccessors<T>(this T container)
        {
            return (container as IRuleSuccessorsContainer<T>).GetSuccessors();
        }

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

        public static T Fourth<T>(this IEnumerable<T> rules)
        {
            return rules.ElementAt(3);
        }

        public static IRuleSuccessor<T> Fourth<T>(this IEnumerable<IRuleSuccessor<T>> rules)
        {
            return rules.ElementAt(3);
        }

        public static void Times(this int value, Action action)
        {
            for (var counter = 0; counter < value; counter++)
            {
                action();
            }
        }

        public static void Times(this int value, Action<int> action)
        {
            for (var counter = 0; counter < value; counter++)
            {
                action(counter);
            }
        }

        public static IEnumerable<T> Times<T>(this int value, Func<int, T> func)
        {
            for (var counter = 0; counter < value; counter++)
            {
                yield return func(counter);
            }
        }
    }

}
