using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Peasy.Tests.Rules
{
    public class ServiceCommandTests
    {
        private static string _count = string.Empty;

        public ServiceCommandTests()
        {
            _count = string.Empty;
        }

        private Func<Task> initializationAsyncMethod = () =>
        {
            _count += "1";
            return Task.FromResult<object>(null);
        };

        private Func<Task<IEnumerable<ValidationResult>>> validationAsyncMethod = () =>
        {
            _count += "2";
            return Task.FromResult(Enumerable.Empty<ValidationResult>());
        };

        private Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod = () =>
        {
            _count += "3";
            return Task.FromResult(Enumerable.Empty<IRule>());
        };

        private Func<Task> executeAsyncMethod = () =>
        {
            _count += "4";
            return Task.FromResult<object>(null);
        };

        [Fact]
        public async Task ServiceCommand_Composition_1()
        {
            var command = new ServiceCommand
            (
                initializationAsyncMethod,
                validationAsyncMethod,
                executeAsyncMethod
            );

            await command.ExecuteAsync();

            _count.ShouldBe("124");
        }

        [Fact]
        public async Task ServiceCommand_Composition_2()
        {

            var command = new ServiceCommand
            (
                initializationAsyncMethod,
                executeAsyncMethod
            );

            await command.ExecuteAsync();

            _count.ShouldBe("14");
        }

        [Fact]
        public async Task ServiceCommand_Composition_5()
        {
            var command = new ServiceCommand
            (
                validationAsyncMethod,
                executeAsyncMethod
            );

            await command.ExecuteAsync();

            _count.ShouldBe("24");
        }

        [Fact]
        public async Task ServiceCommand_Composition_6()
        {
            var command = new ServiceCommand
            (
                executeAsyncMethod
            );

            await command.ExecuteAsync();

            _count.ShouldBe("4");
        }

        [Fact]
        public async Task ServiceCommand_Composition_9()
        {
            var command = new ServiceCommand
            (
                getBusinessRulesAsyncMethod,
                executeAsyncMethod
            );

            await command.ExecuteAsync();

            _count.ShouldBe("34");
        }

        [Fact]
        public async Task ServiceCommand_Composition_13()
        {
            var command = new ServiceCommand
            (
                initializationAsyncMethod,
                getBusinessRulesAsyncMethod,
                executeAsyncMethod
            );

            await command.ExecuteAsync();

            _count.ShouldBe("134");
        }
    }
}
