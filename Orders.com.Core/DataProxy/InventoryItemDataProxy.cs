using Orders.com.Core.Domain;
using System.Threading.Tasks;

namespace Orders.com.Core.DataProxy
{
    public interface IInventoryItemDataProxy : IOrdersDotComDataProxy<InventoryItem>
    {
        InventoryItem GetByProduct(long productID);
        Task<InventoryItem> GetByProductAsync(long productID);
        /// <exception cref="Orders.com.Core.Exceptions.InsufficientStockAmountException">Exception thrown when requested quantity is greater than current quantity</exception>
        InventoryItem DecrementQuantityOnHand(long inventoryID, decimal quantity);
        /// <exception cref="Orders.com.Core.Exceptions.InsufficientStockAmountException">Exception thrown when requested quantity is greater than current quantity</exception>
        Task<InventoryItem> DecrementQuantityOnHandAsync(long inventoryID, decimal quantity);
    }
}
