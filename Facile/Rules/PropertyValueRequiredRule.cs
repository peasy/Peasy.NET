using Facile.Core;

namespace Facile.Rules
{
    public class PropertyValueRequiredRule : RuleBase
    {
        private string _propertyName;

        public PropertyValueRequiredRule(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override void OnValidate()
        {
            Invalidate(string.Format("{0} is required", _propertyName));
        }
    }
}
