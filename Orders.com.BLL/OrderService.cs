using Facile;
using Facile.Extensions;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class OrderService : BusinessServiceBase<Order, int>
    {
        public OrderService(IServiceDataProxy<Order, int> dataProxy) : base(dataProxy)
        {
        }

        protected override void OnBeforeInsertCreated(Order entity)
        {
            entity.OrderDate = DateTime.Now;
        }

    }
}
