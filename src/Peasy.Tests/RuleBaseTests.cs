﻿﻿using Shouldly;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace Peasy.Core.Tests
{
    public class RuleBaseTests
    {
        [Fact]
        public async Task Valid_Rule_Is_Valid_After_Validation_Async()
        {
            var rule = await new TrueRule().ExecuteAsync();
            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Valid_Rule_Does_Not_Contain_An_Error_Message_After_Validation_Async()
        {
            var rule = await new TrueRule().ExecuteAsync();
            rule.ErrorMessage.ShouldBe(null);
        }

        [Fact]
        public async Task Invalid_Rule_Is_Invalid_After_Validation_Async()
        {
            var rule = await new FalseRule1().ExecuteAsync();
            rule.IsValid.ShouldBe(false);
        }

        [Fact]
        public async Task Invalid_Rule_Contains_An_Error_Message_After_Validation_Async()
        {
            var rule = await new FalseRule1().ExecuteAsync();
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Valid_Parent_Fails_When_Successor_Fails_Validation_Async()
        {
            var rule1 = await new TrueRule().IfValidThenValidate(new FalseRule1()).ExecuteAsync();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Successor_Does_Not_Execute_When_Parent_Fails_Async()
        {
            var rule1 = await new FalseRule1().IfValidThenValidate(new FalseRule2()).ExecuteAsync();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task All_Remaining_Successors_Skip_Validation_When_First_Successor_Fails_Async()
        {
            var rule1 = await new TrueRule()
                                .IfValidThenValidate(new FalseRule1(), new FalseRule2(), new FalseRule3())
                                .ExecuteAsync();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Last_Successor_Validates_When_First_Successors_Pass_Async()
        {
            var rule1 = await new TrueRule()
                                .IfValidThenValidate(new TrueRule(), new TrueRule(), new FalseRule1())
                                .ExecuteAsync();
            rule1.IsValid.ShouldBe(false);
            rule1.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Last_Successors_In_Successor_Chain_Are_Skipped_When_First_Successors_Fail_Async()
        {
            var rule = await new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new FalseRule1())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .ExecuteAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Last_Successor_In_Successor_Chain_Is_Skipped_When_First_Successors_Pass_Async()
        {
            var rule = await new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new FalseRule2(), new FalseRule3())
                              .ExecuteAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [Fact]
        public async Task Parent_Fails_When_Last_Successor_In_Chains_Fails_Validation_Async()
        {
            var rule = await new TrueRule()
                              .IfValidThenValidate(new TrueRule(), new TrueRule())
                              .IfValidThenValidate(new TrueRule(), new FalseRule3())
                              .ExecuteAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule3 failed validation");
        }

        [Fact]
        public async Task Three_Rule_Chain_Executes_Successfully_Async()
        {
            var rule = await new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new TrueRule())).ExecuteAsync();

            rule.IsValid.ShouldBe(true);
        }

        [Fact]
        public async Task Three_Rule_Chain_Fail_Skips_Third_In_Chain_When_Second_Fails_Async()
        {
            var rule = await new TrueRule()
                            .IfValidThenValidate(new FalseRule1()
                                                      .IfValidThenValidate(new FalseRule2())).ExecuteAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Three_Rule_Chain_Hits_Third_In_Chain_And_Fails_Parent_Async()
        {
            var rule = await new TrueRule()
                            .IfValidThenValidate(new TrueRule()
                                                      .IfValidThenValidate(new FalseRule1())).ExecuteAsync();
            rule.IsValid.ShouldBe(false);
            rule.ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Async_Before_Successor_Validation()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenValidate(new TrueRule(output).IfValidThenInvoke(async rule => output += "2"))
                    .IfValidThenInvoke(async rule => output += "1")
                    .ExecuteAsync();

            output.ShouldBe("12");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Async_Before_Successor_Validation_Multiple()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "2")
                            .IfValidThenValidate(
                                new TrueRule()
                                    .IfValidThenInvoke(async rule => output += "3"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("123");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Async_Before_Successor_Validation_Multiple_II()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "2")
                            .IfValidThenValidate(
                                new TrueRule()
                                    .IfValidThenInvoke(async rule => output += "3"),
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "4"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("1234");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Combo_I()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "2")
                            .IfValidThenValidate(
                                new FalseRule1()
                                    .IfInvalidThenInvoke(async rule => output += "3"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("123");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Combo_II()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "2")
                            .IfValidThenValidate(
                                new TrueRule()
                                    .IfValidThenInvoke(async rule => output += "3"),
                                new FalseRule1()
                                    .IfInvalidThenInvoke(async rule => output += "4"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("1234");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Combo_III()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "2")
                            .IfValidThenValidate(
                                new FalseRule1()
                                    .IfInvalidThenInvoke(async rule => output += "4"),
                                new TrueRule() // this rule won't validate because of false rule above
                                    .IfValidThenInvoke(async rule => output += "3"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("124");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Combo_IV()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfInvalidThenInvoke(async rule => output += "2")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "3")
                            .IfInvalidThenInvoke(async rule => output += "4")
                            .IfValidThenValidate(
                                new FalseRule1()
                                    .IfValidThenInvoke(async rule => output += "5")
                                    .IfInvalidThenInvoke(async rule => output += "6"),
                                new TrueRule() // this rule won't validate because of false rule above
                                    .IfInvalidThenInvoke(async rule => output += "7")
                                    .IfValidThenInvoke(async rule => output += "8"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("136");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Combo_V()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output += "1")
                    .IfInvalidThenInvoke(async rule => output += "2")
                    .IfValidThenValidate(
                        new TrueRule()
                            .IfValidThenInvoke(async rule => output += "3")
                            .IfInvalidThenInvoke(async rule => output += "4")
                            .IfValidThenValidate(
                                new TrueRule() // this rule won't validate because of false rule above
                                    .IfInvalidThenInvoke(async rule => output += "5")
                                    .IfValidThenInvoke(async rule => output += "6"),
                                new FalseRule1()
                                    .IfValidThenInvoke(async rule => output += "7")
                                    .IfInvalidThenInvoke(async rule => output += "8"))
                    )
                    .ExecuteAsync();

            output.ShouldBe("1368");
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Async_And_Correctly_Awaits_Async_Invocation()
        {
            List<Person> people = new List<Person>();

            var result = await new TrueRule()
                    .IfValidThenInvoke(async rule =>
                    {
                        var everyone = await new PersonDataProxy().GetPeople();
                        people.AddRange(everyone);
                    })
                    .IfValidThenValidate(new PersonCountRule(people, 3))
                    .ExecuteAsync();

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task Invokes_IfValidThenExecute_Async()
        {
            var output = string.Empty;

            await new TrueRule()
                    .IfValidThenInvoke(async rule => output = "pass")
                    .ExecuteAsync();

            output.ShouldBe("pass");
        }

        [Fact]
        public async Task Does_Not_Invoke_IfValidThenExecute_Async()
        {
            var output = string.Empty;
            await new FalseRule1()
                    .IfValidThenInvoke(async rule => output = "pass")
                    .ExecuteAsync();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public async Task Invokes_IfInvalidThenExecute_Async()
        {
            var output = string.Empty;
            await new FalseRule1()
                    .IfInvalidThenInvoke(async rule => output = "pass")
                    .ExecuteAsync();

            output.ShouldBe("pass");
        }

        [Fact]
        public async Task Does_Not_Invoke_IfInvalidThenExecute_Async()
        {
            var output = string.Empty;
            await new TrueRule()
                   .IfInvalidThenInvoke(async rule => output = "pass")
                   .ExecuteAsync();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public async Task Successor_Invokes_IfValidThenExecute_Async()
        {
            var output = string.Empty;
            await new TrueRule()
                    .IfValidThenValidate(new TrueRule()
                                                .IfValidThenInvoke(async r => output = "pass")).ExecuteAsync();

            output.ShouldBe("pass");
        }

        [Fact]
        public async Task Successor_Does_Not_Invoke_IfValidThenExecute_Async()
        {
            var output = string.Empty;
            await new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfValidThenInvoke(async r => output = "pass")).ExecuteAsync();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public async Task Successor_Invokes_IfInvalidThenExecute_Async()
        {
            var output = string.Empty;
            await new TrueRule()
                .IfValidThenValidate(new FalseRule1()
                                         .IfInvalidThenInvoke(async r => output = "pass")).ExecuteAsync();

            output.ShouldBe("pass");
        }

        [Fact]
        public async Task Successor_Does_Not_Invoke_IfInvalidThenExecute_Async()
        {
            var output = string.Empty;
            await new TrueRule()
                .IfValidThenValidate(new TrueRule()
                                         .IfInvalidThenInvoke(async r => output = "pass")).ExecuteAsync();

            output.ShouldBe(string.Empty);
        }

        [Fact]
        public async Task First_Valid_Rule_In_First_Successor_Chain_Should_Execute_Async()
        {
            var output = string.Empty;
            var rule = await new TrueRule()
                .IfValidThenValidate(new TrueRule().IfValidThenInvoke(async r => output = "pass"), new TrueRule())
                .IfValidThenValidate(new TrueRule(), new FalseRule3())
                .ExecuteAsync();
            output.ShouldBe("pass");
        }

        [Fact]
        public async Task Second_Valid_Rule_In_First_Successor_Chain_Should_Execute_Async()
        {
            var output = string.Empty;
            var rule = await new TrueRule()
                .IfValidThenValidate(new TrueRule(), new TrueRule().IfValidThenInvoke(async r => output = "pass"))
                .IfValidThenValidate(new TrueRule(), new FalseRule3())
                .ExecuteAsync();
            output.ShouldBe("pass");
        }

        [Fact]
        public async Task First_Valid_Rule_In_Second_Successor_Chain_Should_Execute_Async()
        {
            var output = string.Empty;
            var rule = await new TrueRule()
                .IfValidThenValidate(new TrueRule(), new TrueRule())
                .IfValidThenValidate(new TrueRule().IfValidThenInvoke(async r => output = "pass"), new FalseRule3())
                .ExecuteAsync();
            output.ShouldBe("pass");
        }

        [Fact]
        public async Task Second_Invalid_Rule_In_Second_Successor_Chain_Should_Execute_Async()
        {
            var output = string.Empty;
            var rule = await new TrueRule()
                .IfValidThenValidate(new TrueRule(), new TrueRule())
                .IfValidThenValidate(new TrueRule(), new FalseRule3().IfInvalidThenInvoke(async rule =>
                {
                    await Task.Delay(1000);
                    output = "pass";
                }))
                .ExecuteAsync();
            output.ShouldBe("pass");
        }

        [Fact]
        public async Task First_Valid_Rule_In_Second_Successor_Chain_Should_Execute_But_Third_False_Rule_Should_Not_Async()
        {
            var output = string.Empty;
            var output2 = string.Empty;
            var rule = await new TrueRule()
                              .IfValidThenValidate(new TrueRule().IfValidThenInvoke(async r => output = "pass"), new TrueRule())
                              .IfValidThenValidate(new FalseRule2().IfValidThenInvoke(async r => output2 = "pass"), new FalseRule3())
                              .ExecuteAsync();
            output.ShouldBe("pass");
            output2.ShouldBe(string.Empty);
            rule.ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        [Fact]
        public async Task The_Correct_Association_Is_Set_As_A_Result_Of_Failed_Successor_Async()
        {
            var rule = new TrueRule("Foo")
                .IfValidThenValidate(new TrueRule(), new FalseRuleWithAssociation("Address"));
            await rule.ExecuteAsync();
            rule.Association.ShouldBe("Address");
            rule.ErrorMessage.ShouldBe("Address failed validation");
        }

        [Fact]
        public void Allows_access_to_successor_rules_via_IRulesContainer_interface()
        {
            var rule = new TrueRule()
                .IfValidThenValidate(new TrueRule(), new FalseRule2())
                .IfValidThenValidate
                (
                    new TrueRule().IfValidThenValidate(new FalseRule1()),
                    new FalseRule3()
                );

            rule.GetSuccessors().Count().ShouldBe(2);

            var firstSuccessor = rule.GetSuccessors().First();
            firstSuccessor.Rules.Count().ShouldBe(2);
            firstSuccessor.Rules.First().ShouldBeOfType<TrueRule>();
            firstSuccessor.Rules.Second().ShouldBeOfType<FalseRule2>();

            var secondSuccessor = rule.GetSuccessors().Second();
            secondSuccessor.Rules.Count().ShouldBe(2);
            secondSuccessor.Rules.First().ShouldBeOfType<TrueRule>();
            secondSuccessor.Rules.First().GetSuccessors().Count().ShouldBe(1);
            secondSuccessor.Rules.First().GetSuccessors().First().Rules.First().ShouldBeOfType<FalseRule1>();
            secondSuccessor.Rules.Second().ShouldBeOfType<FalseRule3>();
        }

        [Fact]
        public async Task Rule_Using_If_Function_Works_As_Expected()
        {
            var rule = new TrueRuleUsingIfMethod();
            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task Rule_Using_If_Function_Works_As_Expected_II()
        {
            var rule = new FalseRuleUsingIfMethod();
            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("Name cannot be James Hendrix");
        }

        [Fact]
        public async Task Rule_Using_IfNot_Function_Works_As_Expected()
        {
            var rule = new TrueRuleUsingIfNotMethod();
            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task Rule_Using_IfNot_Function_Works_As_Expected_II()
        {
            var rule = new FalseRuleUsingIfNotMethod();
            await rule.ExecuteAsync();
            rule.IsValid.ShouldBeFalse();
            rule.ErrorMessage.ShouldBe("Not old enough");
        }

        [Fact]
        public async Task And_Operator_Works_As_Expected_I()
        {
            var rules = new TrueRule() & new TrueRule() & new FalseRule1() & new FalseRule3();

            rules.Count().ShouldBe(4);
            rules.First().ShouldBeOfType<TrueRule>();
            rules.Second().ShouldBeOfType<TrueRule>();
            rules.Third().ShouldBeOfType<FalseRule1>();
            rules.Fourth().ShouldBeOfType<FalseRule3>();
        }

        [Fact]
        public async Task And_Operator_Works_As_Expected_II()
        {
            var rules = new TrueRule().IfValidThenValidate(new TrueRule()) &
                new TrueRule().IfValidThenValidate(new FalseRule2());

            rules.Count().ShouldBe(2);
            rules.First().ShouldBeOfType<TrueRule>();
            rules.First().GetSuccessors().First().Rules.First().ShouldBeOfType<TrueRule>();
            rules.Second().ShouldBeOfType<TrueRule>();
            rules.Second().GetSuccessors().First().Rules.First().ShouldBeOfType<FalseRule2>();
        }

        [Fact]
        public async Task And_Operator_Works_As_Expected_III()
        {
            var ruleSetOne = new TrueRule() & new FalseRule3() & new TrueRule();
            var rule = new FalseRule2();
            var ruleSetTwo = ruleSetOne & rule;

            ruleSetTwo.Count().ShouldBe(4);
            ruleSetTwo.First().ShouldBeOfType<TrueRule>();
            ruleSetTwo.Second().ShouldBeOfType<FalseRule3>();
            ruleSetTwo.Third().ShouldBeOfType<TrueRule>();
            ruleSetTwo.Fourth().ShouldBeOfType<FalseRule2>();
        }

        [Fact]
        public async Task And_Operator_Works_As_Expected_IV()
        {
            var rule = new FalseRule2();
            var ruleSetOne = new TrueRule() & new FalseRule3() & new TrueRule();
            var ruleSetTwo = rule & ruleSetOne;

            ruleSetTwo.Count().ShouldBe(4);
            ruleSetTwo.First().ShouldBeOfType<FalseRule2>();
            ruleSetTwo.Second().ShouldBeOfType<TrueRule>();
            ruleSetTwo.Third().ShouldBeOfType<FalseRule3>();
            ruleSetTwo.Fourth().ShouldBeOfType<TrueRule>();
        }

        [Fact]
        public async Task Addition_Operator_Works_As_Expected_I()
        {
            var rules = new TrueRule() + new TrueRule() + new FalseRule1() + new FalseRule3();

            rules.Count().ShouldBe(4);
            rules.First().ShouldBeOfType<TrueRule>();
            rules.Second().ShouldBeOfType<TrueRule>();
            rules.Third().ShouldBeOfType<FalseRule1>();
            rules.Fourth().ShouldBeOfType<FalseRule3>();
        }

        [Fact]
        public async Task Addition_Operator_Works_As_Expected_II()
        {
            var rules = new TrueRule().IfValidThenValidate(new TrueRule())
                + new TrueRule().IfValidThenValidate(new FalseRule2());

            rules.Count().ShouldBe(2);
            rules.First().ShouldBeOfType<TrueRule>();
            rules.First().GetSuccessors().First().Rules.First().ShouldBeOfType<TrueRule>();
            rules.Second().ShouldBeOfType<TrueRule>();
            rules.Second().GetSuccessors().First().Rules.First().ShouldBeOfType<FalseRule2>();
        }

        [Fact]
        public async Task Addition_Operator_Works_As_Expected_III()
        {
            var ruleSetOne = new TrueRule() + new FalseRule3() + new TrueRule();
            var rule = new FalseRule2();
            var ruleSetTwo = ruleSetOne + rule;

            ruleSetTwo.Count().ShouldBe(4);
            ruleSetTwo.First().ShouldBeOfType<TrueRule>();
            ruleSetTwo.Second().ShouldBeOfType<FalseRule3>();
            ruleSetTwo.Third().ShouldBeOfType<TrueRule>();
            ruleSetTwo.Fourth().ShouldBeOfType<FalseRule2>();
        }

        [Fact]
        public async Task Addition_Operator_Works_As_Expected_IV()
        {
            var rule = new FalseRule2();
            var ruleSetOne = new TrueRule() + new FalseRule3() + new TrueRule();
            var ruleSetTwo = rule + ruleSetOne;

            ruleSetTwo.Count().ShouldBe(4);
            ruleSetTwo.First().ShouldBeOfType<FalseRule2>();
            ruleSetTwo.Second().ShouldBeOfType<TrueRule>();
            ruleSetTwo.Third().ShouldBeOfType<FalseRule3>();
            ruleSetTwo.Fourth().ShouldBeOfType<TrueRule>();
        }

        public class PersonDataProxy
        {
            public async Task<IEnumerable<Person>> GetPeople()
            {
                await Task.CompletedTask;

                return new []
                {
                    new Person(),
                    new Person(),
                    new Person()
                };
            }
        }
    }
}
