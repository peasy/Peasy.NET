using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Facile.Core.Tests.Extensions
{
    [Trait("Rules", "IRuleExtensionTests")]
    public class IRuleExtensionsTests
    {
        [Fact]
        public void GetBusinessRulesResultsReturnsOneValidationResult()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };
            rules.GetBusinessRulesResults().Count().ShouldBe(1);
        }

        [Fact]
        public void GetBusinessRulesResultsReturnsTwoValidationResults()
        {
            var rules = new List<IRule>
            {
                new FalseRule1(), new FalseRule2(), new TrueRule()
            };
            rules.GetBusinessRulesResults().Count().ShouldBe(2);
        }

        [Fact]
        public void GetBusinessRulesResultsReturnsThreeValidationResults()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new FalseRule2(), new TrueRule(), new FalseRule3()
            };
            rules.GetBusinessRulesResults().Count().ShouldBe(3);
        }

        [Fact]
        public void GetBusinessRulesResultsReturnsOneValidationResultWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new TrueRule()
            };
            rules.GetBusinessRulesResults().Count().ShouldBe(1);
        }

        [Fact]
        public void GetBusinessRulesResultsReturnsTwoValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new FalseRule2()
            };
            rules.GetBusinessRulesResults().Count().ShouldBe(2);
        }

        [Fact]
        public void GetBusinessRulesResultsReturnsThreeValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()),
                new TrueRule().IfValidThenValidate(new FalseRule2()),
                new FalseRule3()
            };
            rules.GetBusinessRulesResults().Count().ShouldBe(3);
        }

        [Fact]
        public void GetBusinessRulesResultsReturnsTwoValidationResultsWithCorrectMessages()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetBusinessRulesResults();
            results.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            results.Last().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [Fact]
        public void GetBusinessRulesResultsSetsValidationResultMemberNameToEmptyString()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetBusinessRulesResults();
            results.First().MemberNames.First().ShouldBe(string.Empty);
            results.Last().MemberNames.First().ShouldBe(string.Empty);
        }

        [Fact]
        public void GetBusinessRulesResultsSetsValidationResultMemberName()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetBusinessRulesResults("MyEntity");
            results.First().MemberNames.First().ShouldBe("MyEntity");
            results.Last().MemberNames.First().ShouldBe("MyEntity");
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsOneValidationResult()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(1);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsTwoValidationResults()
        {
            var rules = new List<IRule>
            {
                new FalseRule1(), new FalseRule2(), new TrueRule()
            };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(2);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsThreeValidationResults()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new FalseRule2(), new TrueRule(), new FalseRule3()
            };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(3);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsOneValidationResultWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new TrueRule()
            };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(1);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsTwoValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new FalseRule2()
            };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(2);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsThreeValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()),
                new TrueRule().IfValidThenValidate(new FalseRule2()),
                new FalseRule3()
            };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(3);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncReturnsTwoValidationResultsWithCorrectMessages()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            results.Result.Last().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncSetsValidationResultMemberNameToEmptyString()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetBusinessRulesResultsAsync();
            results.Wait();
            results.Result.First().MemberNames.First().ShouldBe(string.Empty);
            results.Result.Last().MemberNames.First().ShouldBe(string.Empty);
        }

        [Fact]
        public void GetBusinessRulesResultsAsyncSetsValidationResultMemberName()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetBusinessRulesResultsAsync("MyEntity");
            results.Wait();
            results.Result.First().MemberNames.First().ShouldBe("MyEntity");
            results.Result.Last().MemberNames.First().ShouldBe("MyEntity");
        }

        [Fact]
        public void IfAllValidateThenValidateValidatesOnSuccess()
        {
            var number = 0;
            var rules = new List<IRule> { new TrueRule(), new TrueRule() };
            var rule = rules.IfAllValidThenValidate(new TrueRule().IfValidThenExecute((r) => number = 42));
            rule.Validate();
            number.ShouldBe(42);
        }

        [Fact]
        public void IfAllValidateThenValidateDoesNotValidateOnFailure()
        {
            var number = 0;
            var rules = new List<IRule> { new TrueRule(), new FalseRule1() };
            var rule = rules.IfAllValidThenValidate(new TrueRule().IfValidThenExecute((r) => number = 42));
            rule.Validate();
            number.ShouldBe(0);
        }
    }
}
