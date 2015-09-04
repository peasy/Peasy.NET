using System.Threading.Tasks;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Facile;
using Facile.Core;
using Orders.com.BLL.Commands;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class ProductService : OrdersDotComServiceBase<Product>
    {
        private InventoryItemService _inventoryService;
        private ITransactionContext _transactionContext;

        public ProductService(IProductDataProxy dataProxy, InventoryItemService inventoryService, ITransactionContext transactionContext) : base(dataProxy)
        {
            _inventoryService = inventoryService;
            _transactionContext = transactionContext;
        }

        public override ICommand<Product> InsertCommand(Product entity)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new CreateProductCommand(entity, dataProxy, _inventoryService, _transactionContext);
        }

        public ICommand<IEnumerable<Product>> GetByCategoryCommand(long categoryID)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new ServiceCommand<IEnumerable<Product>>
            (
                executeMethod: () => dataProxy.GetByCategory(categoryID),
                executeAsyncMethod: () => dataProxy.GetByCategoryAsync(categoryID)
            );
        }
    }
}
