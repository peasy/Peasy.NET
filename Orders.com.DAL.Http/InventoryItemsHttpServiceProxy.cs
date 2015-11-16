using Orders.com.DataProxy;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.Http
{
    public class InventoryItemsHttpServiceProxy : HttpServiceProxyBase<InventoryItem, long>, IInventoryItemDataProxy
    {
        protected override string RequestUri
        {
            get { return "http://localhost:53534/api/inventoryitems"; }
        }

        public InventoryItem GetByProduct(long productID)
        {
            return base.GET<InventoryItem>($"{RequestUri}?productid={productID}");
        }

        public Task<InventoryItem> GetByProductAsync(long productID)
        {
            return base.GETAsync<InventoryItem>($"{RequestUri}?productid={productID}");
        }
    }
}
