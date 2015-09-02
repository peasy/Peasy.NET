using Facile.Core;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;

namespace Orders.com.BLL.Rules
{
    public class ValidOrderItemStatusForDeleteRule : RuleBase
    {
        private OrderItem _item;

        public ValidOrderItemStatusForDeleteRule(OrderItem item)
        {
            _item = item;
        }

        protected override void OnValidate()
        {
            if (_item.OrderStatus().IsShipped)
            {
                Invalidate("Shipped items cannot be deleted");
            }
        }
    }
}
