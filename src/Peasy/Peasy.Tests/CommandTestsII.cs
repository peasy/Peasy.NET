﻿using Moq;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Core.Tests
{
    public interface IDoThings
    {
        void Log(string message);
        void DoSomething();
        string GetValue();
    }

    public class CommandStubOne : Command
    {
        private IEnumerable<IRule> _rules;
        private IEnumerable<ValidationResult> _validationResults;
        private IDoThings _doerOfThings;

        public CommandStubOne(IDoThings doerOfThings)
        {
            _doerOfThings = doerOfThings;
        }

        public CommandStubOne(IDoThings doerOfThings, IEnumerable<IRule> rules) : this(doerOfThings)
        {
            _rules = rules;
        }

        public CommandStubOne(IDoThings doerOfThings, IEnumerable<ValidationResult> validationResults) : this(doerOfThings)
        {
            _validationResults = validationResults;
        }

        protected override void OnInitialization()
        {
            _doerOfThings.Log(nameof(OnInitialization));
        }

        protected override Task OnInitializationAsync()
        {
            _doerOfThings.Log(nameof(OnInitializationAsync));
            return Task.CompletedTask;
        }

        protected override IEnumerable<ValidationResult> OnGetErrors()
        {
            _doerOfThings.Log(nameof(OnGetErrors));
            return _validationResults ?? base.OnGetErrors();
        }

        protected async override Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            _doerOfThings.Log(nameof(OnGetErrorsAsync));
            return _validationResults ?? await base.OnGetErrorsAsync();
        }

        protected override IEnumerable<IRule> OnGetRules()
        {
            _doerOfThings.Log(nameof(OnGetRules));
            return _rules ?? base.OnGetRules();
        }

        protected async override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            _doerOfThings.Log(nameof(OnGetRulesAsync));
            return _rules ?? await base.OnGetRulesAsync();
        }

        protected override void OnExecute()
        {
            _doerOfThings.Log(nameof(OnExecute));
            _doerOfThings.DoSomething();
            base.OnExecute();
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

        protected override ExecutionResult OnServiceException(ServiceException exception)
        {
            _doerOfThings.Log(nameof(OnServiceException));
            return base.OnServiceException(exception);
        }

        protected override ExecutionResult OnSuccessfulExecution()
        {
            _doerOfThings.Log(nameof(OnSuccessfulExecution));
            return base.OnSuccessfulExecution();
        }
    }

    public class CommandTestsII
    {
        #region Synchronous

        [Fact]
        public void Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandStubOne(doerOfThings.Object);

            var result = command.Execute();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrors"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public void Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_All_Rules_Pass()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandStubOne(doerOfThings.Object, new IRule[] { new TrueRule(), new TrueRule() });

            var result = command.Execute();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrors"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public void Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Rules_Fail()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStubOne(doerOfThings.Object, rules);

            var result = command.Execute();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrors"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public void Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Validation_Results_Exist()
        {
            var doerOfThings = new Mock<IDoThings>();
            var validationResult = new ValidationResult("You shall not pass");
            var command = new CommandStubOne(doerOfThings.Object, new [] { validationResult });

            var result = command.Execute();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrors"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public void Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_A_ServiceException_Is_Caught()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.DoSomething()).Throws(new ServiceException("You shall not pass"));
            var command = new CommandStubOne(doerOfThings.Object);

            var result = command.Execute();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrors"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }


        #endregion

        #region Asynchronous

        [Fact]
        public async Task Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandStubOne(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrorsAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public async Task Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_All_Rules_Pass_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandStubOne(doerOfThings.Object, new IRule[] { new TrueRule(), new TrueRule() });

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrorsAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public async Task Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Rules_Fail_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStubOne(doerOfThings.Object, rules);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrorsAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public async Task Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Validation_Results_Exist_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            var validationResult = new ValidationResult("You shall not pass");
            var command = new CommandStubOne(doerOfThings.Object, new [] { validationResult });

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrorsAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public async Task Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_A_ServiceException_Is_Caught_Async()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.DoSomething()).Throws(new ServiceException("You shall not pass"));
            var command = new CommandStubOne(doerOfThings.Object);

            var result = await command.ExecuteAsync();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");

            doerOfThings.Verify(d => d.Log("OnInitializationAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetErrorsAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRulesAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecuteAsync"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnServiceException"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        #endregion

        #region IRulesContainer Support

        [Fact]
        public void Allows_Retrieval_Of_Configured_Rules()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStubOne(doerOfThings.Object, rules);

            command.GetRules().ShouldBe(rules);
        }

        #endregion

        #region IValidationErrorsContainer Support

        [Fact]
        public void Allows_Execution_Of_Rules_Via_Supported_Interface()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandStubOne(doerOfThings.Object, rules);

            var errors = command.GetErrors().ToArray();

            errors.Count().ShouldBe(1);
            errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        #endregion
    }
}