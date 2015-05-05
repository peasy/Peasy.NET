using Facile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class Order : DomainBase<int>
    {
        public int OrderID { get; set; }

        [FacileForeignKey]
        public int CustomerID { get; set; }

        public override int ID
        {
            get { return OrderID; }
            set { OrderID = value; }
        }
    }
}
