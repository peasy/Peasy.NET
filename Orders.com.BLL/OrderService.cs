using Facile.Core;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.Core.QueryData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class OrderService : OrdersDotComServiceBase<Order>
    {
        private InventoryItemService _inventoryService;
        private OrderItemService _orderItemService;

        public OrderService(IOrderDataProxy dataProxy, OrderItemService orderItemService, InventoryItemService inventoryService) : base(dataProxy)
        {
            _inventoryService = inventoryService;
            _orderItemService = orderItemService;
        }

        protected override void OnBeforeInsertCommandExecuted(Order entity)
        {
            entity.OrderDate = DateTime.Now;
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
