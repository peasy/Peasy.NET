using Peasy;
using Peasy.Core;
using Peasy.Core.Extensions;
using Peasy.Extensions;
using Orders.com.BLL;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Products.com.BLL.Commands
{
    public class DeleteProductCommand : Command
    {
        private long _productID;
        private IProductDataProxy _productDataProxy;
        private ITransactionContext _transactionContext;
        private IInventoryItemService _inventoryService;
        private IOrderService _orderService;

        public DeleteProductCommand(long productID,
                                    IProductDataProxy productDataProxy,
                                    IInventoryItemService inventoryService,
                                    IOrderService orderService,
                                    ITransactionContext transactionContext)
        {
            _productID = productID;
            _productDataProxy = productDataProxy;
            _inventoryService = inventoryService;
            _orderService = orderService;
            _transactionContext = transactionContext;
        }

        protected override void OnExecute()
        {
            _transactionContext.Execute(() =>
            {
                var item = _inventoryService.GetByProductCommand(_productID).Execute().Value;
                _inventoryService.DeleteCommand(item.ID).Execute();
                _productDataProxy.Delete(_productID);
            });
        }

        protected override async Task OnExecuteAsync()
        {
            await _transactionContext.ExecuteAsync(async () =>
            {
                var result = await _inventoryService.GetByProductCommand(_productID).ExecuteAsync();
                await _inventoryService.DeleteCommand(result.Value.ID).ExecuteAsync();
                await _productDataProxy.DeleteAsync(_productID);
            });
        }

        public IEnumerable<IRule> GetRules()
        {
            yield return new CanDeleteProductRule(_productID, _orderService);
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            var rule = _productID.CreateValueRequiredRule("id").Validate();
            if (!rule.IsValid)
                yield return new ValidationResult(rule.ErrorMessage);

            foreach (var error in GetRules().GetValidationResults())
                yield return error;
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return GetErrors();
        }
    }
}
