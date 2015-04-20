using Facile.Core;
using Orders.com.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Rules
{
    public class DomainObjectMustContainIDRule<TKey> : RuleBase
    {
        private DomainBase<TKey> _domainBase;

        public DomainObjectMustContainIDRule(DomainBase<TKey> domainBase)
        {
            _domainBase = domainBase;
        }

        protected override void OnValidate()
        {
            //if (_domainBase.ID <= 0)
            //{
            //    Invalidate("Domain object must contain an id value greater than 0");
            //}
        }
    }
}
