using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy.Core.Tests.Extensions
{
    [TestClass]
    public class IRuleExtensionsTests
    {
        [TestMethod]
        public void GetValidationResults_Returns_One_Validation_Result()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };
            rules.GetValidationResults().Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetValidationResults_IRule_Returns_One_Validation_Result()
        {
            var rule = new TrueRule().IfValidThenValidate
            (
                new TrueRule(), new FalseRule1(), new TrueRule()
            );
            rule.GetValidationResults().Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetValidationResults_Returns_Two_Validation_Results()
        {
            var rules = new List<IRule>
            {
                new FalseRule1(), new FalseRule2(), new TrueRule()
            };
            rules.GetValidationResults().Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetValidationResults_Returns_Three_Validation_Results()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new FalseRule2(), new TrueRule(), new FalseRule3()
            };
            rules.GetValidationResults().Count().ShouldBe(3);
        }

        [TestMethod]
        public void GetValidationResults_Returns_One_Validation_Result_When_Nested_Rule_Fails()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new TrueRule()
            };
            rules.GetValidationResults().Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetValidationResults_Returns_Two_Validation_Results_When_Nested_Rule_Fails()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new FalseRule2()
            };
            rules.GetValidationResults().Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetValidationResults_Returns_Three_Validation_Results_Nested_Rules_Fail()
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
        public void GetValidationResults_Returns_Two_Validation_Results_With_Correct_Messages()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResults();
            results.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            results.Last().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [TestMethod]
        public void GetValidationResults_Sets_Validation_Result_Member_Name_To_Empty_String()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResults();
            results.First().MemberNames.First().ShouldBe(string.Empty);
            results.Last().MemberNames.First().ShouldBe(string.Empty);
        }

        [TestMethod]
        public void GetValidationResults_Sets_Validation_Result_Member_Name_Explicitly()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = rules.GetValidationResults("MyEntity");
            results.First().MemberNames.First().ShouldBe("MyEntity");
            results.Last().MemberNames.First().ShouldBe("MyEntity");
        }

        [TestMethod]
        public void GetValidationResults_Sets_Validation_Result_Member_Name_From_Association_Property()
        {
            var rules = new List<IRule> { new FalseRuleWithAssociation("Name"), new FalseRuleWithAssociation("Address") };
            var results = rules.GetValidationResults().ToArray();
            results.First().MemberNames.First().ShouldBe("Name");
            results.Last().MemberNames.First().ShouldBe("Address");
        }

        [TestMethod]
        public void GetValidationResults_Returns_Association_Property_With_Nested_Rules()
        {
            var ruleWithAssociation = new FalseRuleWithAssociation("Address");
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(ruleWithAssociation), new TrueRule()
            };

            var results = rules.GetValidationResults();
            results.First().MemberNames.First().ShouldBe("Address");
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_One_Validation_Result()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new TrueRule()
            };
            var results = await rules.GetValidationResultsAsync();
            results.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_IRule_Returns_One_Validation_Result()
        {
            var rule = new TrueRule().IfValidThenValidate
            (
                new TrueRule(), new FalseRule1(), new TrueRule()
            );
            var results = await rule.GetValidationResultsAsync();
            results.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_Two_Validation_Results()
        {
            var rules = new List<IRule>
            {
                new FalseRule1(), new FalseRule2(), new TrueRule()
            };
            var results = await rules.GetValidationResultsAsync();
            results.Count().ShouldBe(2);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_Three_Validation_Results()
        {
            var rules = new List<IRule>
            {
                new TrueRule(), new FalseRule1(), new FalseRule2(), new TrueRule(), new FalseRule3()
            };
            var results = await rules.GetValidationResultsAsync();
            results.Count().ShouldBe(3);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_One_Validation_Result_With_Nested_Rules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new TrueRule()
            };
            var results = await rules.GetValidationResultsAsync();
            results.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_Two_Validation_Results_With_Nested_Rules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()), new FalseRule2()
            };
            var results = await rules.GetValidationResultsAsync();
            results.Count().ShouldBe(2);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_Three_Validation_Results_With_Nested_Rules()
        {
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(new FalseRule1()),
                new TrueRule().IfValidThenValidate(new FalseRule2()),
                new FalseRule3()
            };
            var results = await rules.GetValidationResultsAsync();
            results.Count().ShouldBe(3);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_Two_Validation_Results_With_Correct_Messages()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = await rules.GetValidationResultsAsync();
            results.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            results.Last().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Sets_Validation_Result_Member_Name_To_Empty_String()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = await rules.GetValidationResultsAsync();
            results.First().MemberNames.First().ShouldBe(string.Empty);
            results.Last().MemberNames.First().ShouldBe(string.Empty);
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Sets_Validation_Result_Member_Name()
        {
            var rules = new List<IRule> { new FalseRule1(), new FalseRule2() };
            var results = await rules.GetValidationResultsAsync("MyEntity");
            results.First().MemberNames.First().ShouldBe("MyEntity");
            results.Last().MemberNames.First().ShouldBe("MyEntity");
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Sets_Validation_Result_Member_Name_From_Association_Property()
        {
            var rules = new List<IRule> { new FalseRuleWithAssociation("Name"), new FalseRuleWithAssociation("Address") };
            var results = (await rules.GetValidationResultsAsync()).ToArray();
            results.First().MemberNames.First().ShouldBe("Name");
            results.Last().MemberNames.First().ShouldBe("Address");
        }

        [TestMethod]
        public async Task GetValidationResultsAsync_Returns_Association_Property_With_Nested_Rules()
        {
            var ruleWithAssociation = new FalseRuleWithAssociation("Address");
            var rules = new List<IRule>
            {
                new TrueRule().IfValidThenValidate(ruleWithAssociation), new TrueRule()
            };

            var results = await rules.GetValidationResultsAsync();
            results.First().MemberNames.First().ShouldBe("Address");
        }

        [TestMethod]
        public void IfAllValidateThenValidate_Validates_On_Success()
        {
            var number = 0;
            var rules = new List<IRule> { new TrueRule(), new TrueRule() };
            var rule = rules.IfAllValidThenValidate(new TrueRule().IfValidThenExecute((r) => number = 42));
            rule.Validate();
            number.ShouldBe(42);
        }

        [TestMethod]
        public void IfAllValidateThenValidate_Does_Not_Validate_On_Failure()
        {
            var number = 0;
            var rules = new List<IRule> { new TrueRule(), new FalseRule1() };
            var rule = rules.IfAllValidThenValidate(new TrueRule().IfValidThenExecute((r) => number = 42));
            rule.Validate();
            number.ShouldBe(0);
        }
    }
}
