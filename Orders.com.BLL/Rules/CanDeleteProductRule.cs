using Facile.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Rules
{
    public class CanDeleteProductRule : RuleBase
    {
        private OrderService _orderService;
        private long _productID;
        private string _errorMessage = "This product is associated with one or more orders and cannot be deleted.";

        public CanDeleteProductRule(long productID, OrderService orderService)
        {
            _productID = productID;
            _orderService = orderService;
        }
        
        protected override void OnValidate()
        {
            var products = _orderService.GetByProductCommand(_productID).Execute().Value;
            if (products.Any())
            {
                Invalidate(_errorMessage);
            }
        }

        protected override async Task OnValidateAsync()
        {
            var products = await _orderService.GetByProductCommand(_productID).ExecuteAsync();
            if (products.Value.Any())
            {
                Invalidate(_errorMessage);
            }
        }
    }
}
