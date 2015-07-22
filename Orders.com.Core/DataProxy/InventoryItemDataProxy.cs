using Orders.com.Core.Domain;
using System.Threading.Tasks;

namespace Orders.com.Core.DataProxy
{
    public interface IInventoryItemDataProxy : IOrdersDotComDataProxy<InventoryItem>
    {
        InventoryItem GetByProduct(long productID);
        Task<InventoryItem> GetByProductAsync(long productID);
    }
}
