namespace Peasy.Rules
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
            Invalidate($"{_propertyName} is required", _propertyName);
        }
    }
}
