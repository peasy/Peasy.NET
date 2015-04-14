using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facile.Extensions;

namespace Facile.Rules
{
    public class ConcurrencyCheckRule : RuleBase
    {
        private DomainBase _originalEntity;
        private DomainBase _newEntity;

        public ConcurrencyCheckRule(DomainBase originalEntity, DomainBase newEntity)
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
