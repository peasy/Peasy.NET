using Facile.Core;
using Facile.Core.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Facile.Core.Tests
{
    [TestClass]
    public class RuleBaseTests
    {
        [TestMethod]
        public void ValidRuleIsValidAfterValidation()
        {
            var rule = new TrueRule().Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ValidRuleDoesNotContainAnErrorMessageAfterValidation()
        {
            var rule = new TrueRule().Validate();
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void InvalidRuleIsInvalidAfterValidation()
        {
            var rule = new FalseRule1().Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void InvalidRuleContainsAnErrorMessageAfterValidation()
        {
            var rule = new FalseRule1().Validate();
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void ValidParentFailsWhenSuccessorFailsValidation()
        {
            var rule1 = new TrueRule().IfValidThenValidate(new FalseRule1()).Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void SuccessorDoesNotExecuteWhenParentFails()
        {
            var rule1 = new FalseRule1().IfValidThenValidate(new FalseRule2()).Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void AllRemainingSuccessorsSkipValidationWhenFirstSuccessorFails()
        {
            var rule1 = new TrueRule()
                                .IfValidThenValidate(new FalseRule1(), new FalseRule2(), new FalseRule3())
                                .Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void LastSuccessorValidatesWhenFirstSuccessorsPass()
        {
            var rule1 = new TrueRule()
                                .IfValidThenValidate(new TrueRule(), new TrueRule(), new FalseRule1())
                                .Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void LastSuccessorsInSuccessorChainAreSkippedWhenFirstSuccessorsFail()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new FalseRule1())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        } 

        [TestMethod]
        public void LastSuccessorInSuccessorChainIsSkippedWhenFirstSuccessorsPass()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule2 failed validation");
        } 

        [TestMethod]
        public void ParentFailsWhenLastSuccessorInChainsFailsValidation()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule3 failed validation");
        }

        [TestMethod]
        public void ThreeRuleChainExecutesSuccessfully()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new TrueRule())).Validate();

            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ThreeRuleChainFailSkipsThirdInChainWhenSecondFails()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new FalseRule1()
                                                      .IfValidThenValidate(new FalseRule2())).Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void ThreeRuleChainHitsThirdInChainAndFailsParent()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new FalseRule1())).Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void InvokesIfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                    .IfValidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void DoesNotInvokeIfValidThenExecute()
        {
            var output = string.Empty;
            new FalseRule1()
                    .IfValidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void InvokesIfInvalidThenExecute()
        {
            var output = string.Empty;
            new FalseRule1()
                    .IfInvalidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void DoesNotInvokeIfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule() 
                   .IfInvalidThenExecute(rule => output = "pass")
                   .Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void SuccessorInvokesExecuteIfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                    .IfValidThenValidate(new TrueRule()
                                                .IfValidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void SuccessorDoesNotInvokeExecuteIfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfValidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void SuccessorInvokesExecuteIfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfInvalidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void SuccessorDoesNotInvokeExecuteIfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new TrueRule()
                                         .IfInvalidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void FirstValidRuleInFirstSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void SecondValidRuleInFirstSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule().IfValidThenExecute(r => output = "pass"))
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void FirstValidRuleInSecondSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void SecondInvalidRuleInSecondSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3().IfInvalidThenExecute(r => output = "pass"))
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void FirstValidRuleInSecondSuccessorChainShouldExecuteButThirdFalseRuleShouldNot()
        {
            var output = string.Empty;
            var output2 = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new TrueRule())
                              .IfValidThenValidate(new FalseRule2().IfValidThenExecute(r => output2 = "pass"), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
            output2.ShouldBe(string.Empty);
            rule.ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }
    }

}
