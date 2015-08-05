using System;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Facile.Core;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class InventoryItemService : OrdersDotComServiceBase<InventoryItem> 
    {
        public InventoryItemService(IInventoryItemDataProxy dataProxy) : base(dataProxy)
        {
        }

        public ICommand<InventoryItem> GetByProductCommand(long productID)
        {
            var proxy = DataProxy as IInventoryItemDataProxy;
            return new ServiceCommand<InventoryItem>
            (
                executeMethod: () => proxy.GetByProduct(productID),
                executeAsyncMethod: () => proxy.GetByProductAsync(productID)
            );
        }
    }
}
