using Facile.Core;
using Facile.Core.Extensions;
using Facile.Extensions;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Exceptions;
using Orders.com.BLL.Rules;
using Facile;
using Facile.Rules;

namespace Orders.com.BLL.Commands
{
    public class DeleteOrderCommand : Command
    {
        private long _orderID;
        private IOrderDataProxy _orderDataProxy;
        private OrderItemService _orderItemService;
        private ITransactionContext _transactionContext;

        public DeleteOrderCommand(long orderID, IOrderDataProxy orderDataProxy, OrderItemService orderItemService, ITransactionContext transactionContext)
        {
            _orderID = orderID;
            _orderDataProxy = orderDataProxy;
            _orderItemService = orderItemService;
            _transactionContext = transactionContext;
        }

        private IEnumerable<OrderItem> CurrentOrderItems { get; set; }

        protected override void OnExecute()
        {
            _transactionContext.Execute(() =>
            {
                _orderDataProxy.Delete(_orderID);
                CurrentOrderItems.ForEach(i =>
                {
                    _orderItemService.DeleteCommand(i.ID).Execute();
                });
            });
        }

        protected override async Task OnExecuteAsync()
        {
            await _transactionContext.ExecuteAsync(async () =>
            {
                await _orderDataProxy.DeleteAsync(_orderID);
                foreach (var item in CurrentOrderItems)
                    await _orderItemService.DeleteCommand(item.ID).ExecuteAsync();
            });
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            var rule = _orderID.CreateValueRequiredRule("id").Validate();
            if (!rule.IsValid)
                yield return new ValidationResult(rule.ErrorMessage);
            else
            {
                CurrentOrderItems = _orderItemService.GetByOrderCommand(_orderID).Execute().Value;
                var errors = CurrentOrderItems.SelectMany(i => _orderItemService.DeleteCommand(i.ID).GetErrors());
                foreach (var error in errors)
                    yield return error;
            }
        }
    }
}
