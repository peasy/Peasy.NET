using Orders.com.Core.Domain;
using Orders.com.Core.QueryData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.Core.DataProxy
{
    public interface IOrderDataProxy : IOrdersDotComDataProxy<Order>
    {
        IEnumerable<OrderInfo> GetAll(int start, int pageSize);
        Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize);
        IEnumerable<Order> GetByCustomer(long customerID);
        Task<IEnumerable<Order>> GetByCustomerAsync(long customerID);
    }
}
