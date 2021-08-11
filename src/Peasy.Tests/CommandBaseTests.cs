﻿using Moq;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Core.Tests.CommandBaseTests
{
    public class Tests
    {
        [Fact]
        public async Task Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandStub(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidateAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnCompleteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public async Task Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_All_Rules_Pass_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandStub(doerOfThings.Object, new IRule[] { new TrueRule(), new TrueRule() });

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidateAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnCompleteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public async Task Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Rules_Fail_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidateAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnCompleteAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public async Task Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Validation_Results_Exist_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var validationResult = new ValidationResult("You shall not pass");
            var command = new CommandStub(doerOfThings.Object, new [] { validationResult });

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidateAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnCompleteAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public async Task Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_A_ServiceException_Is_Caught_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.DoSomething()).Throws(new PeasyException("You shall not pass"));
            var command = new CommandStub(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidateAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnCompleteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        #region IRulesContainer Support

        [Fact]
        public async Task Allows_Retrieval_Of_Configured_Rules()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var results = await command.GetRulesAsync();
            results.ShouldBe(rules);
        }

        [Fact]
        public async Task Allows_Retrieval_Of_Configured_Rules_With_TheseRules()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new TheseRulesStub(doerOfThings.Object);

            var results = await command.GetRulesAsync();
            results.Count().ShouldBe(3);
        }

        #endregion

        #region ISupportCommandValidation Support

        [Fact]
        public async Task Allows_Validation_Of_Configured_Rules()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var errors = (await command.ValidateAsync()).Errors.ToArray();

            errors.Count().ShouldBe(1);
            errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Operation_Cannot_Complete_If_Any_Rules_Fail_Validation()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var result = await command.ValidateAsync();

            result.CanContinue.ShouldBe(false);
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public async Task Operation_Can_Complete_If_Rules_Pass_Validation_And_Complete_Validation_With_Successful_Validation_Results()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new TrueRule() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var validationResult = await command.ValidateAsync();

            validationResult.CanContinue.ShouldBeTrue();
            validationResult.Errors.Count().ShouldBe(0);

            var executionResult = await validationResult.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Completion_Properly_Handles_Caught_Peasy_Exception()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.DoSomething()).Throws(new PeasyException("You shall not pass"));
            var rules = new IRule[] { new TrueRule(), new TrueRule() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var validationResult = await command.ValidateAsync();

            validationResult.CanContinue.ShouldBeTrue();
            validationResult.Errors.Count().ShouldBe(0);

            var executionResult = await validationResult.CompleteCommandExecutionAsync();

            executionResult.Success.ShouldBeFalse();
            executionResult.Errors.Count().ShouldBe(1);
            executionResult.Errors.First().ErrorMessage.ShouldBe("You shall not pass");
        }

        #endregion

        #region RuleList() support

        [Fact]
        public async Task TheseRules_Works_As_Expected()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new TheseRulesStub(doerOfThings.Object);
            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(2);
        }

        #endregion

        #region Operator() support

        [Fact]
        public async Task Plus_Operator_Works_As_Expected()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new PlusOperatorCommandStub(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(2);
        }

        [Fact]
        public async Task And_Operator_Works_As_Expected()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new AndOperatorCommandStub(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(2);
        }

        #endregion
    }

    public class CommandStub : CommandBase
    {
        private IEnumerable<IRule> _rules;
        private IEnumerable<ValidationResult> _validationResults;
        private IDoThings _doerOfThings;

        public CommandStub(IDoThings doerOfThings)
        {
            _doerOfThings = doerOfThings;
        }

        public CommandStub(IDoThings doerOfThings, IEnumerable<IRule> rules) : this(doerOfThings)
        {
            _rules = rules;
        }

        public CommandStub(IDoThings doerOfThings, IEnumerable<ValidationResult> validationResults) : this(doerOfThings)
        {
            _validationResults = validationResults;
        }

        protected override Task OnInitializationAsync()
        {
            _doerOfThings.Log(nameof(OnInitializationAsync));
            return Task.CompletedTask;
        }

        protected async override Task<IEnumerable<ValidationResult>> OnValidateAsync()
        {
            _doerOfThings.Log(nameof(OnValidateAsync));
            return _validationResults ?? await base.OnValidateAsync();
        }

        protected async override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            _doerOfThings.Log(nameof(OnGetRulesAsync));
            return _rules ?? await base.OnGetRulesAsync();
        }

        protected override Task<ExecutionResult> OnCompleteAsync()
        {
            _doerOfThings.Log(nameof(OnCompleteAsync));
            return base.OnCompleteAsync();
        }

        protected override Task OnExecuteAsync()
        {
            _doerOfThings.Log(nameof(OnExecuteAsync));
            _doerOfThings.DoSomething();
            return base.OnExecuteAsync();
        }

        protected override ExecutionResult OnFailedExecution(IEnumerable<ValidationResult> validationResults)
        {
            _doerOfThings.Log(nameof(OnFailedExecution));
            return base.OnFailedExecution(validationResults);
        }

        protected override ExecutionResult OnPeasyExceptionHandled(PeasyException exception)
        {
            _doerOfThings.Log(nameof(OnPeasyExceptionHandled));
            return base.OnPeasyExceptionHandled(exception);
        }

        protected override ExecutionResult OnSuccessfulExecution()
        {
            _doerOfThings.Log(nameof(OnSuccessfulExecution));
            return base.OnSuccessfulExecution();
        }
    }

    public class TheseRulesStub : CommandStub
    {
        public TheseRulesStub(IDoThings doerOfThings) : base(doerOfThings) { }

        public TheseRulesStub(IDoThings doerOfThings, IEnumerable<IRule> rules) : base(doerOfThings, rules) {}

        public TheseRulesStub(IDoThings doerOfThings, IEnumerable<ValidationResult> validationResults) : base(doerOfThings, validationResults) {}

        // Because we should be able to.
        protected override Task<IEnumerable<IRule>> TheseRules(params IRule[] rules)
        {
            return base.TheseRules(rules);
        }

        protected async override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            await Task.CompletedTask;

            return await TheseRules
            (
                new FalseRule1(),
                new TrueRule(),
                new TrueRule().IfValidThenValidate(new FalseRule1())
            );
        }
    }

    public class AndOperatorCommandStub : CommandStub
    {
        public AndOperatorCommandStub(IDoThings doerOfThings) : base(doerOfThings)
        {
        }

        public AndOperatorCommandStub(IDoThings doerOfThings, IEnumerable<IRule> rules) : base(doerOfThings, rules)
        {
        }

        public AndOperatorCommandStub(IDoThings doerOfThings, IEnumerable<ValidationResult> validationResults) : base(doerOfThings, validationResults)
        {
        }

        protected async override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            await Task.CompletedTask;

            return new FalseRule1() &
                new TrueRule() &
                new TrueRule().IfValidThenValidate(new FalseRule1());
        }
    }

    public class PlusOperatorCommandStub : CommandStub
    {
        public PlusOperatorCommandStub(IDoThings doerOfThings) : base(doerOfThings)
        {
        }

        public PlusOperatorCommandStub(IDoThings doerOfThings, IEnumerable<IRule> rules) : base(doerOfThings, rules)
        {
        }

        public PlusOperatorCommandStub(IDoThings doerOfThings, IEnumerable<ValidationResult> validationResults) : base(doerOfThings, validationResults)
        {
        }

        protected async override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            await Task.CompletedTask;

            return new FalseRule1() +
                new TrueRule() +
                new TrueRule().IfValidThenValidate(new FalseRule1());
        }
    }
}