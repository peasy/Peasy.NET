using Orders.com.DataProxy;
using Orders.com.Domain;

namespace Orders.com.BLL
{
    public class OrderStatusService : OrdersDotComServiceBase<OrderStatus>, IOrderStatusService
    {
        public OrderStatusService(IOrderStatusDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
