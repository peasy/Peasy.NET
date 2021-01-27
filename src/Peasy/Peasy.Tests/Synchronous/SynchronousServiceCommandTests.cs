using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Peasy.Synchronous;

namespace Peasy.Tests.Rules
{
    public class SynchronousServiceCommandTests
    {
        private static string _count = string.Empty;

        public SynchronousServiceCommandTests()
        {
            _count = string.Empty;
        }

        private Action initializationMethod = () =>
        {
            _count += "1";
        };

        private Func<IEnumerable<ValidationResult>> validationMethod = () =>
        {
            _count += "2";
            return Enumerable.Empty<ValidationResult>();
        };

        private Func<IEnumerable<ISynchronousRule>> getBusinessRulesMethod = () =>
        {
            _count += "3";
            return Enumerable.Empty<ISynchronousRule>();
        };

        private Action executeMethod = () =>
        {
            _count += "4";
        };

        [Fact]
        public async Task ServiceCommand_Composition_1()
        {
            var command = new SynchronousServiceCommand
            (
                initializationMethod,
                validationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("124");
        }

        [Fact]
        public async Task ServiceCommand_Composition_2()
        {
            var command = new SynchronousServiceCommand
            (
                initializationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("14");
        }

        [Fact]
        public async Task ServiceCommand_Composition_5()
        {
            var command = new SynchronousServiceCommand
            (
                validationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("24");
        }

        [Fact]
        public async Task ServiceCommand_Composition_6()
        {
            var command = new SynchronousServiceCommand
            (
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("4");
        }

        [Fact]
        public async Task ServiceCommand_Composition_7()
        {
            var command = new SynchronousServiceCommand
            (
                validationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("24");
        }

        [Fact]
        public async Task ServiceCommand_Composition_9()
        {
            var command = new SynchronousServiceCommand
            (
                getBusinessRulesMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("34");
        }

        [Fact]
        public async Task ServiceCommand_Composition_10()
        {
            var command = new SynchronousServiceCommand
            (
                getBusinessRulesMethod,
                validationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("234");
        }
    }
}
