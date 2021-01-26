using Peasy.Attributes;
using Shouldly;
using Peasy.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Peasy.Core.Tests;

namespace Peasy.Tests.Extensions
{
    public class DomainObjectExtensionsTests
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
            var results = person.Validate();
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
            var results = person.Validate();
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
            var results = person.Validate();
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
            var results = person.Validate();
            results.Count().ShouldBe(3);
        }

        [Fact]
        public void ClassName_Should_Return_Class_Name_Without_Peasy_DisplayNameAttribute()
        {
            var className = new StubClass().ClassName();
            className.ShouldBe("StubClass");
        }

        [Fact]
        public void ClassName_Should_Return_Name_Of_Peasy_DisplayNameAttribute()
        {
            var className = new StubClass2().ClassName();
            className.ShouldBe("Stub II");
        }

        [Fact]
        public void Object_With_Peasy_ForeignKeyAttribute_in_base_class_Should_Revert_Zeros_To_Nulls()
        {
            var stub = new StubClass2 {ForeignKeyID = 0};
            stub.RevertForeignKeysFromZeroToNull();
            stub.ForeignKeyID.ShouldBe(null);
        }

        [Fact]
        public void Object_With_Peasy_ForeignKeyAttribute_Should_Revert_Zeros_To_Nulls()
        {
            var stub = new StubClass2 {SomeForeignKeyID = 0};
            stub.RevertForeignKeysFromZeroToNull();
            stub.SomeForeignKeyID.ShouldBe(null);
        }

        [Fact]
        public void Object_With_NonEditableAttribute_Should_Revert_Values_To_Original()
        {
            var original = new StubClass2 { Name = "Jimi Hendrix" };
            var newStub = new StubClass2 { Name = "Jim Morrison" };
            newStub.RevertNonEditableValues(original);
            newStub.Name.ShouldBe("Jimi Hendrix");
        }

        [Fact]
        public void Object_With_NonEditableAttribute_in_base_class_Should_Revert_Values_To_Original()
        {
            var original = new StubClass2 { SomeValue = "Jimi Hendrix" };
            var newStub = new StubClass2 { SomeValue = "Jim Morrison" };
            newStub.RevertNonEditableValues(original);
            newStub.SomeValue.ShouldBe("Jimi Hendrix");
        }

        [Fact]
        public void Fifty_Objects_With_NonEditableAttributes_Should_Revert_Values_To_Original()
        {
            var original = new StubClass2 { Name = "Jimi Hendrix" };
            var newStubs = 50.Times(i => new StubClass2 { Name = $"Jim Morrison{i}"}).ToArray();
            Parallel.ForEach(newStubs, stub => stub.RevertNonEditableValues(original));
            newStubs.ShouldAllBe(m => m.Name == "Jimi Hendrix");
        }
    }


    public class StubClass
    {
        [Editable(false)]
        public string SomeValue { get; set; }

        [PeasyForeignKey]
        public int? ForeignKeyID { get; set; }
    }

    [PeasyDisplayName("Stub II")]
    public class StubClass2 : StubClass
    {
        public int ID { get; set; }

        [PeasyForeignKey]
        public int? SomeForeignKeyID { get; set; }

        [Editable(false)]
        public string Name { get; set; }
    }
}