using Facile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
