using Orders.com.Core.Domain;
using Orders.com.WPF.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF
{
    public class CustomerOrderUpdatedEvent
    {
        public CustomerOrderUpdatedEvent(CustomerOrderVM order)
        {
            Order = order;
        }

        public CustomerOrderVM Order { get; private set; }
    }
}
