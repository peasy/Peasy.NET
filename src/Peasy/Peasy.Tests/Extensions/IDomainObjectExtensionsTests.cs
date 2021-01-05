using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace Peasy.Core.Tests.Extensions
{
    [TestClass]
    public class IDomainObjectExtensionsTests
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
