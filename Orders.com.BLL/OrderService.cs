using Facile;
using Facile.Core;
using Facile.Extensions;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.QueryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class OrderService : OrdersDotComServiceBase<Order>
    {
        public OrderService(IOrderDataProxy dataProxy) : base(dataProxy)
        {
        }

        protected override void OnBeforeInsertCommandExecuted(Order entity)
        {
            entity.OrderDate = DateTime.Now;
        }
        
        public ICommand<IEnumerable<OrderInfo>> GetAllCommand(int start, int pageSize)
        {
            var proxy = DataProxy as IOrderDataProxy;
            return new ServiceCommand<IEnumerable<OrderInfo>>
            (
                executeMethod: () => proxy.GetAll(start, pageSize),
                executeAsyncMethod: () => proxy.GetAllAsync(start, pageSize)
            );
        }
    }
}
