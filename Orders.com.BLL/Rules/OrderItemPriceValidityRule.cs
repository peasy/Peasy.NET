using Facile.Core;
using Orders.com.Core.Domain;

namespace Facile.Rules
{
    public class OrderItemPriceValidityRule : RuleBase
    {
        private OrderItem _item;
        private Product _product;

        public OrderItemPriceValidityRule(OrderItem item, Product product)
        {
            _item = item;
            _product = product;
        }

        protected override void OnValidate()
        {
            if (_item.Price != _product.Price)
            {
                Invalidate(string.Format("The price for {0} no longer reflects the current price in our system", _product.Name));
            }
        }
    }
}
