using System.Collections.Generic;
using Orders.com.Domain;
using Orders.com.QueryData;
using Peasy.Core;

namespace Orders.com.BLL
{
    public interface IOrderService : IService<Order, long>
    {
        ICommand<IEnumerable<OrderInfo>> GetAllCommand(int start, int pageSize);
        ICommand<IEnumerable<Order>> GetByCustomerCommand(long customerID);
        ICommand<IEnumerable<Order>> GetByProductCommand(long productID);
    }
}