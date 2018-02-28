using System;

namespace Peasy.Rules
{
    public class ValueRequiredRule : RuleBase
    {
        private string _errorMessage;
        private string _fieldName;
        private Func<bool> _validate;

        public ValueRequiredRule(long value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        public ValueRequiredRule(decimal value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        public ValueRequiredRule(int value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        public ValueRequiredRule(string value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be supplied";
            _fieldName = fieldName;
            _validate = () => !string.IsNullOrWhiteSpace(value);
        }

        public ValueRequiredRule(Guid value, string fieldName)
        {
            _errorMessage = $"A valid UUID for {fieldName} must be supplied";
            _fieldName = fieldName;
            _validate = () => value == null || value != Guid.Empty;
        }

        protected override void OnValidate()
        {
            if (!_validate())
                Invalidate(_errorMessage, _fieldName);
        }
    }
}
