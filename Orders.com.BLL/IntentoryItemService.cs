using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

namespace Orders.com.BLL
{
    public class InventoryItemService : OrdersDotComServiceBase<InventoryItem> 
    {
        public InventoryItemService(IInventoryItemDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
