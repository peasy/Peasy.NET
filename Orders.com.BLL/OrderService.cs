using Facile;
using Facile.Core;
using Orders.com.BLL.Commands;
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
        private ITransactionContext _transactionContext;

        public OrderService(IOrderDataProxy dataProxy, OrderItemService orderItemService, ITransactionContext transactionContext) : base(dataProxy)
        {
            _orderItemService = orderItemService;
            _transactionContext = transactionContext;
        }

        private IOrderDataProxy OrdersDataProxy
        {
            get { return DataProxy as IOrderDataProxy; }
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
            return new ServiceCommand<IEnumerable<OrderInfo>>
            (
                executeMethod: () => OrdersDataProxy.GetAll(start, pageSize),
                executeAsyncMethod: () => OrdersDataProxy.GetAllAsync(start, pageSize)
            );
        }

        public override ICommand DeleteCommand(long id)
        {
            return new DeleteOrderCommand(id, OrdersDataProxy, _orderItemService, _transactionContext);
        }
    }
}
