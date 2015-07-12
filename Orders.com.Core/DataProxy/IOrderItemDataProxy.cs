using Facile;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.DataProxy
{
    public interface IOrderItemDataProxy : IOrdersDotComDataProxy<OrderItem>
    {
        IEnumerable<OrderItem> GetByOrder(long orderID);
        Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID);
    }
}
