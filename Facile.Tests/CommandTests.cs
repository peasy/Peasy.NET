using Facile.Core;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Rules
{
    [Trait("Command", "Command")]
    public class CommandTests
    {
        [Fact]
        public void OnInitializationIsInvoked()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            mock.OnInitializationWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnExecuteIsInvokedWhenNoErrorsExist()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void ExecutionResultIsSuccessfulWhenValidationIsSuccessful()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            result.Success.ShouldBe(true); 
        }

        [Fact]
        public void ExecutionResultShouldContainNoErrorsWhenValidationIsSuccessful()
        {
            var mock = new MockCommand();
            var result = mock.Execute();
            result.Errors.ShouldBe(null);
        }

        [Fact]
        public void OnExecuteIsNotInvokedWhenErrorsExist()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            mock.Execute();
            mock.OnExecuteWasInvoked.ShouldBe(false);
        }

        [Fact]
        public void ExecutionResultIsNotSuccessfulWhenValidationIsNotSuccessful()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            var result = mock.Execute();
            result.Success.ShouldBe(false); 
        }

        [Fact]
        public void ExecutionResultShouldContainErrorsWhenValidationIsNotSuccessful()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            var result = mock.Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public void OnInitializationAsyncIsInvoked()
        {
            var mock = new MockCommand();
            var x = mock.ExecuteAsync();
            x.ContinueWith((r) => mock.OnInitializationAsyncWasInvoked.ShouldBe(true));
        }

        [Fact]
        public void OnExecuteAsyncIsInvokedWhenNoErrorsExist()
        {
            var mock = new MockCommand();
            mock.ExecuteAsync().Wait();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task OnExecuteAsyncIsInvokedWhenNoErrorsExistA()
        {
            var mock = new MockCommand();
            var x = await mock.ExecuteAsync();
            mock.OnExecuteAsyncWasInvoked.ShouldBe(true);
        }

        //[Fact]
        //public void OnExecuteAsyncIsNotInvokedWhenErrorsExist()
        //{
        //    var mock = new MockCommand();
        //    mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
        //    mock.ExecuteAsync().Wait();
        //    mock.OnExecuteAsyncWasInvoked.ShouldBe(false);
        //}
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
