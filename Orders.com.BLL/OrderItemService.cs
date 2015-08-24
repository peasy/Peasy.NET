using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System.Collections.Generic;
using System;
using Orders.com.BLL.Rules;
using System.Threading.Tasks;
using Facile.Rules;
using System.Linq;

namespace Orders.com.BLL
{
    public class OrderItemService : OrdersDotComServiceBase<OrderItem>
    {
        private IProductDataProxy _productDataProxy;

        public OrderItemService(IOrderItemDataProxy dataProxy, IProductDataProxy productDataProxy) : base(dataProxy)
        {
            _productDataProxy = productDataProxy;
        }

        protected override void OnBeforeInsertCommandExecuted(OrderItem entity)
        {
            entity.OrderStatus().SetPendingState();
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(OrderItem entity)
        {
            var currentProduct = _productDataProxy.GetByID(entity.ProductID);
            yield return new OrderItemPriceValidityRule(entity, currentProduct);
            yield return new OrderItemAmountValidityRule(entity, currentProduct);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForUpdate(OrderItem entity)
        {
            var currentProduct = _productDataProxy.GetByID(entity.ProductID);
            yield return new OrderItemPriceValidityRule(entity, currentProduct);
            yield return new OrderItemAmountValidityRule(entity, currentProduct);
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
            //TODO: decrement inventory service
            //TODO: create a ShipOrderCommand that requires OrderService and Inventory Service, and return that command here
            // perform auth check?
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ServiceCommand<OrderItem>
            (
                executeMethod: () =>
                {
                    var orderItem = _dataProxy.GetByID(orderItemID);
                    return proxy.Ship(orderItemID, DateTime.Now);
                },
                executeAsyncMethod: () => proxy.ShipAsync(orderItemID, DateTime.Now) 
            );
        }
    }
}
