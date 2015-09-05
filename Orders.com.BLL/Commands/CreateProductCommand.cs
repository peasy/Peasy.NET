using Facile;
using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Threading.Tasks;

namespace Orders.com.BLL.Commands
{
    public class CreateProductCommand : Command<Product>
    {
        private InventoryItemService _inventoryService;
        private IProductDataProxy _productDataProxy;
        private ITransactionContext _transactionContext;

        public CreateProductCommand(Product product, IProductDataProxy productDataProxy, InventoryItemService inventoryService, ITransactionContext transactionContext)
        {
            CurrentProduct = product;
            _productDataProxy = productDataProxy;
            _inventoryService = inventoryService;
            _transactionContext = transactionContext;
        }

        private Product CurrentProduct { get; set; }

        protected override Product OnExecute()
        {
            return _transactionContext.Execute(() =>
            {
                var product = _productDataProxy.Insert(CurrentProduct);
                _inventoryService.InsertCommand(BuildInventoryItem(product)).Execute();
                return product;
            });
        }

        protected override async Task<Product> OnExecuteAsync()
        {
            return await _transactionContext.ExecuteAsync(async () =>
            {
                var product = await _productDataProxy.InsertAsync(CurrentProduct);
                await _inventoryService.InsertCommand(BuildInventoryItem(product)).ExecuteAsync();
                return product;
            });
        }
        
        private InventoryItem BuildInventoryItem(Product product)
        {
            return new InventoryItem { ProductID = product.ProductID, QuantityOnHand = 0 };
        }
    }
}
