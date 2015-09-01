using Facile.Core;
using Facile.Core.Extensions;
using Facile.Extensions;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

namespace Orders.com.BLL
{
    public class InventoryItemService : OrdersDotComServiceBase<InventoryItem> 
    {
        public InventoryItemService(IInventoryItemDataProxy dataProxy) : base(dataProxy)
        {
        }

        public ICommand<InventoryItem> DecrementQuantityOnHandCommand(long productID, decimal quantity)
        {
            var proxy = DataProxy as IInventoryItemDataProxy;
            return new ServiceCommand<InventoryItem>
            (
                executeMethod: () => proxy.DecrementQuantityOnHand(productID, quantity),
                executeAsyncMethod: () => proxy.DecrementQuantityOnHandAsync(productID, quantity),
                getValidationRulesMethod: () => new[] { quantity.CreateValueRequiredRule("quantity") }.GetBusinessRulesResults(this.GetType().Name)
            );
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
