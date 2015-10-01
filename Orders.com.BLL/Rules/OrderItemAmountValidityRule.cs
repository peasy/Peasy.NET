using Peasy.Core;
using Orders.com.Core.Domain;

namespace Peasy.Rules
{
    public class OrderItemAmountValidityRule : RuleBase
    {
        private OrderItem _item;
        private Product _product;

        public OrderItemAmountValidityRule(OrderItem item, Product product)
        {
            _item = item;
            _product = product;
        }

        protected override void OnValidate()
        {
            if (_item.Amount != _product.Price * _item.Quantity)
            {
                Invalidate(string.Format("The amount for {0} does not match the specified quantity multiplied by the current price in our system", _product.Name));
            }
        }
    }
}
