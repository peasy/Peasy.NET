using System.Collections.Generic;
using Orders.com.Core.Domain;
using Peasy.Core;

namespace Orders.com.BLL
{
    public interface IOrderItemService : IService<OrderItem, long>
    {
        ICommand<IEnumerable<OrderItem>> GetByOrderCommand(long orderID);
        ICommand<OrderItem> ShipCommand(long orderItemID);
        ICommand<OrderItem> SubmitCommand(long orderItemID);
    }
}