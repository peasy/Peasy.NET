using Facile.Rules;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq.Protected;

namespace Facile.Tests.Rules
{
    [Trait("Rules", "RuleBase")]
    public class RuleBaseTests
    {
        [Fact] 
        public void SuccessorExecutesOnTrue()
        {
            var entity = new Mock<DomainBase>().Object;
            var rule = new GreaterThanZeroRule(1, "foo")
                               .IfValidThenValidate(new DomainObjectMustContainIDRule(entity));
            Assert.Equal("Domain object must contain an id value greater than 0", rule.Validate().ErrorMessage);
        }

        [Fact] 
        public void SuccessorDoesNotExecuteOnFalse()
        {
            var entity = new Mock<DomainBase>().Object;
            var rule = new GreaterThanZeroRule(0, "field must be greater than zero")
                               .IfValidThenValidate(new DomainObjectMustContainIDRule(entity));
            Assert.Equal("field must be greater than zero", rule.Validate().ErrorMessage);
        }
        
        [Fact] 
        public void ThreeRuleChainExecutesSuccessfully()
        {
            var entity = BuildEntityMock(1, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var entity2 = BuildEntityMock(2, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });

            var rule = new GreaterThanZeroRule(1, "foo")
                           .IfValidThenValidate(new DomainObjectMustContainIDRule(entity)
                                                    .IfValidThenValidate(new ConcurrencyCheckRule(entity, entity2)));

            Assert.True(rule.Validate().IsValid);
        }
        
        [Fact] 
        public void InvokesIfValidThenExecute()
        {
            var output = string.Empty;
            new GreaterThanZeroRule(1, "foo")
                   .IfValidThenExecute(rule => output = "pass")
                   .Validate();

            Assert.Equal("pass", output);
        }

        [Fact] 
        public void DoesNotInvokeIfValidThenExecute()
        {
            var output = string.Empty;
            new GreaterThanZeroRule(0, "foo")
                   .IfValidThenExecute(rule => output = "pass")
                   .Validate();

            Assert.Equal("", output);
        }

        [Fact] 
        public void InvokesIfInvalidThenExecute()
        {
            var output = string.Empty;
            new GreaterThanZeroRule(0, "foo")
                   .IfInvalidThenExecute(rule => output = "pass")
                   .Validate();

            Assert.Equal("pass", output);
        }

        [Fact] 
        public void DoesNotInvokeIfInvalidThenExecute()
        {
            var output = string.Empty;
            new GreaterThanZeroRule(1, "foo")
                   .IfInvalidThenExecute(rule => output = "pass")
                   .Validate();

            Assert.Equal("", output);
        }

        [Fact] 
        public void SuccessorInvokesExecuteIfValidThenExecute()
        {
            var output = string.Empty;
            var entity = BuildEntityMock(1, null);
            var rule = new GreaterThanZeroRule(1, "foo")
                               .IfValidThenValidate(new DomainObjectMustContainIDRule(entity)
                                                            .IfValidThenExecute(r => output = "pass"));
            rule.Validate();
            Assert.Equal("pass", output);
        }
        
        [Fact] 
        public void SuccessorDoesNotInvokeExecuteIfValidThenExecute()
        {
            var output = string.Empty;
            var entity = BuildEntityMock(0, null);
            var rule = new GreaterThanZeroRule(1, "foo")
                               .IfValidThenValidate(new DomainObjectMustContainIDRule(entity)
                                                            .IfValidThenExecute(r => output = "pass"));
            rule.Validate();
            Assert.Equal("", output);
        }

        [Fact] 
        public void SuccessorInvokesExecuteIfInvalidThenExecute()
        {
            var output = string.Empty;
            var entity = BuildEntityMock(0, null);
            var rule = new GreaterThanZeroRule(1, "foo")
                               .IfValidThenValidate(new DomainObjectMustContainIDRule(entity)
                                                            .IfInvalidThenExecute(r => output = "pass"));
            rule.Validate();
            Assert.Equal("pass", output);
        }
        
        [Fact] 
        public void SuccessorDoesNotInvokeExecuteIfInvalidThenExecute()
        {
            var output = string.Empty;
            var entity = BuildEntityMock(1, null);
            var rule = new GreaterThanZeroRule(1, "foo")
                               .IfValidThenValidate(new DomainObjectMustContainIDRule(entity)
                                                            .IfInvalidThenExecute(r => output = "pass"));
            rule.Validate();
            Assert.Equal("", output);
        }

        private static DomainBase BuildEntityMock(int domainID, byte[] version)
        {
            var mock = new Mock<DomainBase>();
            mock.Setup(e => e.ID).Returns(domainID);
            var entity = mock.Object;
            entity.Version = version;
            return entity;
        }
    }
}
