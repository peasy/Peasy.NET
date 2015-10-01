using Peasy.Core;

namespace Peasy.Rules
{
    public class GreaterThanZeroRule : RuleBase
    {
        private decimal _value;
        private string _errorMessage;

        public GreaterThanZeroRule(decimal value, string errorMessage)
        {
            _value = value;
            _errorMessage = errorMessage;
        }

        protected override void OnValidate()
        {
            if (_value <= 0)
            {
                Invalidate(_errorMessage); 
            }
        }
    }
}
