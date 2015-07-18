using Facile;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class OrderStatusService : OrdersDotComServiceBase<OrderStatus> 
    {
        public OrderStatusService(IOrderStatusDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
