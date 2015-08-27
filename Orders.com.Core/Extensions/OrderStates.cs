using System;
using System.Linq;
using Orders.com.Core.Domain;
using System.Collections.Generic;

namespace Orders.com.Core.Extensions
{
    public class OrderStatusConstants
    {
        public const long PENDING_STATUS = 1;
        public const long SUBMITTED_STATUS = 2;
        public const long BACK_ORDERED_STATE = 3;
        public const long SHIPPED_STATUS = 4;
    }

    public static class OrderStatusExtensions
    {
        public static OrderStateBase OrderStatus(this IOrderStatusIDContainer order)
        {
            switch (order.OrderStatusID)
            {
                case OrderStatusConstants.PENDING_STATUS:
                    return new PendingState(order);
                case OrderStatusConstants.SUBMITTED_STATUS:
                    return new SubmittedState(order);
                case OrderStatusConstants.SHIPPED_STATUS:
                    return new ShippedState(order);
                case OrderStatusConstants.BACK_ORDERED_STATE:
                    return new BackorderedState(order);
                default:
                    return new NoneState(order);
            }
        }

        public static OrderStateBase OrderStatus(this IEnumerable<IOrderStatusIDContainer> items)
        {
            if (!items.Any()) return null;

            if (items.Any(i => i.OrderStatus() is BackorderedState))
                return (items.First(i => i.OrderStatus() is BackorderedState)).OrderStatus();

            var relevantItems = items.Where(i => i.OrderStatus() is NoneState == false);
            if (relevantItems.Any())
                return relevantItems.First(i => i.OrderStatusID == relevantItems.Min(o => o.OrderStatusID)).OrderStatus();

            return items.First().OrderStatus();
        }
    }

    public abstract class OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public OrderStateBase(IOrderStatusIDContainer order)
        {
            _statusIDContainer = order;
        }

        public abstract string Name { get; }
        public abstract void SetPendingState();
        public abstract void SetSubmittedState();
        public abstract void SetShippedState();
        public abstract void SetBackorderedState();

        public bool IsBackordered
        {
            get { return _statusIDContainer.OrderStatusID == OrderStatusConstants.BACK_ORDERED_STATE; }
        }

        public bool IsPending
        {
            get { return _statusIDContainer.OrderStatusID == OrderStatusConstants.PENDING_STATUS; }
        }

        public bool IsSubmitted
        {
            get { return _statusIDContainer.OrderStatusID == OrderStatusConstants.SUBMITTED_STATUS; }
        }

        public bool IsShipped
        {
            get { return _statusIDContainer.OrderStatusID == OrderStatusConstants.SHIPPED_STATUS; }
        }

        public bool CanSubmit
        {
            get { return IsPending; }
        }

        public bool CanShip
        {
            get { return IsSubmitted || IsBackordered; }
        }

    }

    public class NoneState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public NoneState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order; 
        }

        public override string Name
        {
            get { return string.Empty; }
        }

        public override void SetBackorderedState()
        {
        }

        public override void SetPendingState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
        }

        public override void SetSubmittedState()
        {
        }

        public override void SetShippedState()
        {
        }
    }

    public class BackorderedState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public BackorderedState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order; 
        }

        public override void SetPendingState()
        {
        }

        public override void SetBackorderedState()
        {
        }

        public override void SetSubmittedState()
        {
        }

        public override void SetShippedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
        }

        public override string Name
        {
            get { return "Backordered"; }
        }
    }

    public class PendingState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public PendingState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order; 
        }

        public override void SetPendingState()
        {
        }

        public override void SetBackorderedState()
        {
        }

        public override void SetSubmittedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
        }

        public override void SetShippedState()
        {
        }

        public override string Name
        {
            get { return "Pending"; }
        }
    }

    public class SubmittedState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public SubmittedState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order; 
        }

        public override void SetBackorderedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE;
        }

        public override void SetPendingState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
        }

        public override void SetSubmittedState()
        {
        }

        public override void SetShippedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
        }

        public override string Name
        {
            get { return "Submitted"; }
        }
    }

    public class ShippedState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public ShippedState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order; 
        }

        public override void SetBackorderedState()
        {
        }

        public override void SetPendingState()
        {
        }

        public override void SetSubmittedState()
        {
        }

        public override void SetShippedState()
        {
        }

        public override string Name
        {
            get { return "Shipped"; }
        }
    }

}
