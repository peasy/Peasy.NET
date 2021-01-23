﻿using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Core.Tests
{
    public class CommandTests
    {
        #region Command

        [Fact]
        public void OnInitialization_Is_Invoked()
        {
            var stub = new CommandStub();
            var result = stub.Execute();
            stub.OnInitializationWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnGetRules_Is_Invoked()
        {
            var stub = new CommandStub();
            var result = stub.Execute();
            stub.OnGetRulesWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnGetErrors_Is_Invoked()
        {
            var stub = new CommandStub();
            var result = stub.Execute();
            stub.OnGetErrorsWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnExecute_Is_Invoked_When_No_Errors_Exist()
        {
            var stub = new CommandStub();
            var result = stub.Execute();
            stub.OnExecuteWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void ExecutionResult_Is_Successful_When_Validation_Is_Successful()
        {
            var stub = new CommandStub();
            var result = stub.Execute();
            result.Success.ShouldBe(true);
        }

        [Fact]
        public void ExecutionResult_Should_Contain_No_Errors_When_Validation_Is_Successful()
        {
            var stub = new CommandStub();
            var result = stub.Execute();
            result.Errors.ShouldBe(null);
        }

        [Fact]
        public void OnExecute_Is_Not_Invoked_When_Errors_Exist()
        {
            var stub = new CommandStub { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            stub.Execute();
            stub.OnExecuteWasInvoked.ShouldBe(false);
        }

        [Fact]
        public void ExecutionResult_Is_Not_Successful_When_Validation_Is_Not_Successful()
        {
            var stub = new CommandStub { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            var result = stub.Execute();
            result.Success.ShouldBe(false);
        }

        [Fact]
        public void ExecutionResult_Should_Contain_Errors_When_Validation_Is_Not_Successful()
        {
            var stub = new CommandStub { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            var result = stub.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public void ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught()
        {
            var command = new CommandStubThrowsErrorsOnExecute();
            var result = command.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public async Task OnInitializationAsync_Is_Invoked()
        {
            var stub = new CommandStub();
            await stub.ExecuteAsync();
            stub.OnInitializationAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task OnGetRulesAsync_Is_Invoked()
        {
            var stub = new CommandStub();
            await stub.ExecuteAsync();
            stub.OnGetRulesAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task OnGetErrorsAsync_Is_Invoked()
        {
            var stub = new CommandStub();
            await stub.ExecuteAsync();
            stub.OnGetErrorsAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task OnExecuteAsync_Is_Invoked_When_No_Errors_Exist()
        {
            var stub = new CommandStub();
            await stub.ExecuteAsync();
            stub.OnExecuteAsyncWasInvoked.ShouldBe(true);
       }

        [Fact]
        public async Task OnExecuteAsync_Is_Not_Invoked_When_Errors_Exist()
        {
            var stub = new CommandStub { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            await stub.ExecuteAsync();
            stub.OnExecuteAsyncWasInvoked.ShouldBe(false);
        }

        [Fact]
        public async Task ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught_Async()
        {
            var command = new CommandStubThrowsErrorsOnExecute();
            var result = await command.ExecuteAsync();
            result.Errors.Count().ShouldBe(1);
        }

        #endregion

        #region Command<T>

        [Fact]
        public void Command_of_T_OnInitialization_Is_Invoked()
        {
            var stub = new CommandStubOfString();
            var result = stub.Execute();
            stub.OnInitializationWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Command_of_T_OnExecute_Is_Invoked_When_No_Errors_Exist()
        {
            var stub = new CommandStubOfString();
            var result = stub.Execute();
            stub.OnExecuteWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Command_of_T_Execute_Returns_Value_When_No_Errors_Exist()
        {
            var stub = new CommandStubOfString();
            var result = stub.Execute();
            result.Value.ShouldBe("some value");
        }

        [Fact]
        public void Command_of_T_ExecutionResult_Is_Successful_When_Validation_Is_Successful()
        {
            var stub = new CommandStubOfString();
            var result = stub.Execute();
            result.Success.ShouldBe(true);
        }

        [Fact]
        public void Command_of_T_ExecutionResult_Should_Contain_No_Errors_When_Validation_Is_Successful()
        {
            var stub = new CommandStubOfString();
            var result = stub.Execute();
            result.Errors.ShouldBe(null);
        }

        [Fact]
        public void Command_of_T_OnExecute_Is_Not_Invoked_When_Errors_Exist()
        {
            var stub = new CommandStubOfString { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            stub.Execute();
            stub.OnExecuteWasInvoked.ShouldBe(false);
        }

        [Fact]
        public void Command_of_T_ExecutionResult_Is_Not_Successful_When_Validation_Is_Not_Successful()
        {
            var stub = new CommandStubOfString { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            var result = stub.Execute();
            result.Success.ShouldBe(false);
        }

        [Fact]
        public void Command_of_T_ExecutionResult_Should_Contain_Errors_When_Validation_Is_Not_Successful()
        {
            var stub = new CommandStubOfString { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            var result = stub.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public void Command_of_T_ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught()
        {
            var command = new CommandStubOfStringThrowsErrorsOnExecute();
            var result = command.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public async Task Command_of_T_OnInitializationAsync_Is_Invoked()
        {
            var stub = new CommandStubOfString();
            await stub.ExecuteAsync();
            stub.OnInitializationAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Command_of_T_OnExecuteAsync_Is_Invoked_When_No_Errors_Exist()
        {
            var stub = new CommandStubOfString();
            await stub.ExecuteAsync();
            stub.OnExecuteAsyncWasInvoked.ShouldBe(true);
       }

        [Fact]
        public async Task Command_of_T_Execute_Returns_Value_When_No_Errors_Exist_Async()
        {
            var stub = new CommandStubOfString();
            var result = await stub.ExecuteAsync();
            result.Value.ShouldBe("some value");
        }

        [Fact]
        public async Task Command_of_T_OnExecuteAsync_Is_Not_Invoked_When_Errors_Exist()
        {
            var stub = new CommandStubOfString { Errors = new[] { new ValidationResult("Object doesn't exist") } };
            await stub.ExecuteAsync();
            stub.OnExecuteAsyncWasInvoked.ShouldBe(false);
        }

        [Fact]
        public async Task Command_of_T_ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught_Async()
        {
            var command = new CommandStubOfStringThrowsErrorsOnExecute();
            var result = await command.ExecuteAsync();
            result.Errors.Count().ShouldBe(1);
        }

        #endregion
    }

    public class CommandStub : Command
    {
        public CommandStub()
        {
            // Errors = Enumerable.Empty<ValidationResult>();
        }

        public CommandStub(IEnumerable<ValidationResult> validationResults)
        {
            Errors = validationResults;
        }

        public IEnumerable<ValidationResult> Errors { get; set; }
        public bool OnInitializationWasInvoked { get; private set; }
        public bool OnExecuteWasInvoked { get; private set; }
        public bool OnInitializationAsyncWasInvoked { get; private set; }
        public bool OnExecuteAsyncWasInvoked { get; private set; }
        public bool OnGetRulesWasInvoked { get; private set; }
        public bool OnGetRulesAsyncWasInvoked { get; private set; }
        public bool OnGetErrorsWasInvoked { get; private set; }
        public bool OnGetErrorsAsyncWasInvoked { get; private set; }

        protected override void OnInitialization()
        {
            OnInitializationWasInvoked = true;
            base.OnInitialization();
        }

        protected override Task OnInitializationAsync()
        {
            OnInitializationAsyncWasInvoked = true;
            return base.OnInitializationAsync();
        }

        protected override IEnumerable<IRule> OnGetRules()
        {
            OnGetRulesWasInvoked = true;
            return base.OnGetRules();
        }
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            OnGetRulesAsyncWasInvoked = true;
            return base.OnGetRulesAsync();
        }

        protected override IEnumerable<ValidationResult> OnGetErrors()
        {
            OnGetErrorsWasInvoked = true;
            return Errors ?? base.OnGetErrors();
        }

        protected async override Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            OnGetErrorsAsyncWasInvoked = true;
            return Errors ?? await base.OnGetErrorsAsync();
        }

        protected override void OnExecute()
        {
            OnExecuteWasInvoked = true;
            base.OnExecute();
        }

        protected override Task OnExecuteAsync()
        {
            OnExecuteAsyncWasInvoked = true;
            return base.OnExecuteAsync();
        }
    }

    public class CommandStubThrowsErrorsOnExecute : Command
    {
        protected override void OnExecute()
        {
            throw new ServiceException("An error occurred");
        }

        protected override Task OnExecuteAsync()
        {
            throw new ServiceException("An error occurred");
        }
    }

    public class CommandStubOfString : Command<string>
    {
        public CommandStubOfString ()
        {
            Errors = Enumerable.Empty<ValidationResult>();
        }

        public IEnumerable<ValidationResult> Errors { get; set; }
        public bool OnInitializationWasInvoked { get; set; }
        public bool OnExecuteWasInvoked { get; set; }
        public bool OnInitializationAsyncWasInvoked { get; set; }
        public bool OnExecuteAsyncWasInvoked { get; set; }
        public bool OnGetRulesWasInvoked { get; set; }
        public bool OnGetRulesAsyncWasInvoked { get; set; }

        protected override void OnInitialization()
        {
            OnInitializationWasInvoked = true;
            base.OnInitialization();
        }

        protected override Task OnInitializationAsync()
        {
            OnInitializationAsyncWasInvoked = true;
            return base.OnInitializationAsync();
        }

        protected override IEnumerable<ValidationResult> OnGetErrors()
        {
            return Errors;
        }

        protected override Task<IEnumerable<ValidationResult>> OnGetErrorsAsync()
        {
            return Task.FromResult(GetErrors());
        }

        protected override IEnumerable<IRule> OnGetRules()
        {
            OnGetRulesWasInvoked = true;
            return base.OnGetRules();
        }
        protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            OnGetRulesAsyncWasInvoked = true;
            return base.OnGetRulesAsync();
        }

        protected override string OnExecute()
        {
            OnExecuteWasInvoked = true;
            return "some value";
        }

        protected override Task<string> OnExecuteAsync()
        {
            OnExecuteAsyncWasInvoked = true;
            return Task.FromResult("some value");
        }
    }

    public class CommandStubOfStringThrowsErrorsOnExecute : Command<string>
    {
        protected override string OnExecute()
        {
            throw new ServiceException("An error occurred");
        }

        protected override Task<string> OnExecuteAsync()
        {
            throw new ServiceException("An error occurred");
        }
    }
}
