using Facile;
using Facile.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.Core.Domain
{
    public class Order : DomainBase
    {
        public long OrderID { get; set; }

        [FacileForeignKey, FacileRequired]
        public long CustomerID { get; set; }

        [Editable(false)]
        public DateTime OrderDate { get; set; }

        public override long ID
        {
            get { return OrderID; }
            set { OrderID = value; }
        }
    }
}
