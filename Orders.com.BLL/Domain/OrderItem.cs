using Peasy;
using Peasy.Attributes;
using System.ComponentModel.DataAnnotations;
using System;

namespace Orders.com.Domain
{
    public class OrderItem : DomainBase, IOrderStatusIDContainer
    {
        public long OrderItemID { get; set; }

        [PeasyForeignKey, PeasyRequired]
        public long OrderID { get; set; }

        [PeasyForeignKey, PeasyRequired, Editable(false)]
        public long ProductID { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        [Editable(false)]
        public DateTime? BackorderedDate { get; set; }

        [Editable(false)]
        public DateTime? ShippedDate { get; set; }

        //[Editable(false)]
        public DateTime? SubmittedDate { get; set; }

        //[Editable(false)]
        public long OrderStatusID { get; set; }

        public override long ID
        {
            get { return OrderItemID; }
            set { OrderItemID = value; }
        }

    }
}
