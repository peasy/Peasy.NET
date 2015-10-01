using Peasy.Core;
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
        [TestMethod]
        public void OnInitializationIsInvoked()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            mock.OnInitializationWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void OnExecuteIsInvokedWhenNoErrorsExist()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void ExecutionResultIsSuccessfulWhenValidationIsSuccessful()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            result.Success.ShouldBe(true); 
        }

        [TestMethod]
        public void ExecutionResultShouldContainNoErrorsWhenValidationIsSuccessful()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            result.Errors.ShouldBe(null);
        }

        [TestMethod]
        public void OnExecuteIsNotInvokedWhenErrorsExist()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(false);
        }

        [TestMethod]
        public void ExecutionResultIsNotSuccessfulWhenValidationIsNotSuccessful()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            var result = mock.Execute();
            result.Success.ShouldBe(false); 
        }

        [TestMethod]
        public void ExecutionResultShouldContainErrorsWhenValidationIsNotSuccessful()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            var result = mock.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [TestMethod]
        public async Task OnInitializationAsyncIsInvoked()
        {
            var mock = new MockCommand();
            await mock.ExecuteAsync();
            mock.OnInitializationAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task OnExecuteAsyncIsInvokedWhenNoErrorsExist()
        {
            var mock = new MockCommand();
            await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(true);
       } 

        [TestMethod]
        public async Task OnExecuteAsyncIsNotInvokedWhenErrorsExist()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(false);
        }
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
            return Task.Run(() => GetErrors());
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
}
