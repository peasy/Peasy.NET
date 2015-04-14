using Facile.Rules;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Rules
{
    public class DomainObjectMustContainIDRuleTests
    {
        [Fact]
        public void ReturnsTrue()
        {
            var entity = new Mock<DomainBase>();
            entity.SetupGet(d => d.ID).Returns(1);
            var rule = new DomainObjectMustContainIDRule(entity.Object);
            Assert.True(rule.Validate().IsValid);
        }

        [Fact]
        public void ReturnsFalse()
        {
            var entity = new Mock<DomainBase>().Object;
            var rule = new DomainObjectMustContainIDRule(entity);
            Assert.False(rule.Validate().IsValid);
        }

        [Fact]
        public void SetsErrorMessageOnInvalid()
        {
            var entity = new Mock<DomainBase>().Object;
            var rule = new DomainObjectMustContainIDRule(entity);
            Assert.NotEqual("", rule.Validate().ErrorMessage);
        }
    }
}
