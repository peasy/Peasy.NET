﻿using Shouldly;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Peasy.Core.Tests
{
    public class SynchronousRuleBaseTests
    {
        [Fact]
        public void Valid_Rule_Is_Valid_After_Validation()
        {
            var rule = new SynchronousTrueRule().Execute();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Valid_Rule_Does_Not_Contain_An_Error_Message_After_Validation()
        {
            var rule = new SynchronousTrueRule().Execute();
            rule.ErrorMessage.ShouldBe(null);
        }

        [Fact]
        public void Invalid_Rule_Is_Invalid_After_Validation()
        {
            var rule = new SynchronousFalseRule1().Execute();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Invalid_Rule_Contains_An_Error_Message_After_Validation()
        {
            var rule = new SynchronousFalseRule1().Execute();
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Valid_Parent_Fails_When_Successor_Fails_Validation()
        {
            var rule1 = new SynchronousTrueRule().IfValidThenValidate(new SynchronousFalseRule1()).Execute();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Successor_Does_Not_Execute_When_Parent_Fails()
        {
            var rule1 = new SynchronousFalseRule1().IfValidThenValidate(new SynchronousFalseRule2()).Execute();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void All_Remaining_Successors_Skip_Validation_When_First_Successor_Fails()
        {
            var rule1 = new SynchronousTrueRule()
                                .IfValidThenValidate(new SynchronousFalseRule1(), new SynchronousFalseRule2(), new SynchronousFalseRule3())
                                .Execute();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Last_Successor_Validates_When_First_Successors_Pass()
        {
            var rule1 = new SynchronousTrueRule()
                                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousTrueRule(), new SynchronousFalseRule1())
                                .Execute();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Last_Successors_In_Successor_Chain_Are_Skipped_When_First_Successors_Fail()
        {
            var rule = new SynchronousTrueRule()
                              .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRule1())
                              .IfValidThenValidate(new SynchronousFalseRule2(), new SynchronousFalseRule3())
                              .Execute();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Last_Successor_In_Successor_Chain_Is_Skipped_When_First_Successors_Pass()
        {
            var rule = new SynchronousTrueRule()
                              .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousTrueRule())
                              .IfValidThenValidate(new SynchronousFalseRule2(), new SynchronousFalseRule3())
                              .Execute();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule2 failed validation");
        }

        [Fact]
        public void Parent_Fails_When_Last_Successor_In_Chains_Fails_Validation()
        {
            var rule = new SynchronousTrueRule()
                              .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousTrueRule())
                              .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRule3())
                              .Execute();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule3 failed validation");
        }

        [Fact]
        public void Three_Rule_Chain_Executes_Successfully()
        {
            var rule = new SynchronousTrueRule()
                            .IfValidThenValidate(new SynchronousTrueRule()
                                                      .IfValidThenValidate(new SynchronousTrueRule())).Execute();

            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public void Three_Rule_Chain_Fail_Skips_Third_In_Chain_When_Second_Fails()
        {
            var rule = new SynchronousTrueRule()
                            .IfValidThenValidate(new SynchronousFalseRule1()
                                                      .IfValidThenValidate(new SynchronousFalseRule2())).Execute();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Three_Rule_Chain_Hits_Third_In_Chain_And_Fails_Parent()
        {
            var rule = new SynchronousTrueRule()
                            .IfValidThenValidate(new SynchronousTrueRule()
                                                      .IfValidThenValidate(new SynchronousFalseRule1())).Execute();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule1 failed validation");
        }

        [Fact]
        public void Invokes_IfValidThenExecute()
        {
            var output = string.Empty;
            new SynchronousTrueRule()
                    .IfValidThenInvoke(rule => output = "pass")
                    .Execute();

            output.ShouldBe("pass");
        }

        [Fact]
        public void Does_Not_Invoke_IfValidThenExecute()
        {
            var output = string.Empty;
            new SynchronousFalseRule1()
                    .IfValidThenInvoke(rule => output = "pass")
                    .Execute();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void Invokes_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new SynchronousFalseRule1()
                    .IfInvalidThenInvoke(rule => output = "pass")
                    .Execute();

            output.ShouldBe("pass");
        }

        [Fact]
        public void Does_Not_Invoke_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new SynchronousTrueRule()
                   .IfInvalidThenInvoke(rule => output = "pass")
                   .Execute();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void Successor_Invokes_IfValidThenExecute()
        {
            var output = string.Empty;
            new SynchronousTrueRule()
                    .IfValidThenValidate(new SynchronousTrueRule()
                                                .IfValidThenInvoke(r => output = "pass")).Execute();

            output.ShouldBe("pass");
        }

        [Fact]
        public void Successor_Does_Not_Invoke_IfValidThenExecute()
        {
            var output = string.Empty;
            new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousFalseRule1()
                                         .IfValidThenInvoke(r => output = "pass")).Execute();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void Successor_Invokes_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousFalseRule1()
                                         .IfInvalidThenInvoke(r => output = "pass")).Execute();

            output.ShouldBe("pass");
        }

        [Fact]
        public void Successor_Does_Not_Invoke_IfInvalidThenExecute()
        {
            var output = string.Empty;
            new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousTrueRule()
                                         .IfInvalidThenInvoke(r => output = "pass")).Execute();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public void First_Valid_Rule_In_First_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new SynchronousTrueRule()
                          .IfValidThenValidate(new SynchronousTrueRule().IfValidThenInvoke(r => output = "pass"), new SynchronousTrueRule())
                          .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRule3())
                          .Execute();
            output.ShouldBe("pass");
        }

        [Fact]
        public void Second_Valid_Rule_In_First_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousTrueRule().IfValidThenInvoke(r => output = "pass"))
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRule3())
                .Execute();
            output.ShouldBe("pass");
        }

        [Fact]
        public void First_Valid_Rule_In_Second_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousTrueRule())
                .IfValidThenValidate(new SynchronousTrueRule().IfValidThenInvoke(r => output = "pass"), new SynchronousFalseRule3())
                .Execute();
            output.ShouldBe("pass");
        }

        [Fact]
        public void Second_Invalid_Rule_In_Second_Successor_Chain_Should_Execute()
        {
            var output = string.Empty;
            var rule = new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousTrueRule())
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRule3().IfInvalidThenInvoke(r => output = "pass"))
                .Execute();
            output.ShouldBe("pass");
        }

        [Fact]
        public void First_Valid_Rule_In_Second_Successor_Chain_Should_Execute_But_Third_False_Rule_Should_Not()
        {
            var output = string.Empty;
            var output2 = string.Empty;
            var rule = new SynchronousTrueRule()
                              .IfValidThenValidate(new SynchronousTrueRule().IfValidThenInvoke(r => output = "pass"), new SynchronousTrueRule())
                              .IfValidThenValidate(new SynchronousFalseRule2().IfValidThenInvoke(r => output2 = "pass"), new SynchronousFalseRule3())
                              .Execute();
            output.ShouldBe("pass");
            output2.ShouldBe(string.Empty);
            rule.ErrorMessage.ShouldBe("SynchronousFalseRule2 failed validation");
        }

        [Fact]
        public void The_Correct_Association_Is_Set_As_A_Result_Of_Failed_Successor()
        {
            var rule = new SynchronousTrueRule("Foo")
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRuleWithAssociation("Address"));
            rule.Execute();
            rule.Association.ShouldBe("Address");
            rule.ErrorMessage.ShouldBe("Address failed validation");
        }

        [Fact]
        public void Allows_access_to_successor_rules_via_IRulesContainer_interface()
        {
            var rule = new SynchronousTrueRule()
                .IfValidThenValidate(new SynchronousTrueRule(), new SynchronousFalseRule2())
                .IfValidThenValidate
                (
                    new SynchronousTrueRule().IfValidThenValidate(new SynchronousFalseRule1()),
                    new SynchronousFalseRule3()
                );

            rule.GetSuccessors().Count().ShouldBe(2);

            var firstSuccessor = rule.GetSuccessors().First();
            firstSuccessor.Rules.Count().ShouldBe(2);
            firstSuccessor.Rules.First().ShouldBeOfType<SynchronousTrueRule>();
            firstSuccessor.Rules.Second().ShouldBeOfType<SynchronousFalseRule2>();

            var secondSuccessor = rule.GetSuccessors().Second();
            secondSuccessor.Rules.Count().ShouldBe(2);
            secondSuccessor.Rules.First().ShouldBeOfType<SynchronousTrueRule>();
            secondSuccessor.Rules.First().GetSuccessors().Count().ShouldBe(1);
            secondSuccessor.Rules.First().GetSuccessors().First().Rules.First().ShouldBeOfType<SynchronousFalseRule1>();
            secondSuccessor.Rules.Second().ShouldBeOfType<SynchronousFalseRule3>();
        }
    }

}
