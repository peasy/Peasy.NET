using Facile.Core;
using System.Linq;

namespace Orders.com.BLL.Rules
{
    public class CanDeleteCustomerRule : RuleBase
    {
        private long _customerID;
        private OrderService _orderService;

        public CanDeleteCustomerRule(long customerID, OrderService orderService)
        {
            _customerID = customerID;
            _orderService = orderService;
        }

        protected override void OnValidate()
        {
            var orders = _orderService.GetByCustomerCommand(_customerID).Execute().Value;
            if (orders.Any())
            {
                Invalidate("This customer is associated with one or more orders and cannot be deleted.");
            }
        }
    }
}
