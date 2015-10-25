using Peasy;
using Peasy.Core;
using Peasy.Core.Extensions;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Exceptions;
using Orders.com.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;

namespace Orders.com.BLL.Commands
{
    public class ShipOrderItemCommand : Command<OrderItem>
    {
        private IOrderItemDataProxy _orderItemDataProxy;
        private InventoryItemService _inventoryService;
        private long _orderItemID;
        private ITransactionContext _transactionContext;
        private IInventoryItemDataProxy _inventoryDataProxy;

        public ShipOrderItemCommand(long orderItemID, IOrderItemDataProxy orderItemDataProxy, IInventoryItemDataProxy inventoryDataProxy, ITransactionContext transactionContext)
        {
            _orderItemID = orderItemID;
            _orderItemDataProxy = orderItemDataProxy;
            _inventoryDataProxy = inventoryDataProxy;
            _transactionContext = transactionContext;
        }

        private OrderItem CurrentOrderItem { get; set; }

        protected override OrderItem OnExecute()
        {
            return _transactionContext.Execute(() =>
            {
                var inventoryItem = _inventoryDataProxy.GetByProduct(CurrentOrderItem.ProductID);
                if (inventoryItem.QuantityOnHand - CurrentOrderItem.Quantity >= 0)
                {
                    CurrentOrderItem.OrderStatus().SetShippedState();
                    inventoryItem.QuantityOnHand -= CurrentOrderItem.Quantity;
                    _inventoryDataProxy.Update(inventoryItem);
                }
                else
                {
                    CurrentOrderItem.OrderStatus().SetBackorderedState();
                }
                return _orderItemDataProxy.Update(CurrentOrderItem);
            });
        }

        protected override async Task<OrderItem> OnExecuteAsync()
        {
            return await _transactionContext.ExecuteAsync(async () =>
            {
                var inventoryItem = await _inventoryDataProxy.GetByProductAsync(CurrentOrderItem.ProductID);
                if (inventoryItem.QuantityOnHand - CurrentOrderItem.Quantity >= 0)
                {
                    CurrentOrderItem.OrderStatus().SetShippedState();
                    inventoryItem.QuantityOnHand -= CurrentOrderItem.Quantity;
                    await _inventoryDataProxy.UpdateAsync(inventoryItem);
                }
                else
                {
                    CurrentOrderItem.OrderStatus().SetBackorderedState();
                }
                return await _orderItemDataProxy.UpdateAsync(CurrentOrderItem);
            });
        }

        public IEnumerable<IRule> GetRules()
        {
            yield return new CanShipOrderItemRule(CurrentOrderItem);
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            CurrentOrderItem = _orderItemDataProxy.GetByID(_orderItemID);
            foreach (var error in GetRules().GetBusinessRulesResults())
                yield return error;
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            CurrentOrderItem = await _orderItemDataProxy.GetByIDAsync(_orderItemID);
            return await GetRules().GetBusinessRulesResultsAsync();
        }
    }
}
