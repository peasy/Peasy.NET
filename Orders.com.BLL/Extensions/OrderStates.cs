using System;
using System.Linq;
using Orders.com.Domain;
using System.Collections.Generic;

namespace Orders.com.Extensions
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
        public abstract IOrderStatusIDContainer SetPendingState();
        public abstract IOrderStatusIDContainer SetSubmittedState();
        public abstract IOrderStatusIDContainer SetShippedState();
        public abstract IOrderStatusIDContainer SetBackorderedState();

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

        public override IOrderStatusIDContainer SetBackorderedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetPendingState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetSubmittedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetShippedState()
        {
            return _statusIDContainer;
        }
    }

    public class BackorderedState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public BackorderedState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order;
        }

        public override IOrderStatusIDContainer SetPendingState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetBackorderedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetSubmittedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetShippedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
            return _statusIDContainer;
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

        public override IOrderStatusIDContainer SetPendingState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetBackorderedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetSubmittedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetShippedState()
        {
            return _statusIDContainer;
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

        public override IOrderStatusIDContainer SetBackorderedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.BACK_ORDERED_STATE;
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetPendingState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetSubmittedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetShippedState()
        {
            _statusIDContainer.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
            return _statusIDContainer;
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

        public override IOrderStatusIDContainer SetBackorderedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetPendingState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetSubmittedState()
        {
            return _statusIDContainer;
        }

        public override IOrderStatusIDContainer SetShippedState()
        {
            return _statusIDContainer;
        }

        public override string Name
        {
            get { return "Shipped"; }
        }
    }

}
