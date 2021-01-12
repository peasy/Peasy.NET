namespace Peasy.Rules
{
    /// <summary>
    /// </summary>
    public class GreaterThanZeroRule : RuleBase
    {
        private decimal _value;
        private string _errorMessage;

        /// <summary>
        /// </summary>
        public GreaterThanZeroRule(decimal value, string errorMessage)
        {
            _value = value;
            _errorMessage = errorMessage;
        }

        /// <summary>
        /// </summary>
        protected override void OnValidate()
        {
            if (_value <= 0)
            {
                Invalidate(_errorMessage);
            }
        }
    }
}
