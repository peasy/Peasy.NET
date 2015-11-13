using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.DataProxy
{
    public interface IOrderItemDataProxy : IOrdersDotComDataProxy<OrderItem>
    {
        IEnumerable<OrderItem> GetByOrder(long orderID);
        Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID);
    }
}
