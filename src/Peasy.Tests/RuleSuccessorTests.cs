using Peasy.Synchronous;
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

            var successor = new RuleSuccessor<IRule>(rules);
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

            var successor = new RuleSuccessor<IRule>(rules);
            var results = successor.Select(r => r);

            results.Count().ShouldBe(3);
        }

        [Fact]
        public void Intializes_With_Synchronous_Rules_Appropriately()
        {
            var rules = new ISynchronousRule[]
            {
                new SynchronousTrueRule(), new SynchronousFalseRule1(), new SynchronousTrueRule()
            };

            var successor = new RuleSuccessor<ISynchronousRule>(rules);
            successor.Rules.Count().ShouldBe(3);
            successor.Rules.First().ShouldBeOfType<SynchronousTrueRule>();
            successor.Rules.Second().ShouldBeOfType<SynchronousFalseRule1>();
            successor.Rules.Third().ShouldBeOfType<SynchronousTrueRule>();
        }

        [Fact]
        public void SupportsProperEnumerator_Synchronous()
        {
            var rules = new ISynchronousRule[]
            {
                new SynchronousTrueRule(), new SynchronousFalseRule1(), new SynchronousTrueRule()
            };

            var successor = new RuleSuccessor<ISynchronousRule>(rules);
            var results = successor.Select(r => r);

            results.Count().ShouldBe(3);
        }
    }

}
