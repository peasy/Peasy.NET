using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Peasy.Core.Tests;

namespace Peasy.Tests.Rules
{
    public class ServiceCommandOfTTests
    {
        private static string _count = string.Empty;

        public ServiceCommandOfTTests()
        {
            _count = string.Empty;
        }

        private Action initializationMethod = () =>
        {
            _count += "1";
        };

        private Func<Task> initializationAsyncMethod = () =>
        {
            _count += "1";
            return Task.FromResult<object>(null);
        };

        private Func<IEnumerable<ValidationResult>> getErrorsMethod = () =>
        {
            _count += "2";
            return Enumerable.Empty<ValidationResult>();
        };

        private Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod = () =>
        {
            _count += "2";
            return Task.FromResult(Enumerable.Empty<ValidationResult>());
        };

        private Func<IEnumerable<IRule>> getBusinessRulesMethod = () =>
        {
            _count += "3";
            return Enumerable.Empty<IRule>();
        };

        private Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod = () =>
        {
            _count += "3";
            return Task.FromResult(Enumerable.Empty<IRule>());
        };

        private Func<Person> executeMethod = () =>
        {
            _count += "4";
            return default(Person);
        };

        private Func<Task<Person>> executeAsyncMethod = () =>
        {
            _count += "4";
            return Task.FromResult(default(Person));
        };

        [Fact]
        public async Task ServiceCommandOfT_Composition_1()
        {
            var command = new ServiceCommand<Person>
            (
                initializationMethod,
                initializationAsyncMethod,
                getErrorsMethod,
                getErrorsAsyncMethod,
                executeMethod,
                executeAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("124124");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_2()
        {

            var command = new ServiceCommand<Person>
            (
                initializationMethod,
                initializationAsyncMethod,
                executeMethod,
                executeAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("1414");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_3()
        {
            var command = new ServiceCommand<Person>
            (
                initializationMethod,
                executeMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("14");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_4()
        {
            var command = new ServiceCommand<Person>
            (
                initializationAsyncMethod,
                executeAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("14");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_5()
        {
            var command = new ServiceCommand<Person>
            (
                getErrorsMethod,
                getErrorsAsyncMethod,
                executeMethod,
                executeAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("2424");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_6()
        {
            var command = new ServiceCommand<Person>
            (
                executeMethod,
                executeAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("44");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_7()
        {
            var command = new ServiceCommand<Person>
            (
                executeMethod,
                getErrorsMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("24");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_8()
        {
            var command = new ServiceCommand<Person>
            (
                executeAsyncMethod,
                getErrorsAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("24");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_9()
        {
            var command = new ServiceCommand<Person>
            (
                executeMethod,
                executeAsyncMethod,
                getBusinessRulesMethod,
                getBusinessRulesAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("3434");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_10()
        {
            var command = new ServiceCommand<Person>
            (
                executeMethod,
                getBusinessRulesMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("34");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_11()
        {
            var command = new ServiceCommand<Person>
            (
                executeAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("4");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_12()
        {
            var command = new ServiceCommand<Person>
            (
                executeAsyncMethod,
                getBusinessRulesAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("34");
        }

        [Fact]
        public async Task ServiceCommandOfT_Composition_13()
        {
            var command = new ServiceCommand<Person>
            (
                initializationAsyncMethod,
                executeAsyncMethod,
                getBusinessRulesAsyncMethod
            );

            command.Execute();
            await command.ExecuteAsync();

            _count.ShouldBe("134");
        }
    }
}
