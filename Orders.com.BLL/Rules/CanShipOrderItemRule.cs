using Facile.Core;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;

namespace Orders.com.BLL.Rules
{
    public class CanShipOrderItemRule : RuleBase
    {
        private OrderItem _orderItem;

        public CanShipOrderItemRule(OrderItem orderItem)
        {
            _orderItem = orderItem;
        }

        protected override void OnValidate()
        {
            if (!_orderItem.OrderStatus().CanShip)
            {
                Invalidate(string.Format("Order Item ID:{0} is in a {1} state and cannot be shipped", _orderItem .ID.ToString(), _orderItem .OrderStatus().Name));
            }
        }
    }
}
