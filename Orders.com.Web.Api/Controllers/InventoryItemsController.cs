using Orders.com.BLL;
using Orders.com.Domain;

namespace Orders.com.Web.Api.Controllers
{
    public class InventoryItemsController : ApiControllerBase<InventoryItem, long>
    {
        public InventoryItemsController(IInventoryItemService inventoryService)
        {
            _businessService = inventoryService;
        }
    }
}