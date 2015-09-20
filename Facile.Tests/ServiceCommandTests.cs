using Facile.Core;
using Facile.Core.Tests;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Rules
{
    [Trait("Command", "ServiceCommandTests")]
    public class ServiceCommandTests
    {
        [Fact]
        public void ServiceCommandConstructorWithSyncMethodArgsContainsCorrectErrorsOnFailure()
        {
            var command = new ServiceCommand<Person>(
                executeMethod: () => new Person(),
                getBusinessRulesMethod: () => new[] { new FalseRule1() }
            );
            command.Execute().Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void ServiceCommandConstructorWithAsyncMethodArgsContainsCorrectErrorsOnFailure()
        {
            var command = new ServiceCommand<Person>(
                executeAsyncMethod: () => Task.Run(() => new Person()),
                getBusinessRulesAsyncMethod: () => Task.Run<IEnumerable<IRule>>(() => new [] { new FalseRule1() })
            );
            var result = command.ExecuteAsync();
            result.Wait();
            result.Result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [Fact]
        public void ServiceCommandConstructorWithAsyncAndSyncMethodArgsContainsCorrectErrorsOnFailure()
        {
            var command = new ServiceCommand<Person>(
                executeMethod: () => new Person(),
                executeAsyncMethod: () => Task.Run(() => new Person()),
                getBusinessRulesMethod: () => new[] { new FalseRule1() },
                getBusinessRulesAsyncMethod: () => Task.Run<IEnumerable<IRule>>(() => new [] { new FalseRule2() })
            );
            command.Execute().Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            var result = command.ExecuteAsync();
            result.Wait();
            result.Result.Errors.First().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }

        public class Person
        {
        }
    }
}
