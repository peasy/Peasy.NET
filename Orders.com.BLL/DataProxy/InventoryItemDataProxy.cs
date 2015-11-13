using Orders.com.Domain;
using System.Threading.Tasks;

namespace Orders.com.DataProxy
{
    public interface IInventoryItemDataProxy : IOrdersDotComDataProxy<InventoryItem>
    {
        InventoryItem GetByProduct(long productID);
        Task<InventoryItem> GetByProductAsync(long productID);
    }
}
