using Facile.Core.Extensions;
using Facile.Extensions;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class CustomerOrderVM : OrdersDotComVMBase<Order>
    {
        private OrderItemService _orderItemService;
        private OrderService _orderService;
        private ObservableCollection<OrderItemVM> _orderItems;
        private ICommand _saveOrderCommand;
        private ICommand _submitOrderCommand;
        private ICommand _shipOrderCommand;
        private ICommand _addOrderItemCommand;
        private ICommand _deleteSelectedItemCommand;
        private ICommand _refreshCommand;
        private EventAggregator _eventAggregator;
        private MainWindowVM _mainVM;

        public CustomerOrderVM(EventAggregator eventAggregator, OrderService orderService, OrderItemService orderItemService, MainWindowVM mainVM)
            : base(orderService)
        {
            Setup(eventAggregator, orderService, orderItemService, mainVM);
        }

        public CustomerOrderVM(EventAggregator eventAggregator, Order order, OrderService orderService, OrderItemService orderItemService, MainWindowVM mainVM)
            : base(order, orderService)
        {
            Setup(eventAggregator, orderService, orderItemService, mainVM);
        }

        private void Setup(EventAggregator eventAggregator, OrderService orderService, OrderItemService orderItemService, MainWindowVM mainVM)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _mainVM = mainVM;
            _eventAggregator = eventAggregator;
            _orderItems = new ObservableCollection<OrderItemVM>();
            _saveOrderCommand = new Command(async () => await SaveAsync());
            _addOrderItemCommand = new Command(() => AddOrderItem(), () => CanAdd);
            _deleteSelectedItemCommand = new Command(async () => await DeleteSelectedItemAsync());
            _submitOrderCommand = new Command(async () => await SubmitAsync());
            _shipOrderCommand = new Command(async () => await ShipAsync());
            _refreshCommand = new Command(async () => await LoadOrderItemsAsync());
        }

        public IEnumerable<CustomerVM> Customers
        {
            get { return _mainVM.CustomersVM.Customers; }
        }

        public OrderItemVM SelectedOrderItem
        {
            get;
            set;
        }

        public long ID
        {
            get { return CurrentEntity.OrderID; }
        }

        public decimal Total
        {
            get
            {
                if (!OrderItems.Any()) return 0;
                return OrderItems.Sum(o => o.Amount.GetValueOrDefault());
            }
        }

        public long CurrentCustomerID
        {
            get { return CurrentEntity.CustomerID; }
            set
            {
                CurrentEntity.CustomerID = value;
                IsDirty = true;
                OnPropertyChanged("CurrentCustomerID", "CanSave");
            }
        }

        public OrderStateBase Status
        {
            get { return OrderItems.OrderStatus(); }
        }

        public IEnumerable<OrderItemVM> OrderItems
        {
            get { return _orderItems; }
        }

        public ICommand AddOrderItemCommand
        {
            get { return _addOrderItemCommand; }
        }

        public ICommand SaveOrderCommand
        {
            get { return _saveOrderCommand; }
        }

        public ICommand SubmitOrderCommand
        {
            get { return _submitOrderCommand; }
        }

        public ICommand ShipOrderCommand
        {
            get { return _shipOrderCommand; }
        }

        public ICommand DeleteSelectedItemCommand
        {
            get { return _deleteSelectedItemCommand; }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand; }
        }

        protected override void OnInsertSuccess(Order result)
        {
            OnPropertyChanged("ID");
            _eventAggregator.SendMessage<OrderInsertedEvent>(new OrderInsertedEvent { Order = this });
        }

        protected override void OnUpdateSuccess(Order result)
        {
            _eventAggregator.SendMessage<OrderUpdatedEvent>(new OrderUpdatedEvent(this));
        }

        public bool CanChangeCustomer
        {
            get { return OrderItems.All(i => i.Status is ShippedState == false); }
        }

        public bool CanAdd
        {
            get { return !IsNew || IsDirty; }
        }
        
        public override bool CanSave
        {
            get { return IsDirty || OrderItems.Any(i => i.IsDirty); }
        }

        public bool CanSubmit
        {
            get { return OrderItems.Any(i => i.Status == null || i.Status.CanSubmit) &&
                         OrderItems.All(i => (!i.IsDirty && !i.IsNew));
            }
        }

        public bool CanShip
        {
            get { return OrderItems.Any(i => i.Status == null || i.Status.CanShip) &&
                          OrderItems.All(i => (!i.IsDirty && !i.IsNew));
            }
        }

        public override async Task SaveAsync()
        {
            if (CanSave)
            {
                await base.SaveAsync();
                var results = OrderItems.ForEach(item => item.OrderID = CurrentEntity.OrderID)
                                        .Select(vm => vm.SaveAsync())
                                        .ToArray();
                await Task.WhenAll(results);
                OnPropertyChanged("CanSave", "CanSubmit", "CanShip");
            }
        }

        public async Task SubmitAsync()
        {
            if (CanSubmit)
            {
                var submitTasks = OrderItems.Select(i => i.SubmitAsync()).ToArray();
                await Task.WhenAll(submitTasks);
                _eventAggregator.SendMessage<OrderUpdatedEvent>(new OrderUpdatedEvent(this));
                OnPropertyChanged("CanSubmit");
            }
        }

        public async Task ShipAsync()
        {
            if (CanShip)
            {
                var shipTasks = OrderItems.Select(i => i.SubmitAsync()).ToArray();
                await Task.WhenAll(shipTasks);
                _eventAggregator.SendMessage<OrderUpdatedEvent>(new OrderUpdatedEvent(this));
                OnPropertyChanged("CanShip", "CanChangeCustomer");
            }
        }

        private void AddOrderItem()
        {
            if (CanAdd)
            {
                var item = new OrderItemVM(_orderItemService, _mainVM);
                SubscribeHandlers(item);
                _orderItems.Add(item);
            }
        }

        private async Task LoadOrderItemsAsync()
        {
            var result = await _orderItemService.GetByOrderCommand(CurrentEntity.OrderID).ExecuteAsync();
            _orderItems.Clear();
            result.Value.ForEach(i => LoadOrderItem(i));
            OnPropertyChanged("CurrentCustomerID", "CanSave", "CanSubmit", "CanShip", "CanChangeCustomer", "Total");
        }

        private void LoadOrderItem(OrderItem orderItem)
        {
            var item = new OrderItemVM(orderItem, _orderItemService, _mainVM);
            SubscribeHandlers(item);
            _orderItems.Add(item);
        }

        private void SubscribeHandlers(OrderItemVM item)
        {
            item.EntitySaved += (s, e) =>
            {
                _eventAggregator.SendMessage<OrderUpdatedEvent>(new OrderUpdatedEvent(this));
            };
            item.EntityDeleted += (s, e) =>
            {
                _orderItems.Remove(SelectedOrderItem);
                OnPropertyChanged("Total", "CanChangeCustomer");
                _eventAggregator.SendMessage<OrderUpdatedEvent>(new OrderUpdatedEvent(this));
            };
            item.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged("Total", "CanSave");
                if (e.PropertyName == "ShippedOn")
                {
                    _eventAggregator.SendMessage<OrderUpdatedEvent>(new OrderUpdatedEvent(this));
                    OnPropertyChanged("CanChangeCustomer");
                }
            };
        }

        private async Task DeleteSelectedItemAsync()
        {
            if (SelectedOrderItem.IsNew)
                _orderItems.Remove(SelectedOrderItem);
            else
                await SelectedOrderItem.DeleteAsync();
            OnPropertyChanged("CanSubmit", "CanShip");
        }
    }
}
