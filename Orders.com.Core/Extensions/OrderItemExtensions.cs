using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Extensions
{
    public static class OrderItemExtensions
    {
        public static Tuple<OrderItem> SetPrice(this OrderItem item, decimal price)
        {
            item.LockedInPrice = price;
            return new Tuple<OrderItem>(item);
        }

        public static OrderItem ThenSetAmount(this Tuple<OrderItem> item)
        {
            item.Item1.Amount = item.Item1.Quantity * item.Item1.LockedInPrice;
            return item.Item1;
        }
    }
}
