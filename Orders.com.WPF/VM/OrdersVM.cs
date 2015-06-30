using Orders.com.BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class OrdersVM : ViewModelBase, IListener<OrderUpdatedEvent>
    {
        private OrderService _ordersService;
        private ObservableCollection<OrderVM> _orders;
        private ICommand _addOrderCommand;
        private ICommand _saveOrdersCommand;
        private ICommand _loadOrdersCommand;
        private ICommand _deleteSelectedCommand;
        private EventAggregator _eventAggregator;

        public OrdersVM(OrderService categoryService, EventAggregator eventAggregator)
        {
            _ordersService = categoryService;
            //_addOrderCommand = new Command(() => AddOrder());
            //_saveOrdersCommand = new Command(() => SaveOrders());
            _loadOrdersCommand = new Command(() => LoadOrders());
            _deleteSelectedCommand = new Command(() => DeleteSelectedVM());
            _eventAggregator = eventAggregator;
            _eventAggregator.AddListener<OrderUpdatedEvent>(this, true);
        }

        public OrderVM SelectedOrder
        {
            get;
            set;
        }

        public ICommand AddOrderCommand
        {
            get { return _addOrderCommand; }
        }

        public ICommand SaveOrdersCommand
        {
            get { return _saveOrdersCommand; }
        }

        public ICommand LoadOrdersCommand
        {
            get { return _loadOrdersCommand; }
        }

        public ICommand DeleteSelectedCommand
        {
            get { return _deleteSelectedCommand; }
        }

        public IEnumerable<OrderVM> Orders
        {
            get { return _orders; }
            set
            {
                _orders = new ObservableCollection<OrderVM>(value);
                OnPropertyChanged("Orders");
            }
        }

        private async Task LoadOrders()
        {
            var result = await _ordersService.GetAllCommand(0, 10).ExecuteAsync();
            var vms = result.Value.Select(c => new OrderVM(c));
            Orders = vms.ToArray();
        }

        //private async Task SaveOrders()
        //{
        //    var results = Orders.Select(vm => vm.SaveAsync()).ToArray();
        //    await Task.WhenAll(results);
        //}

        //private void AddOrder()
        //{
        //    _orders.Add(new OrderVM(_ordersService));
        //}

        private async Task DeleteSelectedVM()
        {
            if (SelectedOrder != null && !SelectedOrder.IsNew)
            {
                await _ordersService.DeleteCommand(SelectedOrder.ID).ExecuteAsync();
                _orders.Remove(SelectedOrder);
                SelectedOrder = null;
            }
        }

        public void Handle(OrderUpdatedEvent message)
        {
            System.Diagnostics.Debug.WriteLine(message.Order.OrderID);
        }
    }
}
