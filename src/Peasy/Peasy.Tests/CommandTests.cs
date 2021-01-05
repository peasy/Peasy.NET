using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy.Core.Tests
{
    [TestClass]
    public class CommandTests
    {
        #region Command

        [TestMethod]
        public void OnInitialization_Is_Invoked()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            mock.OnInitializationWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void OnExecute_Is_Invoked_When_No_Errors_Exist()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void ExecutionResult_Is_Successful_When_Validation_Is_Successful()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            result.Success.ShouldBe(true);
        }

        [TestMethod]
        public void ExecutionResult_Should_Contain_No_Errors_When_Validation_Is_Successful()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            result.Errors.ShouldBe(null);
        }

        [TestMethod]
        public void OnExecute_Is_Not_Invoked_When_Errors_Exist()
        {
            var mock = new MockCommand {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(false);
        }

        [TestMethod]
        public void ExecutionResult_Is_Not_Successful_When_Validation_Is_Not_Successful()
        {
            var mock = new MockCommand {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            var result = mock.Execute();
            result.Success.ShouldBe(false);
        }

        [TestMethod]
        public void ExecutionResult_Should_Contain_Errors_When_Validation_Is_Not_Successful()
        {
            var mock = new MockCommand {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            var result = mock.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught()
        {
            var command = new MockCommandThrowsErrorsOnExecute();
            var result = command.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task OnInitializationAsync_Is_Invoked()
        {
            var mock = new MockCommand();
            await mock.ExecuteAsync();
            mock.OnInitializationAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task OnExecuteAsync_Is_Invoked_When_No_Errors_Exist()
        {
            var mock = new MockCommand();
            await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(true);
       }

        [TestMethod]
        public async Task OnExecuteAsync_Is_Not_Invoked_When_Errors_Exist()
        {
            var mock = new MockCommand {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(false);
        }

        [TestMethod]
        public async Task ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught_Async()
        {
            var command = new MockCommandThrowsErrorsOnExecute();
            var result = await command.ExecuteAsync();
            result.Errors.Count().ShouldBe(1);
        }

        #endregion

        #region Command<T>

        [TestMethod]
        public void Command_of_T_OnInitialization_Is_Invoked()
        {
            var mock = new MockCommandOfString();
            var result = mock.Execute();
            mock.OnInitializationWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void Command_of_T_OnExecute_Is_Invoked_When_No_Errors_Exist()
        {
            var mock = new MockCommandOfString();
            var result = mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void Command_of_T_Execute_Returns_Value_When_No_Errors_Exist()
        {
            var mock = new MockCommandOfString();
            var result = mock.Execute();
            result.Value.ShouldBe("some value");
        }

        [TestMethod]
        public void Command_of_T_ExecutionResult_Is_Successful_When_Validation_Is_Successful()
        {
            var mock = new MockCommandOfString();
            var result = mock.Execute();
            result.Success.ShouldBe(true);
        }

        [TestMethod]
        public void Command_of_T_ExecutionResult_Should_Contain_No_Errors_When_Validation_Is_Successful()
        {
            var mock = new MockCommandOfString();
            var result = mock.Execute();
            result.Errors.ShouldBe(null);
        }

        [TestMethod]
        public void Command_of_T_OnExecute_Is_Not_Invoked_When_Errors_Exist()
        {
            var mock = new MockCommandOfString {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(false);
        }

        [TestMethod]
        public void Command_of_T_ExecutionResult_Is_Not_Successful_When_Validation_Is_Not_Successful()
        {
            var mock = new MockCommandOfString {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            var result = mock.Execute();
            result.Success.ShouldBe(false);
        }

        [TestMethod]
        public void Command_of_T_ExecutionResult_Should_Contain_Errors_When_Validation_Is_Not_Successful()
        {
            var mock = new MockCommandOfString {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            var result = mock.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Command_of_T_ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught()
        {
            var command = new MockCommandOfStringThrowsErrorsOnExecute();
            var result = command.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task Command_of_T_OnInitializationAsync_Is_Invoked()
        {
            var mock = new MockCommandOfString();
            await mock.ExecuteAsync();
            mock.OnInitializationAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task Command_of_T_OnExecuteAsync_Is_Invoked_When_No_Errors_Exist()
        {
            var mock = new MockCommandOfString();
            await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(true);
       }

        [TestMethod]
        public async Task Command_of_T_Execute_Returns_Value_When_No_Errors_Exist_Async()
        {
            var mock = new MockCommandOfString();
            var result = await mock.ExecuteAsync();
            result.Value.ShouldBe("some value");
        }

        [TestMethod]
        public async Task Command_of_T_OnExecuteAsync_Is_Not_Invoked_When_Errors_Exist()
        {
            var mock = new MockCommandOfString {Errors = new[] {new ValidationResult("Object doesn't exist")}};
            await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(false);
        }

        [TestMethod]
        public async Task Command_of_T_ExecutionResult_Should_Contain_Errors_When_ServiceException_Is_Caught_Async()
        {
            var command = new MockCommandOfStringThrowsErrorsOnExecute();
            var result = await command.ExecuteAsync();
            result.Errors.Count().ShouldBe(1);
        }

        #endregion
    }

    public class MockCommand : Command
    {
        public MockCommand()
        {
            Errors = Enumerable.Empty<ValidationResult>();
        }

        public IEnumerable<ValidationResult> Errors { get; set; }
        public bool OnInitializationWasInvoked { get; set; }
        public bool OnExecuteWasInvoked { get; set; }
        public bool OnInitializationAsyncWasInvoked { get; set; }
        public bool OnExecuteAsyncWasInvoked { get; set; }

        protected override void OnInitialization()
        {
            OnInitializationWasInvoked = true;
        }

        protected override Task OnInitializationAsync()
        {
            OnInitializationAsyncWasInvoked = true;
            return Task.FromResult(true);
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return Errors;
        }

        public override Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return Task.FromResult(GetErrors());
        }

        protected override void OnExecute()
        {
            OnExecuteWasInvoked = true;
        }

        protected override Task OnExecuteAsync()
        {
            OnExecuteAsyncWasInvoked = true;
            return Task.FromResult(true);
        }
    }

    public class MockCommandThrowsErrorsOnExecute : Command
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

    public class MockCommandOfString : Command<string>
    {
        public MockCommandOfString ()
        {
            Errors = Enumerable.Empty<ValidationResult>();
        }

        public IEnumerable<ValidationResult> Errors { get; set; }
        public bool OnInitializationWasInvoked { get; set; }
        public bool OnExecuteWasInvoked { get; set; }
        public bool OnInitializationAsyncWasInvoked { get; set; }
        public bool OnExecuteAsyncWasInvoked { get; set; }

        protected override void OnInitialization()
        {
            OnInitializationWasInvoked = true;
        }

        protected override Task OnInitializationAsync()
        {
            OnInitializationAsyncWasInvoked = true;
            return Task.FromResult(true);
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return Errors;
        }

        public override Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return Task.FromResult(GetErrors());
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

    public class MockCommandOfStringThrowsErrorsOnExecute : Command<string>
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
