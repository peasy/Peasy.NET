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
            item.Price = price;
            return new Tuple<OrderItem>(item);
        }

        public static OrderItem SetAmount(this OrderItem item)
        {
            item.Amount = item.Quantity * item.Price;
            return item;
        }
    }
}
