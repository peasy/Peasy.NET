using Facile;
using Facile.Core;
using Facile.Rules;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class OrderItemService : OrdersDotComServiceBase<OrderItem>
    {
        private IProductDataProxy _productDataProxy;
        private InventoryItemService _inventoryService;
        private ITransactionContext _transactionContext;

        public OrderItemService(IOrderItemDataProxy dataProxy, 
                                IProductDataProxy productDataProxy, 
                                InventoryItemService inventoryService, 
                                ITransactionContext transactionContext) : base(dataProxy)
        {
            _productDataProxy = productDataProxy;
            _inventoryService = inventoryService;
            _transactionContext = transactionContext;
        }

        protected override void OnBeforeInsertCommandExecuted(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            entity.OrderStatus().SetPendingState();
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            var currentProduct = _productDataProxy.GetByID(entity.ProductID);
            yield return new OrderItemPriceValidityRule(entity, currentProduct);
            yield return new OrderItemAmountValidityRule(entity, currentProduct);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForUpdate(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            var currentProduct = _productDataProxy.GetByID(entity.ProductID);
            yield return new ValidOrderItemStatusForUpdateRule(entity)
                                .IfValidThenValidate
                                (
                                    new OrderItemPriceValidityRule(entity, currentProduct),
                                    new OrderItemAmountValidityRule(entity, currentProduct)
                                );
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id, ExecutionContext<OrderItem> context)
        {
            var currentOrderItem = _dataProxy.GetByID(id);
            yield return new ValidOrderItemStatusForDeleteRule(currentOrderItem);
        }

        public ICommand<IEnumerable<OrderItem>> GetByOrderCommand(long orderID)
        {
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ServiceCommand<IEnumerable<OrderItem>>
            (
                executeMethod: () => proxy.GetByOrder(orderID),
                executeAsyncMethod: () => proxy.GetByOrderAsync(orderID)
            );
        }

        public ICommand<OrderItem> SubmitCommand(long orderItemID)
        {
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ServiceCommand<OrderItem>
            (
                executeMethod: () => proxy.Submit(orderItemID, DateTime.Now),
                executeAsyncMethod: () => proxy.SubmitAsync(orderItemID, DateTime.Now),
                getBusinessRulesMethod: () => GetBusinessRulesForSubmit(orderItemID)
            );
        }

        private IEnumerable<IRule> GetBusinessRulesForSubmit(long orderItemID)
        {
            if (!IsLatencyProne)
            {
                var orderItem = DataProxy.GetByID(orderItemID);
                yield return new CanSubmitOrderItemRule(orderItem);        
            }
        }

        public ICommand<OrderItem> ShipCommand(long orderItemID)
        {
            // perform auth check?
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ShipOrderItemCommand(orderItemID, proxy, _inventoryService, _transactionContext);
        }
    }
}
