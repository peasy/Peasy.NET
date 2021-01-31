﻿using Moq;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Core.Tests.CommandBaseTestsOfT
{
    public class Tests
    {
        [Fact]
        public async Task Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.GetValue()).Returns("You shall pass");
            var command = new CommandStub(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();
            result.Value.ShouldBe("You shall pass");

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
            doerOfThings.Setup(d => d.GetValue()).Returns("You shall pass");
            var command = new CommandStub(doerOfThings.Object, new IRule[] { new TrueRule(), new TrueRule() });

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();
            result.Value.ShouldBe("You shall pass");

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
            result.Value.ShouldBeNull();

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
            result.Value.ShouldBeNull();

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
            doerOfThings.Setup(d => d.GetValue()).Throws(new PeasyException("You shall not pass"));
            var command = new CommandStub(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");
            result.Value.ShouldBeNull();

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

        #endregion

        #region ISupportValidation Support

        [Fact]
        public async Task Allows_Execution_Of_Configured_Rules()
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
            doerOfThings.Setup(d => d.GetValue()).Returns("You shall pass");
            var rules = new IRule[] { new TrueRule(), new TrueRule() };
            var command = new CommandStub(doerOfThings.Object, rules);

            var validationResult = await command.ValidateAsync();

            validationResult.CanContinue.ShouldBeTrue();
            validationResult.Errors.Count().ShouldBe(0);

            var executionResult = await validationResult.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
            executionResult.Value.ShouldBe("You shall pass");
        }

        [Fact]
        public async Task Completion_Properly_Handles_Caught_Peasy_Exception()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.GetValue()).Throws(new PeasyException("You shall not pass"));
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
    }

    public class CommandStub : CommandBase<string>
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

        protected override Task<ExecutionResult<string>> OnCompleteAsync()
        {
            _doerOfThings.Log(nameof(OnCompleteAsync));
            return base.OnCompleteAsync();
        }

        protected override Task<string> OnExecuteAsync()
        {
            _doerOfThings.Log(nameof(OnExecuteAsync));
            return Task.FromResult(_doerOfThings.GetValue());
        }

        protected override ExecutionResult<string> OnFailedExecution(ValidationResult[] validationResults)
        {
            _doerOfThings.Log(nameof(OnFailedExecution));
            return base.OnFailedExecution(validationResults);
        }

        protected override ExecutionResult<string> OnPeasyExceptionHandled(PeasyException exception)
        {
            _doerOfThings.Log(nameof(OnPeasyExceptionHandled));
            return base.OnPeasyExceptionHandled(exception);
        }

        protected override ExecutionResult<string> OnSuccessfulExecution(string value)
        {
            _doerOfThings.Log(nameof(OnSuccessfulExecution));
            return base.OnSuccessfulExecution(value);
        }
    }
}