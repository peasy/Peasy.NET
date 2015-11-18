using Peasy;
using Peasy.Core;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class ProductClientService : ProductService, IProductService
    {
        public ProductClientService(IProductDataProxy dataProxy, IOrderDataProxy orderDataProxy, IInventoryItemService inventoryService, ITransactionContext transactionContext) : base(dataProxy, orderDataProxy, inventoryService, transactionContext)
        {
        }

        public override ICommand<Product> InsertCommand(Product entity)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new ServiceCommand<Product>
            (
                executeMethod: () => dataProxy.Insert(entity), 
                executeAsyncMethod: () => dataProxy.InsertAsync(entity)
            );
        }

        public override ICommand DeleteCommand(long id)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new ServiceCommand
            (
                executeMethod: () => dataProxy.Delete(id), 
                executeAsyncMethod: () => dataProxy.DeleteAsync(id)
            );
        }
    }
}
