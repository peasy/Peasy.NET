using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.Core.DataProxy
{
    public interface IOrderItemDataProxy : IOrdersDotComDataProxy<OrderItem>
    {
        IEnumerable<OrderItem> GetByOrder(long orderID);
        Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID);
        OrderItem Ship(long orderItemID, DateTime shippedOn);
        Task<OrderItem> ShipAsync(long orderItemID, DateTime shippedOn);
    }
}
