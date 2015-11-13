using Peasy;
using Peasy.Core;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.BLL.Commands
{
    public class CreateProductCommand : Command<Product>
    {
        private IInventoryItemService _inventoryService;
        private IProductDataProxy _productDataProxy;
        private ITransactionContext _transactionContext;

        public CreateProductCommand(Product product, 
                                    IProductDataProxy productDataProxy, 
                                    IInventoryItemService inventoryService, 
                                    ITransactionContext transactionContext)
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

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return CurrentProduct.GetValidationErrors();
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return GetErrors();
        }
    }
}
