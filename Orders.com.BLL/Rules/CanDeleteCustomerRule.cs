using Facile.Core;
using Orders.com.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Orders.com.BLL.Rules
{
    public class CanDeleteCustomerRule : RuleBase
    {
        private long _customerID;
        private IEnumerable<Order> _orders;
        private OrderService _orderService;

        //public CanDeleteCustomerRule(long customerID, IEnumerable<Order> orders)
        //{
        //    _customerID = customerID;
        //    _orders = orders;
        //}
        public CanDeleteCustomerRule(long customerID, OrderService orderService)
        {
            _customerID = customerID;
            _orderService = orderService;
        }

        protected override void OnValidate()
        {
            var orders = _orderService.GetByCustomerCommand(_customerID).Execute().Value;
            if (orders.Any(o => o.CustomerID == _customerID))
            {
                Invalidate("This customer is associated with one or more orders and cannot be deleted.");
            }
        }
        //protected override void OnValidate()
        //{
        //    if (_orders.Any(o => o.CustomerID == _customerID))
        //    {
        //        Invalidate("This customer is associated with one or more orders and cannot be deleted.");
        //    }
        //}
    }
}
