using Peasy.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using Peasy.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Peasy.Tests.Extensions
{
    [TestClass]
    public class DomainObjectExtensionsTests
    {
        [TestMethod]
        public void ClassName_Should_Return_Class_Name_Without_Peasy_DisplayNameAttribute()
        {
            var className = new MockClass().ClassName();
            className.ShouldBe("MockClass");
        }

        [TestMethod]
        public void ClassName_Should_Return_Name_Of_Peasy_DisplayNameAttribute()
        {
            var className = new MockClass2().ClassName();
            className.ShouldBe("Mock II");
        }

        [TestMethod]
        public void Object_With_Peasy_ForeignKeyAttribute_Should_Revert_Zeros_To_Nulls()
        {
            var mock = new MockClass2();
            mock.SomeForeignKeyID = 0;
            mock.RevertForeignKeysFromZeroToNull();
            mock.SomeForeignKeyID.ShouldBe(null);
        }

        [TestMethod]
        public void Object_With_NonEditableAttribute_Should_Revert_Values_To_Original()
        {
            var original = new MockClass2() { Name = "Jimi Hendrix" };
            var newMock = new MockClass2() { Name = "Jim Morrison" };
            newMock.RevertNonEditableValues(original);
            newMock.Name.ShouldBe("Jimi Hendrix");
        }

        [TestMethod]
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
