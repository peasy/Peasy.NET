
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
