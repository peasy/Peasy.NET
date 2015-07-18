using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class OrderStatus : DomainBase
    {
        public long OrderStatusID { get; set; }

        public string Name { get; set; }

        public override long ID
        {
            get { return OrderStatusID; }
            set { OrderStatusID = value; }
        }
    }
}
