using Facile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Rules
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
