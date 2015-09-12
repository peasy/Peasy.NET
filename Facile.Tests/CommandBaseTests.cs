using Facile.Core;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Facile.Tests.Rules
{
    [Trait("CommandBase", "CommandBase")]
    public class CommandBaseTests
    {
        [Fact]
        public void CanExecuteReturnsTrueWhenNoErrors()
        {
            var mock = new MockCommandBase();
            mock.CanExecute.ShouldBe(true);
        }

        [Fact]
        public void CanExecuteReturnsFalseWhenErrors()
        {
            var mock = new MockCommandBase();
            mock.Errors = new[] { new ValidationResult("Object doesn't exist") };
            mock.CanExecute.ShouldBe(false);
        }
    }

    public class MockCommandBase : CommandBase
    {
        public MockCommandBase()
        {
            Errors = Enumerable.Empty<ValidationResult>();
        }

        public IEnumerable<ValidationResult> Errors { get; set; }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return Errors;
        }
    }
}
