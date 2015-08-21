using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System.Collections.Generic;
using System;
using Orders.com.BLL.Rules;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class OrderItemService : OrdersDotComServiceBase<OrderItem>
    {
        private InventoryItemService _inventoryService;
        private IProductDataProxy _productDataProxy;

        public OrderItemService(IOrderItemDataProxy dataProxy, IProductDataProxy productDataProxy, InventoryItemService inventoryService) : base(dataProxy)
        {
            _productDataProxy = productDataProxy;
            _inventoryService = inventoryService;
        }

        protected override void OnBeforeInsertCommandExecuted(OrderItem entity)
        {
            entity.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
        }

        protected override OrderItem Insert(OrderItem entity)
        {
            var product = _productDataProxy.GetByID(entity.ProductID);
            entity.SetPrice(product.Price.Value).ThenSetAmount();
            return base.Insert(entity);
        }

        protected override async Task<OrderItem> InsertAsync(OrderItem entity)
        {
            var product = await _productDataProxy.GetByIDAsync(entity.ProductID);
            entity.SetPrice(product.Price.Value).ThenSetAmount();
            return await base.InsertAsync(entity);
        }

        protected override OrderItem Update(OrderItem entity)
        {
            var product = _productDataProxy.GetByID(entity.ProductID);
            entity.SetPrice(product.Price.Value).ThenSetAmount();
            return base.Update(entity);
        }

        protected override async Task<OrderItem> UpdateAsync(OrderItem entity)
        {
            var product = await _productDataProxy.GetByIDAsync(entity.ProductID);
            entity.SetPrice(product.Price.Value).ThenSetAmount();
            return await base.UpdateAsync(entity);
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
                    var inventoryItem = _inventoryService.GetByProductCommand(orderItem.ProductID);
                    return proxy.Ship(orderItemID, DateTime.Now);
                },
                executeAsyncMethod: () => proxy.ShipAsync(orderItemID, DateTime.Now) 
            );
        }
    }
}
