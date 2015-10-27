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
        public void ClassNameShouldReturnClassNameWithoutPeasyDisplayNameAttribute()
        {
            var className = new MockClass().ClassName();
            className.ShouldBe("MockClass");
        }

        [TestMethod]
        public void ClassNameShouldReturnNameOfPeasyDisplayNameAttribute()
        {
            var className = new MockClass2().ClassName();
            className.ShouldBe("Mock II");
        }

        [TestMethod]
        public void ObjectWithPeasyForeignKeyAttributeShouldRevertZerosToNulls()
        {
            var mock = new MockClass2();
            mock.SomeForeignKeyID = 0;
            mock.RevertForeignKeysFromZeroToNull();
            mock.SomeForeignKeyID.ShouldBe(null);
        }

        [TestMethod]
        public void ObjectWithNonEditableAttributeShouldRevertValuesToOriginal()
        {
            var original = new MockClass2() { Name = "Jimi Hendrix" };
            var newMock = new MockClass2() { Name = "Jim Morrison" };
            newMock.RevertNonEditableValues(original);
            newMock.Name.ShouldBe("Jimi Hendrix");
        }

        [TestMethod]
        public async Task TenObjectsWithNonEditableAttributesShouldRevertValuesToOriginal()
        {
            var original = new MockClass2() { Name = "Jimi Hendrix" };
            var newMocks = 10.Times(i => new MockClass2() { Name = string.Format("Jim Morrison{0}", i) }).ToArray();
            var tasks = newMocks.Select(x => Task.Run(() => x.RevertNonEditableValues(original)));
            await Task.WhenAll(tasks);
            newMocks.ShouldAllBe(m => m.Name == "Jimi Hendrix");
        }
    }

    public static class IntExtensions
    {
        public static IEnumerable<T> Times<T>(this int value, Func<int, T> func)
        {
            for (var counter = 0; counter < value; counter ++)
            {
                yield return func(counter);
            }
        }
    }

    public class MockClass
    {
    }

    [PeasyDisplayName("Mock II")]
    public class MockClass2
    {
        public int ID { get; set; }

        [PeasyForeignKey]
        public int? SomeForeignKeyID { get; set; }

        [Editable(false)]
        public string Name { get; set; }
    }
}
