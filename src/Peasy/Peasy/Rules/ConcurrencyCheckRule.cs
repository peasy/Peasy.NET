using Peasy.Extensions;

namespace Peasy.Rules
{
    /// <summary>
    /// </summary>
    public class ConcurrencyCheckRule : RuleBase
    {
        private IVersionContainer _originalEntity;
        private IVersionContainer _newEntity;

        /// <summary>
        /// </summary>
        public ConcurrencyCheckRule(IVersionContainer originalEntity, IVersionContainer newEntity)
        {
            _originalEntity = originalEntity;
            _newEntity = newEntity;
        }

        /// <summary>
        /// </summary>
        protected override void OnValidate()
        {
            if (_originalEntity.Version != _newEntity.Version)
            {
                Invalidate($"{_newEntity.ClassName()} was changed by another user and cannot be changed.");
            }
        }
    }
}
