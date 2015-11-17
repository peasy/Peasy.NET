using Peasy;
using Peasy.Core;
using Peasy.Rules;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.DataProxy;
using Orders.com.Domain;
using Orders.com.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Exception;

namespace Orders.com.BLL
{
    public class OrderItemService : OrdersDotComServiceBase<OrderItem>, IOrderItemService
    {
        private IProductDataProxy _productDataProxy;
        private ITransactionContext _transactionContext;
        private IInventoryItemDataProxy _inventoryDataProxy;

        public OrderItemService(IOrderItemDataProxy dataProxy,
                                IProductDataProxy productDataProxy,
                                IInventoryItemDataProxy inventoryDataProxy,
                                ITransactionContext transactionContext) : base(dataProxy)
        {
            _productDataProxy = productDataProxy;
            _inventoryDataProxy = inventoryDataProxy;
            _transactionContext = transactionContext;
        }

        protected override void OnInsertCommandInitialization(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            entity.OrderStatus().SetPendingState();
        }

        protected override async Task OnInsertCommandInitializationAsync(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            OnInsertCommandInitialization(entity, context);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            var currentProduct = _productDataProxy.GetByID(entity.ProductID);
            yield return new OrderItemPriceValidityRule(entity, currentProduct);
            yield return new OrderItemAmountValidityRule(entity, currentProduct);
        }

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForInsertAsync(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            var currentProduct = await _productDataProxy.GetByIDAsync(entity.ProductID);
            return new IRule[]
            {
                new OrderItemPriceValidityRule(entity, currentProduct),
                new OrderItemAmountValidityRule(entity, currentProduct)
            };
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

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForUpdateAsync(OrderItem entity, ExecutionContext<OrderItem> context)
        {
            var currentProduct = await _productDataProxy.GetByIDAsync(entity.ProductID);
            return new ValidOrderItemStatusForUpdateRule(entity)
                                .IfValidThenValidate
                                (
                                    new OrderItemPriceValidityRule(entity, currentProduct),
                                    new OrderItemAmountValidityRule(entity, currentProduct)
                                )
                                .ToArray();
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id, ExecutionContext<OrderItem> context)
        {
            var currentOrderItem = _dataProxy.GetByID(id);
            yield return new ValidOrderItemStatusForDeleteRule(currentOrderItem);
        }

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(long id, ExecutionContext<OrderItem> context)
        {
            var currentOrderItem = await _dataProxy.GetByIDAsync(id);
            return new ValidOrderItemStatusForDeleteRule(currentOrderItem).ToArray();
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
            var context = new ExecutionContext<OrderItem>();
            return new ServiceCommand<OrderItem>
            (
                executeMethod: () =>
                {
                    context.CurrentEntity.OrderStatus().SetSubmittedState();
                    context.CurrentEntity.SubmittedDate = DateTime.Now;
                    return proxy.Submit(context.CurrentEntity);
                },
                executeAsyncMethod: () =>
                {
                    context.CurrentEntity.OrderStatus().SetSubmittedState();
                    context.CurrentEntity.SubmittedDate = DateTime.Now;
                    return proxy.SubmitAsync(context.CurrentEntity);
                },
                getBusinessRulesMethod: () => GetBusinessRulesForSubmit(orderItemID, context),
                getBusinessRulesAsyncMethod: () => GetBusinessRulesForSubmitAsync(orderItemID, context)
            );
        }

        private IEnumerable<IRule> GetBusinessRulesForSubmit(long orderItemID, ExecutionContext<OrderItem> context)
        {
            var orderItem = DataProxy.GetByID(orderItemID);
            context.CurrentEntity = orderItem;
            yield return new CanSubmitOrderItemRule(orderItem);
        }

        private async Task<IEnumerable<IRule>> GetBusinessRulesForSubmitAsync(long orderItemID, ExecutionContext<OrderItem> context)
        {
            var orderItem = await DataProxy.GetByIDAsync(orderItemID);
            context.CurrentEntity = orderItem;
            return new[] { new CanSubmitOrderItemRule(orderItem) };
        }

        public virtual ICommand<OrderItem> ShipCommand(long orderItemID)
        {
            // perform auth check?
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ShipOrderItemCommand(orderItemID, proxy, _inventoryDataProxy, _transactionContext);
        }
    }
}
