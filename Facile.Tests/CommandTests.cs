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
            mock.Execute();
            mock.OnInitializationInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnExecuteIsInvokedWhenNoErrorsExist()
        {
            var mock = new MockCommand();
            mock.Execute();
            mock.OnExecuteInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnExecuteIsNotInvokedWhenErrorsExist()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            mock.Execute();
            mock.OnExecuteInvoked.ShouldBe(false);
        }

        [Fact]
        public void OnInitializationAsyncIsInvoked()
        {
            var mock = new MockCommand();
            mock.ExecuteAsync().Wait();
            mock.OnInitializationAsyncInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnExecuteAsyncIsInvokedWhenNoErrorsExist()
        {
            var mock = new MockCommand();
            mock.ExecuteAsync().Wait();
            mock.OnExecuteAsyncInvoked.ShouldBe(true);
        }

        [Fact]
        public void OnExecuteAsyncIsNotInvokedWhenErrorsExist()
        {
            var mock = new MockCommand();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            mock.ExecuteAsync().Wait();
            mock.OnExecuteAsyncInvoked.ShouldBe(false);
        }
    }

    public class MockCommand : Command
    {
        public MockCommand()
        {
            Errors = Enumerable.Empty<ValidationResult>();
        }

        public IEnumerable<ValidationResult> Errors { get; set; }
        public bool OnInitializationInvoked { get; set; }
        public bool OnExecuteInvoked { get; set; }
        public bool OnInitializationAsyncInvoked { get; set; }
        public bool OnExecuteAsyncInvoked { get; set; }

        protected override void OnInitialization()
        {
            OnInitializationInvoked = true;
        }

        protected override Task OnInitializationAsync()
        {
            OnInitializationAsyncInvoked = true;
            return Task.Delay(0);
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return Errors;
        }

        protected override void OnExecute()
        {
            OnExecuteInvoked = true;
        }

        protected override Task OnExecuteAsync()
        {
            OnExecuteAsyncInvoked = true;
            return Task.Delay(0);
        }
    }
}
