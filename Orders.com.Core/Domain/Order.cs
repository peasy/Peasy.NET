using Facile;
using Facile.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.Core.Domain
{
    public class Order : DomainBase, IOrderStatusIDContainer
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
