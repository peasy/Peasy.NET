using System;
using Orders.com.Core.Domain;

namespace Orders.com.Core.Extensions
{
    public class OrderStatusConstants
    {
        public const long PENDING_STATUS = 1;
        public const long SUBMITTED_STATUS = 2;
        public const long SHIPPED_STATUS = 3;
        public const long BACK_statusIDContainerED_STATE = 4;
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
                case OrderStatusConstants.BACK_statusIDContainerED_STATE:
                    return new BackOrderedState(order);
                default:
                    return new NoneState(order);
            }
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
        public abstract void SetBackOrderedState();

        public bool IsBackOrdered
        {
            get { return _statusIDContainer.OrderStatusID == OrderStatusConstants.BACK_statusIDContainerED_STATE; }
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
            get { return IsSubmitted; }
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

        public override void SetBackOrderedState()
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

    public class BackOrderedState : OrderStateBase
    {
        private IOrderStatusIDContainer _statusIDContainer;

        public BackOrderedState(IOrderStatusIDContainer order) : base(order)
        {
            _statusIDContainer = order; 
        }

        public override void SetPendingState()
        {
        }

        public override void SetBackOrderedState()
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
            get { return "BackOrdered"; }
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

        public override void SetBackOrderedState()
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

        public override void SetBackOrderedState()
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

        public override void SetBackOrderedState()
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
