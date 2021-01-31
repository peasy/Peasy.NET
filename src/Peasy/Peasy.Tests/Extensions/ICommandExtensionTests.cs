using Moq;
using Peasy.Synchronous;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Peasy.Extensions;
using System;

namespace Peasy.Core.Tests.Extensions
{
    public class ICommandExtensions
    {
        [Fact]
        public async Task ValidateAsync_Supports_ICommand()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandBaseTests.CommandStub(doerOfThings.Object) as ICommand;
            var result = await command.ValidateAsync();

            result.CanContinue.ShouldBeTrue();
        }

        [Fact]
        public async Task ValidateAsync_Throws_Exception_When_ICommand_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ICommand>();

            var ex = await Assert.ThrowsAsync<InvalidCastException>(() => command.Object.ValidateAsync());
            ex.Message.ShouldBe("Supplied command does not implement ISupportCommandValidation interface.");
        }

        [Fact]
        public async Task ValidateAsync_Supports_ICommand_Of_T()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new CommandBaseTestsOfT.CommandStub(doerOfThings.Object) as ICommand<string>;
            var result = await command.ValidateAsync();

            result.CanContinue.ShouldBeTrue();
        }

        [Fact]
        public async Task ValidateAsync_Throws_Exception_When_ICommand_Of_T_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ICommand<string>>();

            var ex = await Assert.ThrowsAsync<InvalidCastException>(() => command.Object.ValidateAsync());
            ex.Message.ShouldBe("Supplied command does not implement ISupportCommandValidation interface.");
        }

        [Fact]
        public void Validate_Supports_ISynchronousCommand()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new SynchronousCommandBaseTests.SynchronousCommandStub(doerOfThings.Object) as ISynchronousCommand;
            var result = command.Validate();

            result.CanContinue.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Throws_Exception_When_ISynchronousCommand_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ISynchronousCommand>();

            var ex = Assert.Throws<InvalidCastException>(() => command.Object.Validate());
            ex.Message.ShouldBe("Supplied command does not implement ISupportSynchronousCommandValidation interface.");
        }

        [Fact]
        public void Validate_Supports_ISynchronousCommand_Of_T()
        {
            var doerOfThings = new Mock<IDoThings>();
            var command = new SynchronousCommandBaseTestsOfT.SynchronousCommandStub(doerOfThings.Object) as ISynchronousCommand<string>;
            var result = command.Validate();

            result.CanContinue.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Throws_Exception_When_ISynchronousCommand_Of_T_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ISynchronousCommand<string>>();

            var ex = Assert.Throws<InvalidCastException>(() => command.Object.Validate());
            ex.Message.ShouldBe("Supplied command does not implement ISupportSynchronousCommandValidation interface.");
        }

        [Fact]
        public async Task GetRulesAsync_Supports_ICommand()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandBaseTests.CommandStub(doerOfThings.Object, rules) as ICommand;
            var results = await command.GetRulesAsync();

            results.Count().ShouldBe(2);
            results.First().ShouldBeOfType<TrueRule>();
            results.Second().ShouldBeOfType<FalseRule1>();
        }

        [Fact]
        public async Task GetRulesAsync_Throws_Exception_When_ICommand_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ICommand>();

            var ex = await Assert.ThrowsAsync<InvalidCastException>(() => command.Object.GetRulesAsync());
            ex.Message.ShouldBe("Supplied command does not implement IRulesContainer interface.");
        }

        [Fact]
        public async Task GetRulesAsync_Supports_ICommand_Of_T()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new IRule[] { new TrueRule(), new FalseRule1() };
            var command = new CommandBaseTestsOfT.CommandStub(doerOfThings.Object, rules) as ICommand<string>;
            var results = await command.GetRulesAsync();

            results.Count().ShouldBe(2);
            results.First().ShouldBeOfType<TrueRule>();
            results.Second().ShouldBeOfType<FalseRule1>();
        }

        [Fact]
        public async Task GetRulesAsync_Throws_Exception_When_ICommand_Of_T_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ICommand<string>>();

            var ex = await Assert.ThrowsAsync<InvalidCastException>(() => command.Object.ValidateAsync());
            ex.Message.ShouldBe("Supplied command does not implement ISupportCommandValidation interface.");
        }

        [Fact]
        public void GetRules_Supports_ISynchronousCommand()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new ISynchronousRule[] { new SynchronousTrueRule(), new SynchronousFalseRule1() };
            var command = new SynchronousCommandBaseTests.SynchronousCommandStub(doerOfThings.Object, rules) as ISynchronousCommand;
            var results = command.GetRules();

            results.Count().ShouldBe(2);
            results.First().ShouldBeOfType<SynchronousTrueRule>();
            results.Second().ShouldBeOfType<SynchronousFalseRule1>();
        }

        [Fact]
        public void GetRules_Throws_Exception_When_ISynchronousCommand_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ISynchronousCommand>();

            var ex = Assert.Throws<InvalidCastException>(() => command.Object.Validate());
            ex.Message.ShouldBe("Supplied command does not implement ISupportSynchronousCommandValidation interface.");
        }

        [Fact]
        public void GetRules_Supports_ISynchronousCommand_Of_T()
        {
            var doerOfThings = new Mock<IDoThings>();
            var rules = new ISynchronousRule[] { new SynchronousTrueRule(), new SynchronousFalseRule1() };
            var command = new SynchronousCommandBaseTestsOfT.SynchronousCommandStub(doerOfThings.Object, rules) as ISynchronousCommand<string>;
            var results = command.GetRules();

            results.Count().ShouldBe(2);
            results.First().ShouldBeOfType<SynchronousTrueRule>();
            results.Second().ShouldBeOfType<SynchronousFalseRule1>();
        }

        [Fact]
        public void GetRules_Throws_Exception_When_ISynchronousCommand_Of_T_Does_Not_Implement_Expected_Interface()
        {
            var command = new Mock<ISynchronousCommand<string>>();

            var ex = Assert.Throws<InvalidCastException>(() => command.Object.Validate());
            ex.Message.ShouldBe("Supplied command does not implement ISupportSynchronousCommandValidation interface.");
        }
    }
}
