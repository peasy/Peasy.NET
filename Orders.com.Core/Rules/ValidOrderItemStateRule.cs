using Facile.Core;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;

namespace Facile.Rules
{
    public class ValidOrderItemStateRule : RuleBase
    {
        private OrderItem _item;

        public ValidOrderItemStateRule(OrderItem item)
        {
            _item = item;
        }

        protected override void OnValidate()
        {
            if (_item.OrderStatus().IsBackordered)
            {
                Invalidate("Backordered items cannot be changed");
            }
            else if (_item.OrderStatus().IsShipped)
            {
                Invalidate("Shipped items cannot be changed");
            }
        }
    }
}
