using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF
{
    public class OrderItemUpdatedEvent
    {
        public OrderItem OrderItem { get; set; }
    }
}
