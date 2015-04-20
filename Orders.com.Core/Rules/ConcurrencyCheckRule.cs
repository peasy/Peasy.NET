using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facile.Extensions;
using Facile.Core;
using Orders.com.Core;

namespace Facile.Rules
{
    public class ConcurrencyCheckRule<TKey> : RuleBase
    {
        private DomainBase<TKey> _originalEntity;
        private DomainBase<TKey> _newEntity;

        public ConcurrencyCheckRule(DomainBase<TKey> originalEntity, DomainBase<TKey> newEntity)
        {
            _originalEntity = originalEntity;
            _newEntity = newEntity;
        }

        protected override void OnValidate()
        {
            if (!_originalEntity.Version.SequenceEqual(_newEntity.Version))
            {
                Invalidate(string.Format("{0} was changed by another user and cannot be changed.", _newEntity.ClassName()));
            }
        }
    }

}
