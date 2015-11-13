using Orders.com.Domain;
using Peasy.Core;

namespace Orders.com.BLL
{
    public interface IInventoryItemService : IService<InventoryItem, long>
    {
        ICommand<InventoryItem> GetByProductCommand(long productID);
    }
}