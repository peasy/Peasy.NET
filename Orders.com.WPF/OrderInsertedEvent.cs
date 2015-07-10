using Orders.com.Core.Domain;
using Orders.com.WPF.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF
{
    public class OrderInsertedEvent
    {
        public CustomerOrderVM Order { get; set; }
    }
}
