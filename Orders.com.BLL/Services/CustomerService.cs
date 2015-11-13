using Peasy.Core;
using Orders.com.BLL.Rules;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class CustomerService : OrdersDotComServiceBase<Customer>, ICustomerService
    {
        private IOrderService _orderService;

        public CustomerService(ICustomerDataProxy dataProxy, IOrderService orderService) : base(dataProxy)
        {
            _orderService = orderService;
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id, ExecutionContext<Customer> context)
        {
            yield return base.GetBusinessRulesForDelete(id, context)
                             .IfAllValidThenValidate
                             (
                                new CanDeleteCustomerRule(id, _orderService)
                             );
        }

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(long id, ExecutionContext<Customer> context)
        {
            var baseRules = await base.GetBusinessRulesForDeleteAsync(id, context);
            return baseRules.IfAllValidThenValidate(new CanDeleteCustomerRule(id, _orderService))
                            .ToArray();
        }
    }
}
