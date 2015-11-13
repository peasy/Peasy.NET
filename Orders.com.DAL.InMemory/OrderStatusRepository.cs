using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;

namespace Orders.com.DAL.InMemory
{
    public class OrderStatusRepository : OrdersDotComMockBase<OrderStatus>, IOrderStatusDataProxy
    {
        protected override IEnumerable<OrderStatus> SeedDataProxy()
        {
            yield return new OrderStatus() { OrderStatusID = 1, Name = "Pending" };
            yield return new OrderStatus() { OrderStatusID = 2, Name = "Submitted" };
            yield return new OrderStatus() { OrderStatusID = 3, Name = "Shipped" };
        }
    }
}
