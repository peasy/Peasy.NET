using Facile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Rules
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
