using Facile.Core;
using Facile.Core.Tests;
using Shouldly;
using Xunit;

namespace Facile.Tests.Rules
{
    [Trait("Rules", "RuleBase")]
    public class RuleBaseTests
    {
        [Fact]
        public void ValidRuleIsValidAfterValidation()
        {
            var rule = new TrueRule().Validate();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void ValidRuleDoesNotContainAnErrorMessageAfterValidation()
        {
            var rule = new TrueRule().Validate();
            rule.ErrorMessage.ShouldBe(null);
        }

        [Fact]
        public void InvalidRuleIsInvalidAfterValidation()
        {
            var rule = new FalseRule1().Validate();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void InvalidRuleContainsAnErrorMessageAfterValidation()
        {
            var rule = new FalseRule1().Validate();
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void ValidParentFailsWhenSuccessorFailsValidation()
        {
            var rule1 = new TrueRule().IfValidThenValidate(new FalseRule1()).Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void SuccessorDoesNotExecuteWhenParentFails()
        {
            var rule1 = new FalseRule1().IfValidThenValidate(new FalseRule2()).Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void AllRemainingSuccessorsSkipValidationWhenFirstSuccessorFails()
        {
            var rule1 = new TrueRule()
                                .IfValidThenValidate(new FalseRule1(), new FalseRule2(), new FalseRule3())
                                .Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void LastSuccessorValidatesWhenFirstSuccessorsPass()
        {
            var rule1 = new TrueRule()
                                .IfValidThenValidate(new TrueRule(), new TrueRule(), new FalseRule1())
                                .Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void LastSuccessorsInSuccessorChainAreSkippedWhenFirstSuccessorsFail()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new FalseRule1())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        } 

        [Fact]
        public void LastSuccessorInSuccessorChainIsSkippedWhenFirstSuccessorsPass()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule2 failed validation");
        } 

        [Fact]
        public void ParentFailsWhenLastSuccessorInChainsFailsValidation()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule3 failed validation");
        }

        [Fact]
        public void ThreeRuleChainExecutesSuccessfully()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new TrueRule())).Validate();

            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void ThreeRuleChainFailSkipsThirdInChainWhenSecondFails()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new FalseRule1()
                                                      .IfValidThenValidate(new FalseRule2())).Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void ThreeRuleChainHitsThirdInChainAndFailsParent()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new FalseRule1())).Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void InvokesIfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                    .IfValidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe("pass");
        }

        [Fact]
        public void DoesNotInvokeIfValidThenExecute()
        {
            var output = string.Empty;
            new FalseRule1()
                    .IfValidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void InvokesIfInvalidThenExecute()
        {
            var output = string.Empty;
            new FalseRule1()
                    .IfInvalidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe("pass");
        }

        [Fact]
        public void DoesNotInvokeIfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule() 
                   .IfInvalidThenExecute(rule => output = "pass")
                   .Validate();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void SuccessorInvokesExecuteIfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                    .IfValidThenValidate(new TrueRule()
                                                .IfValidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe("pass");
        }

        [Fact]
        public void SuccessorDoesNotInvokeExecuteIfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfValidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void SuccessorInvokesExecuteIfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfInvalidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe("pass");
        }

        [Fact]
        public void SuccessorDoesNotInvokeExecuteIfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new TrueRule()
                                         .IfInvalidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void FirstValidRuleInFirstSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [Fact]
        public void SecondValidRuleInFirstSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule().IfValidThenExecute(r => output = "pass"))
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [Fact]
        public void FirstValidRuleInSecondSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [Fact]
        public void SecondInvalidRuleInSecondSuccessorChainShouldExecute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3().IfInvalidThenExecute(r => output = "pass"))
                              .Validate();
            output.ShouldBe("pass");
        }

        [Fact]
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
