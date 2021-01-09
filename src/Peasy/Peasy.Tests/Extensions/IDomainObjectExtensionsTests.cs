using Shouldly;
using System.Linq;
using Xunit;

namespace Peasy.Core.Tests.Extensions
{
    public class IDomainObjectExtensionsTests
    {
        [Fact]
        public void GetValidationErrors_returns_no_results()
        {
            var person = new Person
            {
                ID = 0,
                First = "Skip",
                Last = "Jones"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(0);
        }

        [Fact]
        public void GetValidationErrors_returns_one_result()
        {
            var person = new Person
            {
                ID = -1,
                First = "Skip",
                Last = "Jones"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(1);
        }

        [Fact]
        public void GetValidationErrors_returns_two_results()
        {
            var person = new Person
            {
                ID = -1,
                First = "AnExtremelyLongFirstName",
                Last = "Jones"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(2);
        }

        [Fact]
        public void GetValidationErrors_returns_three_results()
        {
            var person = new Person
            {
                ID = -1,
                First = "AnExtremelyLongFirstName",
                Last = "AnExtremelyLongLastNameAnExtremelyLongLastName"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(3);
        }
    }
}
