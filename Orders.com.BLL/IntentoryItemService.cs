using System;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Facile.Core;
using Facile.Extensions;
using System.Collections.Generic;
using Facile.Core.Extensions;

namespace Orders.com.BLL
{
    public class InventoryItemService : OrdersDotComServiceBase<InventoryItem> 
    {
        public InventoryItemService(IInventoryItemDataProxy dataProxy) : base(dataProxy)
        {
        }

        /// <summary>
        /// Not supported.  All updates to inventory must be made via Increment and Decrement commands
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ICommand<InventoryItem> UpdateCommand(InventoryItem entity)
        {
            throw new NotImplementedException();
        }

        public ICommand<InventoryItem> DecrementQuantityOnHandCommand(long productID, decimal quantity)
        {
            var proxy = DataProxy as IInventoryItemDataProxy;
            return new ServiceCommand<InventoryItem>
            (
                executeMethod: () => proxy.DecrementQuantityOnHand(productID, quantity),
                executeAsyncMethod: () => proxy.DecrementQuantityOnHandAsync(productID, quantity),
                getValidationRulesMethod: () => new[] { quantity.CreateValueRequiredRule("quantity") }.GetBusinessRulesResults(this.GetType().Name)
            );
        }

        public ICommand<InventoryItem> IncrementQuantityOnHandCommand(long productID, decimal quantity)
        {
            var proxy = DataProxy as IInventoryItemDataProxy;
            return new ServiceCommand<InventoryItem>
            (
                executeMethod: () => proxy.IncrementQuantityOnHand(productID, quantity),
                executeAsyncMethod: () => proxy.IncrementQuantityOnHandAsync(productID, quantity)
            );
        }

        public ICommand<InventoryItem> GetByProductCommand(long productID)
        {
            var proxy = DataProxy as IInventoryItemDataProxy;
            return new ServiceCommand<InventoryItem>
            (
                executeMethod: () => proxy.GetByProduct(productID),
                executeAsyncMethod: () => proxy.GetByProductAsync(productID)
            );
        }
    }
}
