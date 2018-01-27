using Peasy.Core;
using Peasy.Core.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Peasy.Core.Tests
{
    [TestClass]
    public class RuleBaseTests
    {
        [TestMethod]
        public void Valid_Rule_Is_Valid_After_Validation()
        {
            var rule = new TrueRule().Validate();
            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Valid_Rule_Does_Not_Contain_An_Error_Message_After_Validation()
        {
            var rule = new TrueRule().Validate();
            rule.ErrorMessage.ShouldBe(null);
        }

        [TestMethod]
        public void Invalid_Rule_Is_Invalid_After_Validation()
        {
            var rule = new FalseRule1().Validate();
            rule.IsValid.ShouldBe(false);
        }

        [TestMethod]
        public void Invalid_Rule_Contains_An_Error_Message_After_Validation()
        {
            var rule = new FalseRule1().Validate();
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void Valid_Parent_Fails_When_Successor_Fails_Validation()
        {
            var rule1 = new TrueRule().IfValidThenValidate(new FalseRule1()).Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void Successor_Does_Not_Execute_When_Parent_Fails()
        {
            var rule1 = new FalseRule1().IfValidThenValidate(new FalseRule2()).Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void All_Remaining_Successors_Skip_Validation_When_First_Successor_Fails()
        {
            var rule1 = new TrueRule()
                                .IfValidThenValidate(new FalseRule1(), new FalseRule2(), new FalseRule3())
                                .Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void Last_Successor_Validates_When_First_Successors_Pass()
        {
            var rule1 = new TrueRule()
                                .IfValidThenValidate(new TrueRule(), new TrueRule(), new FalseRule1())
                                .Validate();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void Last_Successors_In_Successor_Chain_Are_Skipped_When_First_Successors_Fail()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new FalseRule1())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        } 

        [TestMethod]
        public void Last_Successor_In_Successor_Chain_Is_Skipped_When_First_Successors_Pass()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule2 failed validation");
        } 

        [TestMethod]
        public void Parent_Fails_When_Last_Successor_In_Chains_Fails_Validation()
        {
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule3 failed validation");
        }

        [TestMethod]
        public void Three_Rule_Chain_Executes_Successfully()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new TrueRule())).Validate();

            rule.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void Three_Rule_Chain_Fail_Skips_Third_In_Chain_When_Second_Fails()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new FalseRule1()
                                                      .IfValidThenValidate(new FalseRule2())).Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void Three_Rule_Chain_Hits_Third_In_Chain_And_Fails_Parent()
        {
            var rule = new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new FalseRule1())).Validate();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public void Invokes_IfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                    .IfValidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void Does_Not_Invoke_IfValidThenExecute()
        {
            var output = string.Empty;
            new FalseRule1()
                    .IfValidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void Invokes_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new FalseRule1()
                    .IfInvalidThenExecute(rule => output = "pass")
                    .Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void Does_Not_Invoke_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule() 
                   .IfInvalidThenExecute(rule => output = "pass")
                   .Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void Successor_Invokes_IfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                    .IfValidThenValidate(new TrueRule()
                                                .IfValidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void Successor_Does_Not_Invoke_IfValidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfValidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void Successor_Invokes_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfInvalidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe("pass");
        }

        [TestMethod]
        public void Successor_Does_Not_Invoke_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new TrueRule()
                .IfValidThenValidate(new TrueRule()
                                         .IfInvalidThenExecute(r => output = "pass")).Validate();

            output.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void First_Valid_Rule_In_First_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void Second_Valid_Rule_In_First_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule().IfValidThenExecute(r => output = "pass"))
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void First_Valid_Rule_In_Second_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule().IfValidThenExecute(r => output = "pass"), new FalseRule3())
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void Second_Invalid_Rule_In_Second_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3().IfInvalidThenExecute(r => output = "pass"))
                              .Validate();
            output.ShouldBe("pass");
        }

        [TestMethod]
        public void First_Valid_Rule_In_Second_Successor_Chain_Should_Execute_But_Third_False_Rule_Should_Not()
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
       
        [TestMethod]
        public void The_Correct_Association_Is_Set_As_A_Result_Of_Failed_Successor()
        {
            var rule = new TrueRule("Foo")
                .IfValidThenValidate(new TrueRule(), new FalseRuleWithAssociation("Address"));
            rule.Validate();
            rule.Association.ShouldBe("Address");
            rule.ErrorMessage.ShouldBe("Address failed validation");
        }
    }

}
