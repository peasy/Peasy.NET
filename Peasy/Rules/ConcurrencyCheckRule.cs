using Peasy.Extensions;
using Peasy.Core;

namespace Peasy.Rules
{
    public class ConcurrencyCheckRule : RuleBase
    {
        private IVersionContainer _originalEntity;
        private IVersionContainer _newEntity;

        public ConcurrencyCheckRule(IVersionContainer originalEntity, IVersionContainer newEntity)
        {
            _originalEntity = originalEntity;
            _newEntity = newEntity;
        }

        protected override void OnValidate()
        {
            if (_originalEntity.Version != _newEntity.Version)
            {
                Invalidate($"{_newEntity.ClassName()} was changed by another user and cannot be changed.");
            }
        }
    }
}
