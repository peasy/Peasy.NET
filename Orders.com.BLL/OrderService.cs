using Facile.Core;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.QueryData;
using System;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class OrderService : OrdersDotComServiceBase<Order>
    {
        private OrderItemService _orderItemService;

        public OrderService(IOrderDataProxy dataProxy, OrderItemService orderItemService) : base(dataProxy)
        {
            _orderItemService = orderItemService;
        }

        protected override void OnBeforeInsertCommandExecuted(Order entity)
        {
            entity.OrderDate = DateTime.Now;
        }

        protected override IEnumerable<IRule> GetBusinessRulesForUpdate(Order entity)
        {
            yield return new ValidOrderStatusForUpdateRule(entity.ID, _orderItemService);
        }

        public ICommand<IEnumerable<OrderInfo>> GetAllCommand(int start, int pageSize)
        {
            var proxy = DataProxy as IOrderDataProxy;
            return new ServiceCommand<IEnumerable<OrderInfo>>
            (
                executeMethod: () => proxy.GetAll(start, pageSize),
                executeAsyncMethod: () => proxy.GetAllAsync(start, pageSize)
            );
        }

        public override ICommand DeleteCommand(long id)
        {
            //TODO: create a DeleteOrderCommand that takes OrderService and OrderItemService, and return that command here
            return base.DeleteCommand(id);
        }
    }
}
