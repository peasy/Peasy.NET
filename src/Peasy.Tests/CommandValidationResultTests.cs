﻿using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Core.Tests
{
    public class CommandValidationResultTests
    {
        [Fact]
        public async Task CanContinue_Is_True_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult();
            Func<Task<ExecutionResult>> continuationFunc = async () => executionResult;

            new CommandValidationResult(errors, continuationFunc).CanContinue.ShouldBe(true);
            new CommandValidationResult(null, continuationFunc).CanContinue.ShouldBe(true);
        }

        [Fact]
        public async Task Allows_Continuation_Function_To_Execute_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult();
            Func<Task<ExecutionResult>> continuationFunc = async () => executionResult;

            (await new CommandValidationResult(errors, continuationFunc).CompleteCommandExecutionAsync()).ShouldBe(executionResult);
            (await new CommandValidationResult(null, continuationFunc).CompleteCommandExecutionAsync()).ShouldBe(executionResult);
        }

        [Fact]
        public async Task CanContinue_Is_False_When_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult();
            Func<Task<ExecutionResult>> continuationFunc = async () => executionResult;

            new CommandValidationResult(errors, continuationFunc).CanContinue.ShouldBe(false);
        }

        [Fact]
        public async Task Continuation_Function_Throws_Exception_When_Access_Is_Requested_And_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult();
            Func<Task<ExecutionResult>> continuationFunc = async () => executionResult;

            var result = new CommandValidationResult(errors, continuationFunc);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => result.CompleteCommandExecutionAsync());
            ex.Message.ShouldBe("Cannot complete command execution because validation was not successful.");
        }

        [Fact]
        public async Task Of_T_CanContinue_Is_True_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult<string>();
            Func<Task<ExecutionResult<string>>> continuationFunc = async () => executionResult;

            new CommandValidationResult<string>(errors, continuationFunc).CanContinue.ShouldBe(true);
            new CommandValidationResult<string>(null, continuationFunc).CanContinue.ShouldBe(true);
        }

        [Fact]
        public async Task Of_T_Allows_Continuation_Function_To_Execute_When_Validation_Results_Are_Empty_Or_Null()
        {
            var errors = new ValidationResult[] {};
            var executionResult = new ExecutionResult<string>();
            Func<Task<ExecutionResult<string>>> continuationFunc = async () => executionResult;

            (await new CommandValidationResult<string>(errors, continuationFunc).CompleteCommandExecutionAsync()).ShouldBe(executionResult);
            (await new CommandValidationResult<string>(null, continuationFunc).CompleteCommandExecutionAsync()).ShouldBe(executionResult);
        }

        [Fact]
        public async Task Of_T_CanContinue_Is_False_When_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult<string>();
            Func<Task<ExecutionResult<string>>> continuationFunc = async () => executionResult;

            new CommandValidationResult<string>(errors, continuationFunc).CanContinue.ShouldBe(false);
        }

        [Fact]
        public async Task Of_T_Continuation_Function_Throws_Exception_When_Access_Is_Requested_And_Validation_Results_Are_Not_Empty()
        {
            var errors = new ValidationResult[] { new ValidationResult("You shall not pass") };
            var executionResult = new ExecutionResult<string>();
            Func<Task<ExecutionResult<string>>> continuationFunc = async () => executionResult;

            var result = new CommandValidationResult<string>(errors, continuationFunc);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => result.CompleteCommandExecutionAsync());
            ex.Message.ShouldBe("Cannot complete command execution because validation was not successful.");
        }

    }
}