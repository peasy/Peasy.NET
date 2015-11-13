using Orders.com.DataProxy;
using Peasy.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Rules
{
    public class CanDeleteProductRule : RuleBase
    {
        private long _productID;
        private string _errorMessage = "This product is associated with one or more orders and cannot be deleted.";
        private IOrderDataProxy _ordersDataProxy;

        public CanDeleteProductRule(long productID, IOrderDataProxy ordersDataProxy)
        {
            _productID = productID;
            _ordersDataProxy = ordersDataProxy;
        }

        protected override void OnValidate()
        {
            var orders = _ordersDataProxy.GetByProduct(_productID);
            if (orders.Any())
            {
                Invalidate(_errorMessage);
            }
        }

        protected override async Task OnValidateAsync()
        {
            var orders = await _ordersDataProxy.GetByProductAsync(_productID);
            if (orders.Any())
            {
                Invalidate(_errorMessage);
            }
        }
    }
}
