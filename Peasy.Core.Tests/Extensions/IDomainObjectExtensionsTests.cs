using Peasy.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace Peasy.Core.Tests.Extensions
{
    [TestClass]
    public class IDomainObjectExtensionsTests
    {
        [TestMethod]
        public void GetValidationErrorsReturnsNoResults()
        {
            var person = new Person()
            {
                ID = 0,
                First = "Skip",
                Last = "Jones"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(0);
        }

        [TestMethod]
        public void GetValidationErrorsReturnsOneResult()
        {
            var person = new Person()
            {
                ID = -1,
                First = "Skip",
                Last = "Jones"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(1);
        }

        [TestMethod]
        public void GetValidationErrorsReturnsTwoResults()
        {
            var person = new Person()
            {
                ID = -1,
                First = "AnExtremelyLongFirstName",
                Last = "Jones"
            };
            var results = person.GetValidationErrors();
            results.Count().ShouldBe(2);
        }

        [TestMethod]
        public void GetValidationErrorsReturnsThreeResults()
        {
            var person = new Person()
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
