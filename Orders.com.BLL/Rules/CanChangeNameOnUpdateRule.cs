using Facile.Core;
using Orders.com.Core.DataProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.com.Core.Extensions;

namespace Orders.com.BLL.Rules
{
    public class CanChangeNameOnUpdateRule : RuleBase
    {
        private long _orderID;
        private OrderItemService _orderItemDataProxy;

        public CanChangeNameOnUpdateRule(long orderID, OrderItemService orderItemDataProxy)
        {
            _orderID = orderID;
            _orderItemDataProxy = orderItemDataProxy;
        }

        protected override void OnValidate()
        {
            var items = _orderItemDataProxy.GetByOrderCommand(_orderID).Execute().Value;
            if (items.Any(i => i.OrderStatus() is ShippedState))
            {
                Invalidate("The customer for this order cannot change because it has items that have been shipped");
            }
        }

        protected async Task OnValidateAsync()
        {
            var items = await _orderItemDataProxy.GetByOrderCommand(_orderID).ExecuteAsync();
            if (items.Value.Any(i => i.OrderStatus() is ShippedState))
            {
                Invalidate("The customer for this order cannot change because it has items that have been shipped");
            }
        }
    }
}
