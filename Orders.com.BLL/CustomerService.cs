using Facile.Core;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class CustomerService : OrdersDotComServiceBase<Customer>
    {
        private OrderService _orderService;

        public CustomerService(ICustomerDataProxy dataProxy, OrderService orderService) : base(dataProxy)
        {
            _orderService = orderService;
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id)
        {
            yield return base.GetBusinessRulesForDelete(id)
                             .IfAllValidThenValidate
                             (
                                new CanDeleteCustomerRule(id, _orderService)
                             );
        }
    }
}
