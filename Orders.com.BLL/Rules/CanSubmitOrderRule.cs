using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL.Rules
{
    public class CanSubmitOrderRule : RuleBase
    {
        private Order _order;

        public CanSubmitOrderRule(Order order)
        {
            _order = order;
        }

        protected override void OnValidate()
        {
            if (!_order.OrderStatus().IsPending)
            {
                Invalidate(string.Format("Order ID:{0} is in a {1} state and cannot be submitted", _order.ID.ToString(), _order.OrderStatus().Name));
            }
        }
    }
}
