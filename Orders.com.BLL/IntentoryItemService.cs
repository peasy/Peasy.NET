using Peasy.Core;
using Peasy.Core.Extensions;
using Peasy.Extensions;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

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
