using Peasy.Attributes;
using Shouldly;
using Peasy.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Tests.Extensions
{
    public class DomainObjectExtensionsTests
    {
        [Fact]
        public void ClassName_Should_Return_Class_Name_Without_Peasy_DisplayNameAttribute()
        {
            var className = new MockClass().ClassName();
            className.ShouldBe("MockClass");
        }

        [Fact]
        public void ClassName_Should_Return_Name_Of_Peasy_DisplayNameAttribute()
        {
            var className = new MockClass2().ClassName();
            className.ShouldBe("Mock II");
        }

        [Fact]
        public void Object_With_Peasy_ForeignKeyAttribute_in_base_class_Should_Revert_Zeros_To_Nulls()
        {
            var mock = new MockClass2();
            mock.ForeignKeyID = 0;
            mock.RevertForeignKeysFromZeroToNull();
            mock.ForeignKeyID.ShouldBe(null);
        }

        [Fact]
        public void Object_With_Peasy_ForeignKeyAttribute_Should_Revert_Zeros_To_Nulls()
        {
            var mock = new MockClass2();
            mock.SomeForeignKeyID = 0;
            mock.RevertForeignKeysFromZeroToNull();
            mock.SomeForeignKeyID.ShouldBe(null);
        }

        [Fact]
        public void Object_With_NonEditableAttribute_Should_Revert_Values_To_Original()
        {
            var original = new MockClass2() { Name = "Jimi Hendrix" };
            var newMock = new MockClass2() { Name = "Jim Morrison" };
            newMock.RevertNonEditableValues(original);
            newMock.Name.ShouldBe("Jimi Hendrix");
        }

        [Fact]
        public void Object_With_NonEditableAttribute_in_base_class_Should_Revert_Values_To_Original()
        {
            var original = new MockClass2() { SomeValue = "Jimi Hendrix" };
            var newMock = new MockClass2() { SomeValue = "Jim Morrison" };
            newMock.RevertNonEditableValues(original);
            newMock.SomeValue.ShouldBe("Jimi Hendrix");
        }

        [Fact]
        public void Fifty_Objects_With_NonEditableAttributes_Should_Revert_Values_To_Original()
        {
            var original = new MockClass2() { Name = "Jimi Hendrix" };
            var newMocks = 50.Times(i => new MockClass2() { Name = string.Format("Jim Morrison{0}", i) }).ToArray();
            Parallel.ForEach(newMocks, mock => mock.RevertNonEditableValues(original));
            newMocks.ShouldAllBe(m => m.Name == "Jimi Hendrix");
        }
    }


    public class MockClass
    {
        [Editable(false)]
        public string SomeValue { get; set; }

        [PeasyForeignKey]
        public int? ForeignKeyID { get; set; }
    }

    [PeasyDisplayName("Mock II")]
    public class MockClass2 : MockClass
    {
        public int ID { get; set; }

        [PeasyForeignKey]
        public int? SomeForeignKeyID { get; set; }

        [Editable(false)]
        public string Name { get; set; }
    }
}
