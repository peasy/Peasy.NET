using Facile.Core;
using Facile.Core.Extensions;
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

namespace Orders.com.BLL
{
    public class ShipOrderItemCommand : Command<OrderItem>
    {
        private IOrderItemDataProxy _orderItemDataProxy;
        private InventoryItemService _inventoryService;
        private long _orderItemID;

        public ShipOrderItemCommand(long orderItemID, IOrderItemDataProxy orderItemDataProxy, InventoryItemService inventoryService)
        {
            _orderItemID = orderItemID;
            _orderItemDataProxy = orderItemDataProxy;
            _inventoryService = inventoryService;
        }

        private OrderItem CurrentOrderItem { get; set; }

        protected override OrderItem OnExecute()
        {
            try
            {
                _inventoryService.DecrementQuantityOnHandCommand(CurrentOrderItem.ProductID, CurrentOrderItem.Quantity).Execute();
            }
            catch (InsufficientStockAmountException)
            {
                return _orderItemDataProxy.BackOrder(_orderItemID, DateTime.Now);
            }

            return _orderItemDataProxy.Ship(_orderItemID, DateTime.Now);
        }

        protected override async Task<OrderItem> OnExecuteAsync()
        {
            try
            {
                await _inventoryService.DecrementQuantityOnHandCommand(CurrentOrderItem.ProductID, CurrentOrderItem.Quantity).ExecuteAsync();
            }
            catch (InsufficientStockAmountException)
            {
                return await _orderItemDataProxy.BackOrderAsync(_orderItemID, DateTime.Now);
            }

            return await _orderItemDataProxy.ShipAsync(_orderItemID, DateTime.Now);
        }

        public IEnumerable<IRule> GetRules()
        {
            yield return new CanShipOrderItemRule(CurrentOrderItem);
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            CurrentOrderItem = _orderItemDataProxy.GetByID(_orderItemID);

            foreach (var error in GetRules().GetValidationErrors())
                yield return error;

            foreach (var error in _inventoryService.DecrementQuantityOnHandCommand(CurrentOrderItem.ProductID, CurrentOrderItem.Quantity).GetErrors())
                yield return error;
        }
    }
}
