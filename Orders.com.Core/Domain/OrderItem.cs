using Facile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class OrderItem : DomainBase
    {
        public int OrderItemID { get; set; }

        [FacileForeignKey]
        public int ProductID { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public override int ID
        {
            get { return OrderItemID; }
            set { OrderItemID = value; }
        }
    }
}
