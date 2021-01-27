﻿using Moq;
using Peasy.Synchronous;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Peasy.Core.Tests.CommandTestsOfT
{
    public class SynchronousCommandTestsOfT
    {
        [Fact]
        public void Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.GetValue()).Returns("You shall pass");
            var command = new SynchronousCommandStub(doerOfThings.Object);

            var result = command.Execute();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();
            result.Value.ShouldBe("You shall pass");

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidate"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public void Successful_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_All_Rules_Pass()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.GetValue()).Returns("You shall pass");
            var command = new SynchronousCommandStub(doerOfThings.Object, new ISynchronousRule[] { new SynchronousTrueRule(), new SynchronousTrueRule() });

            var result = command.Execute();

            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();
            result.Value.ShouldBe("You shall pass");

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidate"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Once);
        }

        [Fact]
        public void Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Rules_Fail()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new ISynchronousRule[] { new SynchronousTrueRule(), new SynchronousFalseRule1() };
            var command = new SynchronousCommandStub(doerOfThings.Object, rules);

            var result = command.Execute();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            result.Value.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidate"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public void Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_Any_Validation_Results_Exist()
        {
            var doerOfThings = new Mock<IDoThings>();
            var validationResult = new ValidationResult("You shall not pass");
            var command = new SynchronousCommandStub(doerOfThings.Object, new [] { validationResult });

            var result = command.Execute();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");
            result.Value.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidate"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Never);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        [Fact]
        public void Fails_Execution_With_Expected_ExecutionResult_And_Method_Invocations_When_A_ServiceException_Is_Caught()
        {
            var doerOfThings = new Mock<IDoThings>();
            doerOfThings.Setup(d => d.GetValue()).Throws(new PeasyException("You shall not pass"));
            var command = new SynchronousCommandStub(doerOfThings.Object);

            var result = command.Execute();

            result.Success.ShouldBeFalse();
            result.Errors.Count().ShouldBe(1);
            result.Errors.First().ErrorMessage.ShouldBe("You shall not pass");
            result.Value.ShouldBeNull();

            doerOfThings.Verify(d => d.Log("OnInitialization"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnValidate"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnGetRules"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnExecute"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnFailedExecution"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnPeasyExceptionHandled"), Times.Once);
            doerOfThings.Verify(d => d.Log("OnSuccessfulExecution"), Times.Never);
        }

        #region IRulesContainer Support

        [Fact]
        public void Allows_Retrieval_Of_Configured_Rules()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new ISynchronousRule[] { new SynchronousTrueRule(), new SynchronousFalseRule1() };
            var command = new SynchronousCommandStub(doerOfThings.Object, rules);

            command.GetRules().ShouldBe(rules);
        }

        #endregion

        #region IValidationErrorsContainer Support

        [Fact]
        public void Allows_Execution_Of_Configured_Rules()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new ISynchronousRule[] { new SynchronousTrueRule(), new SynchronousFalseRule1() };
            var command = new SynchronousCommandStub(doerOfThings.Object, rules);

            var errors = command.Validate().Results.ToArray();

            errors.Count().ShouldBe(1);
            errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        #endregion
    }

    public class SynchronousCommandStub : SynchronousCommand<string>
    {
        private IEnumerable<ISynchronousRule> _rules;
        private IEnumerable<ValidationResult> _validationResults;
        private IDoThings _doerOfThings;

        public SynchronousCommandStub(IDoThings doerOfThings)
        {
            _doerOfThings = doerOfThings;
        }

        public SynchronousCommandStub(IDoThings doerOfThings, IEnumerable<ISynchronousRule> rules) : this(doerOfThings)
        {
            _rules = rules;
        }

        public SynchronousCommandStub(IDoThings doerOfThings, IEnumerable<ValidationResult> validationResults) : this(doerOfThings)
        {
            _validationResults = validationResults;
        }

        protected override void OnInitialization()
        {
            _doerOfThings.Log(nameof(OnInitialization));
        }

        protected override IEnumerable<ValidationResult> OnValidate()
        {
            _doerOfThings.Log(nameof(OnValidate));
            return _validationResults ?? base.OnValidate();
        }

        protected override IEnumerable<ISynchronousRule> OnGetRules()
        {
            _doerOfThings.Log(nameof(OnGetRules));
            return _rules ?? base.OnGetRules();
        }

        protected override string OnExecute()
        {
            _doerOfThings.Log(nameof(OnExecute));
            return _doerOfThings.GetValue();
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