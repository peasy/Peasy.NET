using System;
using System.Threading.Tasks;

namespace Peasy.Rules
{
    /// <summary>
    /// A general purpose validation rule that verifies whether a supplied type contains a non-zero or non-empty value.
    /// </summary>
    public class ValueRequiredRule : RuleBase
    {
        private string _errorMessage;
        private string _fieldName;
        private Func<bool> _validate;

        /// <summary>
        /// </summary>
        public ValueRequiredRule(double value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        /// <summary>
        /// </summary>
        public ValueRequiredRule(long value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        /// <summary>
        /// </summary>
        public ValueRequiredRule(decimal value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        /// <summary>
        /// </summary>
        public ValueRequiredRule(int value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be greater than 0";
            _fieldName = fieldName;
            _validate = () => value > 0;
        }

        /// <summary>
        /// </summary>
        public ValueRequiredRule(string value, string fieldName)
        {
            _errorMessage = $"{fieldName} must be supplied";
            _fieldName = fieldName;
            _validate = () => !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// </summary>
        public ValueRequiredRule(Guid value, string fieldName)
        {
            _errorMessage = $"A valid UUID for {fieldName} must be supplied";
            _fieldName = fieldName;
            _validate = () => value != Guid.Empty;
        }

        /// <summary>
        /// </summary>
        protected async override Task OnValidateAsync()
        {
            await Task.FromResult<object>(null);
            if (!_validate()) Invalidate(_errorMessage, _fieldName);
        }
    }
}
