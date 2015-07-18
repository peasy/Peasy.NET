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
        public long OrderID { get; set; }

        [Editable(false)]
        public long OrderStatusID { get; set; }

        [FacileForeignKey, FacileRequired]
        public long CustomerID { get; set; }

        [Editable(false)]
        public DateTime OrderDate { get; set; }

        [Editable(false)]
        public DateTime? SubmittedDate { get; set; }

        [Editable(false)]
        public DateTime? ShippedDate { get; set; }

        public override long ID
        {
            get { return OrderID; }
            set { OrderID = value; }
        }
    }
}
