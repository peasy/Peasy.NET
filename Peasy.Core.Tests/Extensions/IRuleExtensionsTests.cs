using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Peasy.Core.Tests.Extensions
{
    [TestClass]
    public class IRuleExtensionsTests
    {
        [TestMethod]
        public void GetBusinessRulesResultsReturnsOneValidationResult()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };
            rules.GetValidationResults().Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetBusinessRulesResultsReturnsTwoValidationResults()
        {
            var rules = new List<IRule>
            {
                new FalseRule1(), new FalseRule2(), new TrueRule()
            };
            rules.GetValidationResults().Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetBusinessRulesResultsReturnsThreeValidationResults()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new FalseRule2(), new TrueRule(), new FalseRule3()
            };
            rules.GetValidationResults().Count().ShouldBe(3);
        }

        [TestMethod]
        public void GetBusinessRulesResultsReturnsOneValidationResultWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new TrueRule()
            };
            rules.GetValidationResults().Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetBusinessRulesResultsReturnsTwoValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new FalseRule2()
            };
            rules.GetValidationResults().Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetBusinessRulesResultsReturnsThreeValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()),
                new TrueRule().IfValidThenValidate(new FalseRule2()),
                new FalseRule3()
            };
            rules.GetValidationResults().Count().ShouldBe(3);
        }

        [TestMethod]
        public void GetBusinessRulesResultsReturnsTwoValidationResultsWithCorrectMessages()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResults();
            results.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            results.Last().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [TestMethod]
        public void GetBusinessRulesResultsSetsValidationResultMemberNameToEmptyString()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResults();
            results.First().MemberNames.First().ShouldBe(string.Empty);
            results.Last().MemberNames.First().ShouldBe(string.Empty);
        }

        [TestMethod]
        public void GetBusinessRulesResultsSetsValidationResultMemberName()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResults("MyEntity");
            results.First().MemberNames.First().ShouldBe("MyEntity");
            results.Last().MemberNames.First().ShouldBe("MyEntity");
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsOneValidationResult()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsTwoValidationResults()
        {
            var rules = new List<IRule>
            {
                new FalseRule1(), new FalseRule2(), new TrueRule()
            };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsThreeValidationResults()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new FalseRule2(), new TrueRule(), new FalseRule3()
            };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(3);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsOneValidationResultWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new TrueRule()
            };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsTwoValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new FalseRule2()
            };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsThreeValidationResultsWithNestedRules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()),
                new TrueRule().IfValidThenValidate(new FalseRule2()),
                new FalseRule3()
            };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.Count().ShouldBe(3);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncReturnsTwoValidationResultsWithCorrectMessages()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            results.Result.Last().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncSetsValidationResultMemberNameToEmptyString()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResultsAsync();
            results.Wait();
            results.Result.First().MemberNames.First().ShouldBe(string.Empty);
            results.Result.Last().MemberNames.First().ShouldBe(string.Empty);
        }

        [TestMethod]
        public void GetBusinessRulesResultsAsyncSetsValidationResultMemberName()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResultsAsync("MyEntity");
            results.Wait();
            results.Result.First().MemberNames.First().ShouldBe("MyEntity");
            results.Result.Last().MemberNames.First().ShouldBe("MyEntity");
        }

        [TestMethod]
        public void IfAllValidateThenValidateValidatesOnSuccess()
        {
            var number = 0;
            var rules = new List<IRule> { new TrueRule(), new TrueRule() };
            var rule = rules.IfAllValidThenValidate(new TrueRule().IfValidThenExecute((r) => number = 42));
            rule.Validate();
            number.ShouldBe(42);
        }

        [TestMethod]
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
