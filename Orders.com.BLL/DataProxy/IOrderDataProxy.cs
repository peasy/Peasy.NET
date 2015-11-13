using Orders.com.Domain;
using Orders.com.QueryData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.DataProxy
{
    public interface IOrderDataProxy : IOrdersDotComDataProxy<Order>
    {
        IEnumerable<OrderInfo> GetAll(int start, int pageSize);
        Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize);
        IEnumerable<Order> GetByCustomer(long customerID);
        Task<IEnumerable<Order>> GetByCustomerAsync(long customerID);
        IEnumerable<Order> GetByProduct(long productID);
        Task<IEnumerable<Order>> GetByProductAsync(long productID);
    }
}
