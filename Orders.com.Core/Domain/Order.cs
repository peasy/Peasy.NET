using Facile;
using Facile.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class Order : DomainBase
    {
        public int OrderID { get; set; }

        public int OrderStatusID { get; set; }

        [FacileForeignKey, FacileRequired]
        public int CustomerID { get; set; }

        [Editable(false), FacileRequired]
        public DateTime OrderDate { get; set; }

        public decimal Total { get; set; }

        public override int ID
        {
            get { return OrderID; }
            set { OrderID = value; }
        }
    }
}
