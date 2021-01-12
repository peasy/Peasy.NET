namespace Peasy.Rules
{
    /// <summary>
    /// </summary>
    public class PropertyValueRequiredRule : RuleBase
    {
        private string _propertyName;

        /// <summary>
        /// </summary>
        public PropertyValueRequiredRule(string propertyName)
        {
            _propertyName = propertyName;
        }

        /// <summary>
        /// </summary>
        protected override void OnValidate()
        {
            Invalidate($"{_propertyName} is required", _propertyName);
        }
    }
}
