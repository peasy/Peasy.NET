using Facile.Core;
using Orders.com.Core;

namespace Orders.com.BLL.Rules
{
    public class DomainObjectMustContainIDRule : RuleBase
    {
        private DomainBase _domainBase;

        public DomainObjectMustContainIDRule(DomainBase domainBase)
        {
            _domainBase = domainBase;
        }

        protected override void OnValidate()
        {
            if (_domainBase.ID <= 0)
            {
                Invalidate("Domain object must contain an id value greater than 0");
            }
        }
    }
}
