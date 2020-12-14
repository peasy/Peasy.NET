using Shouldly;
using System.Linq;
using Xunit;

namespace Peasy.Core.Tests
{
    public class RuleSuccessorTests
    {
        [Fact]
        public void Intializes_With_Rules_Appropriately()
        {
            var rules = new IRule[]
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };

            var successor = new RuleSuccessor(rules);
            successor.Rules.Count().ShouldBe(3);
            successor.Rules.First().ShouldBeOfType<TrueRule>();
            successor.Rules.Second().ShouldBeOfType<FalseRule1>();
            successor.Rules.Third().ShouldBeOfType<TrueRule>();
        }

        [Fact]
        public void SupportsProperEnumerator()
        {
            var rules = new IRule[]
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };

            var successor = new RuleSuccessor(rules);
            var results = successor.Select(r => r);

            results.Count().ShouldBe(3);
        }
    }

}
