using Facile.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Facile.Tests.Rules
{
    class GreaterThanZeroRuleTests
    {
        [Fact]
        public void ReturnsTrue()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(1, "foo");
            Assert.True(greaterThanZeroRule.Validate().IsValid);
        }

        [Fact]
        public void ReturnsFalse()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "foo");
            Assert.False(greaterThanZeroRule.Validate().IsValid);
        }
        
        [Fact]
        public void SetsErrorMessageOnInvalid()
        {
            var greaterThanZeroRule = new GreaterThanZeroRule(0, "the supplied value must be greater than 0");
            greaterThanZeroRule.Validate();
            Assert.Equal("the supplied value must be greater than 0", greaterThanZeroRule.ErrorMessage);
        }

    }
}
