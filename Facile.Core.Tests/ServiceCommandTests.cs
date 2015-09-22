using Facile.Core;
using Facile.Core.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facile.Tests.Rules
{
    [TestClass]
    public class ServiceCommandTests
    {
        [TestMethod]
        public void ServiceCommandConstructorWithSyncMethodArgsContainsCorrectErrorsOnFailure()
        {
            var command = new ServiceCommand<Person>(
                executeMethod: () => new Person(),
                getBusinessRulesMethod: () => new[] { new FalseRule1() }
            );
            command.Execute().Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public async Task ServiceCommandConstructorWithAsyncMethodArgsContainsCorrectErrorsOnFailure()
        {
            var command = new ServiceCommand<Person>(
                executeAsyncMethod: () => Task.Run(() => new Person()),
                getBusinessRulesAsyncMethod: () => Task.Run<IEnumerable<IRule>>(() => new [] { new FalseRule1() })
            );
            var result = await command.ExecuteAsync();
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
        }

        [TestMethod]
        public async Task ServiceCommandConstructorWithAsyncAndSyncMethodArgsContainsCorrectErrorsOnFailure()
        {
            var command = new ServiceCommand<Person>(
                executeMethod: () => new Person(),
                executeAsyncMethod: () => Task.Run(() => new Person()),
                getBusinessRulesMethod: () => new[] { new FalseRule1() },
                getBusinessRulesAsyncMethod: () => Task.Run<IEnumerable<IRule>>(() => new [] { new FalseRule2() })
            );
            command.Execute().Errors.First().ErrorMessage.ShouldBe("FalseRule1 failed validation");
            var result = await command.ExecuteAsync();
            result.Errors.First().ErrorMessage.ShouldBe("FalseRule2 failed validation");
        }
    }
}
