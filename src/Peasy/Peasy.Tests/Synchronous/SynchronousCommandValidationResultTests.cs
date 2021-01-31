﻿using Moq;
using Peasy.Synchronous;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Core.Tests
{
    public class SynchronousCommandValidationResultTests
    {
        [Fact]
        public void CanContinue_Is_True_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult();
            Func<ExecutionResult> continuationFunc = () => executionResult;

            new SynchronousCommandValidationResult(errors, continuationFunc).CanContinue.ShouldBe(true);
            new SynchronousCommandValidationResult(null, continuationFunc).CanContinue.ShouldBe(true);
        }

        [Fact]
        public void Allows_Continuation_Function_To_Execute_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult();
            Func<ExecutionResult> continuationFunc = () => executionResult;

            new SynchronousCommandValidationResult(errors, continuationFunc).CompleteCommandExecution().ShouldBe(executionResult);
            new SynchronousCommandValidationResult(null, continuationFunc).CompleteCommandExecution().ShouldBe(executionResult);
        }

        [Fact]
        public void CanContinue_Is_False_When_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult();
            Func<ExecutionResult> continuationFunc = () => executionResult;

            new SynchronousCommandValidationResult(errors, continuationFunc).CanContinue.ShouldBe(false);
        }

        [Fact]
        public void Continuation_Function_Throws_Exception_When_Access_Is_Requested_And_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult();
            Func<ExecutionResult> continuationFunc = () => executionResult;

            var result = new SynchronousCommandValidationResult(errors, continuationFunc);

            var ex = Assert.Throws<InvalidOperationException>(() => result.CompleteCommandExecution());
            ex.Message.ShouldBe("Cannot complete command execution because validation was not successful.");
        }

        [Fact]
        public void Of_T_CanContinue_Is_True_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult<string>();
            Func<ExecutionResult<string>> continuationFunc = () => executionResult;

            new SynchronousCommandValidationResult<string>(errors, continuationFunc).CanContinue.ShouldBe(true);
            new SynchronousCommandValidationResult<string>(null, continuationFunc).CanContinue.ShouldBe(true);
        }

        [Fact]
        public void Of_T_Allows_Continuation_Function_To_Execute_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult<string>();
            Func<ExecutionResult<string>> continuationFunc = () => executionResult;

            new SynchronousCommandValidationResult<string>(errors, continuationFunc).CompleteCommandExecution().ShouldBe(executionResult);
            new SynchronousCommandValidationResult<string>(null, continuationFunc).CompleteCommandExecution().ShouldBe(executionResult);
        }

        [Fact]
        public void Of_T_CanContinue_Is_False_When_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult<string>();
            Func<ExecutionResult<string>> continuationFunc = () => executionResult;

            new SynchronousCommandValidationResult<string>(errors, continuationFunc).CanContinue.ShouldBe(false);
        }

        [Fact]
        public void Of_T_Continuation_Function_Throws_Exception_When_Access_Is_Requested_And_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult<string>();
            Func<ExecutionResult<string>> continuationFunc = () => executionResult;

            var result = new SynchronousCommandValidationResult<string>(errors, continuationFunc);

            var ex = Assert.Throws<InvalidOperationException>(() => result.CompleteCommandExecution());
            ex.Message.ShouldBe("Cannot complete command execution because validation was not successful.");
        }

    }
}