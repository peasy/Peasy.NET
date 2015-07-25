using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL.Rules
{
    public class CanSubmitOrderRule : RuleBase
    {
        private long _orderID;
        private IOrderDataProxy _orderDataProxy;

        public CanSubmitOrderRule(long orderID, IOrderDataProxy orderDataProxy)
        {
            _orderID = orderID;
            _orderDataProxy = orderDataProxy;
        }

        protected override void OnValidate()
        {
            var order = _orderDataProxy.GetByID(_orderID); 
            if (!order.OrderStatus().IsPending)
            {
                Invalidate(string.Format("Order ID:{0} is in a {1} state and cannot be submitted", _orderID.ToString(), order.OrderStatus().Name));
            }
        }
    }
}
