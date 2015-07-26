using Facile.Core;
using Orders.com.BLL.Rules;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.Core.QueryData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Orders.com.BLL
{
    public class OrderService : OrdersDotComServiceBase<Order>
    {
        public OrderService(IOrderDataProxy dataProxy) : base(dataProxy)
        {
        }

        protected override void OnBeforeInsertCommandExecuted(Order entity)
        {
            entity.OrderDate = DateTime.Now;
            entity.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
        }
        
        public ICommand<IEnumerable<OrderInfo>> GetAllCommand(int start, int pageSize)
        {
            var proxy = DataProxy as IOrderDataProxy;
            return new ServiceCommand<IEnumerable<OrderInfo>>
            (
                executeMethod: () => proxy.GetAll(start, pageSize),
                executeAsyncMethod: () => proxy.GetAllAsync(start, pageSize)
            );
        }

        public override ICommand DeleteCommand(long id)
        {
            //TODO: create a DeleteOrderCommand that takes OrderService and OrderItemService, and return that command here
            return base.DeleteCommand(id);
        }

        public ICommand<Order> SubmitCommand(long orderID)
        {
            var proxy = DataProxy as IOrderDataProxy;
            return new ServiceCommand<Order>
            (
                executeMethod: () => proxy.Submit(orderID, DateTime.Now),
                executeAsyncMethod: () => proxy.SubmitAsync(orderID, DateTime.Now),
                getValidationResultsMethod: () => Enumerable.Empty<ValidationResult>(),
                getBusinessRulesResultsMethod: () => GetBusinessRulesResults(GetBusinessRulesForSubmit(orderID))
            );
        }

        private IEnumerable<IRule> GetBusinessRulesForSubmit(long orderID)
        {
            if (!IsLatencyProne)
            {
                var order = DataProxy.GetByID(orderID);
                yield return new CanSubmitOrderRule(order);        
            }
        }

        public ICommand<Order> ShipCommand(long orderID)
        {
            //TODO: decrement inventory service
            //TODO: create a ShipOrderCommand that requires OrderService and Inventory Service, and return that command here
            // perform auth check?
            var proxy = DataProxy as IOrderDataProxy;
            return new ServiceCommand<Order>
            (
                executeMethod: () => proxy.Ship(orderID, DateTime.Now),
                executeAsyncMethod: () => proxy.ShipAsync(orderID, DateTime.Now) 
            );
        }
    }
}
