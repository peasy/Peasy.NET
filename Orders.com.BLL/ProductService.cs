using Peasy;
using Peasy.Core;
using Orders.com.BLL.Commands;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Products.com.BLL.Commands;
using System.Collections.Generic;

namespace Orders.com.BLL
{
    public class ProductService : OrdersDotComServiceBase<Product>
    {
        private InventoryItemService _inventoryService;
        private ITransactionContext _transactionContext;
        private OrderService _orderService;

        public ProductService(IProductDataProxy dataProxy, OrderService orderService, InventoryItemService inventoryService, ITransactionContext transactionContext) : base(dataProxy)
        {
            _orderService = orderService;
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

        public override ICommand DeleteCommand(long id)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new DeleteProductCommand(id, dataProxy, _inventoryService, _orderService, _transactionContext);
        }
    }
}
