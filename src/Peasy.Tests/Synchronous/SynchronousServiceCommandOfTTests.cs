using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Peasy.Core.Tests;
using Peasy.Synchronous;

namespace Peasy.Tests.Rules
{
    public class SynchronousServiceCommandOfTTests
    {
        private static string _count = string.Empty;

        public SynchronousServiceCommandOfTTests()
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

        private Func<Person> executeMethod = () =>
        {
            _count += "4";
            return default(Person);
        };

        [Fact]
        public void ServiceCommandOfT_Composition_1()
        {
            var command = new SynchronousServiceCommand<Person>
            (
                initializationMethod,
                validationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("124");
        }

        [Fact]
        public void ServiceCommandOfT_Composition_100()
        {
            var command = new SynchronousServiceCommand<Person>
            (
                initializationMethod,
                getBusinessRulesMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("134");
        }

        [Fact]
        public void ServiceCommandOfT_Composition_2()
        {

            var command = new SynchronousServiceCommand<Person>
            (
                initializationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("14");
        }

        [Fact]
        public void ServiceCommandOfT_Composition_5()
        {
            var command = new SynchronousServiceCommand<Person>
            (
                validationMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("24");
        }

        [Fact]
        public void ServiceCommandOfT_Composition_6()
        {
            var command = new SynchronousServiceCommand<Person>
            (
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("4");
        }

        [Fact]
        public void ServiceCommandOfT_Composition_9()
        {
            var command = new SynchronousServiceCommand<Person>
            (
                getBusinessRulesMethod,
                executeMethod
            );

            command.Execute();

            _count.ShouldBe("34");
        }
    }
}
