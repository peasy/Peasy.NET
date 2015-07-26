using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL.Commands
{
    public class SubmitOrderCommand : Command<Order>
    {
        private IOrderDataProxy _dataProxy;
        private long _orderID;

        public SubmitOrderCommand(long orderID, IOrderDataProxy dataProxy)
        {
            _orderID = orderID;
            _dataProxy = dataProxy;
        }

        protected override Order OnExecute()
        {
            return _dataProxy.Submit(_orderID, DateTime.Now);
        }

        protected override Task<Order> OnExecuteAsync()
        {
            return _dataProxy.SubmitAsync(_orderID, DateTime.Now);
        }

        public override IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> GetValidationErrors()
        {
            return base.GetValidationErrors();
        }
    }
}
