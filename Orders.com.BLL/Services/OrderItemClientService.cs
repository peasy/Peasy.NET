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

namespace Orders.com.BLL
{
    public class OrderItemClientService : OrderItemService
    {
        private ITransactionContext _transactionContext;
        private IInventoryItemDataProxy _inventoryDataProxy;

        public OrderItemClientService(IOrderItemDataProxy dataProxy,
                                      IProductDataProxy productDataProxy,
                                      IInventoryItemDataProxy inventoryDataProxy,
                                      ITransactionContext transactionContext) : base(dataProxy, productDataProxy, inventoryDataProxy, transactionContext)
        {
            _inventoryDataProxy = inventoryDataProxy;
            _transactionContext = transactionContext;
        }

        public override ICommand<OrderItem> ShipCommand(long orderItemID)
        {
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ServiceCommand<OrderItem>
            (
                executeMethod: () => { return proxy.Ship(new OrderItem { OrderItemID = orderItemID }); },
                executeAsyncMethod: () => { return proxy.ShipAsync(new OrderItem { OrderItemID = orderItemID }); }
            );
        }
    }
}
