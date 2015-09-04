using Facile.Core;
using System.Linq;

namespace Orders.com.BLL.Rules
{
    public class CanDeleteProductRule : RuleBase
    {
        private OrderService _orderService;
        private long _productID;

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
                Invalidate("This product is associated with one or more orders and cannot be deleted.");
            }
        }
    }
}
