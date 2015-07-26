using Orders.com.Core.Domain;

namespace Orders.com.Core.Extensions
{
    public class OrderStatusConstants
    {
        public const long PENDING_STATUS = 1;
        public const long SUBMITTED_STATUS = 2;
        public const long SHIPPED_STATUS = 3;
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
                default:
                    return new PendingState(order);
            }
        }
    }

    public abstract class OrderStateBase
    {
        private IOrderStatusIDContainer _order;

        public OrderStateBase(IOrderStatusIDContainer order)
        {
            _order = order;
        }

        public abstract string Name { get; }
        public abstract void SetPendingState();
        public abstract void SetSubmittedState();
        public abstract void SetShippedState();

        public bool IsPending
        {
            get { return _order.OrderStatusID == OrderStatusConstants.PENDING_STATUS; }
        }

        public bool IsSubmitted
        {
            get { return _order.OrderStatusID == OrderStatusConstants.SUBMITTED_STATUS; }
        }

        public bool IsShipped
        {
            get { return _order.OrderStatusID == OrderStatusConstants.SHIPPED_STATUS; }

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

    public class PendingState : OrderStateBase
    {
        private IOrderStatusIDContainer _order;

        public PendingState(IOrderStatusIDContainer order) : base(order)
        {
            _order = order; 
        }

        public override void SetPendingState()
        {
        }

        public override void SetSubmittedState()
        {
            _order.OrderStatusID = OrderStatusConstants.SUBMITTED_STATUS;
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
        private IOrderStatusIDContainer _order;

        public SubmittedState(IOrderStatusIDContainer order) : base(order)
        {
            _order = order; 
        }

        public override void SetPendingState()
        {
            _order.OrderStatusID = OrderStatusConstants.PENDING_STATUS;
        }

        public override void SetSubmittedState()
        {
        }

        public override void SetShippedState()
        {
            _order.OrderStatusID = OrderStatusConstants.SHIPPED_STATUS;
        }

        public override string Name
        {
            get { return "Submitted"; }
        }
    }

    public class ShippedState : OrderStateBase
    {
        private IOrderStatusIDContainer _order;

        public ShippedState(IOrderStatusIDContainer order) : base(order)
        {
            _order = order; 
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
