using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System.Collections.Generic;
using System;

namespace Orders.com.BLL
{
    public class OrderItemService : OrdersDotComServiceBase<OrderItem>
    {
        private InventoryItemService _inventoryService;

        public OrderItemService(IOrderItemDataProxy dataProxy, InventoryItemService inventoryService) : base(dataProxy)
        {
            _inventoryService = inventoryService;
        }

        protected override void OnBeforeInsertCommandExecuted(OrderItem entity)
        {
            entity.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
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
