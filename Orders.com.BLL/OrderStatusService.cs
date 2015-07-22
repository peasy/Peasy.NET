using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

namespace Orders.com.BLL
{
    public class OrderStatusService : OrdersDotComServiceBase<OrderStatus> 
    {
        public OrderStatusService(IOrderStatusDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
