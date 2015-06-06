using Facile.Core.Extensions;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class CustomerOrderVM : EntityViewModelBase<Order, int>
    {
        private CustomerService _customerService;
        private OrderItemService _orderItemService;
        private OrderService _orderService;
        private IEnumerable<Customer> _customers;
        private ObservableCollection<OrderItemVM> _orderItems;
        private ICommand _saveOrderCommand;
        private ICommand _addOrderItemCommand;
        private ProductService _productService;
        private CategoryService _categoryService;

        public CustomerOrderVM(OrderService orderService, CustomerService customerService, OrderItemService orderItemService, CategoryService categoryService, ProductService productService)
            : base(orderService)
        {
            Setup(orderService, customerService, orderItemService, categoryService, productService);
        }

        public CustomerOrderVM(Order order, OrderService orderService, CustomerService customerService, OrderItemService orderItemService, CategoryService categoryService, ProductService productService)
            : base(order, orderService)
        {
            Setup(orderService, customerService, orderItemService, categoryService, productService);
        }

        private void Setup(OrderService orderService, CustomerService customerService, OrderItemService orderItemService, CategoryService categoryService, ProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _orderItemService = orderItemService;
            _categoryService = categoryService;
            _productService = productService;
            _orderItems = new ObservableCollection<OrderItemVM>();
            _saveOrderCommand = new Command(() => SaveAsync(), CanSave);
            _addOrderItemCommand = new Command(() => AddOrderItem(), CanSave);
        }

        public IEnumerable<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    LoadCustomers();

                return _customers;
            }
            private set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        public int ID
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

        public int CurrentCustomerID
        {
            get { return CurrentEntity.CustomerID; }
            set
            {
                CurrentEntity.CustomerID = value;
                IsDirty = true;
                OnPropertyChanged("CurrentCustomerID");
            }
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

        private async void LoadCustomers()
        {
            var result = await _customerService.GetAllCommand().ExecuteAsync();
            Customers = result.Value;
        }

        protected override void OnCommandExecutionSuccess(Facile.Core.ExecutionResult<Order> result)
        {
            OnPropertyChanged("ID");
        }

        public override async Task SaveAsync()
        {
            await base.SaveAsync();
            var results = OrderItems.ForEach(item => item.OrderID = CurrentEntity.OrderID)
                                    .Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }

        public async Task UpdateOrder(OrderItemVM orderItemVM)
        {
            if (orderItemVM.IsNew)
                await AddOrderItem(orderItemVM);
            else
                await UpdateOrderItem(orderItemVM);
        }

        private void AddOrderItem()
        {
            var item = new OrderItemVM(_orderItemService, _categoryService, _productService);
            item.PropertyChanged += (s, e) => OnPropertyChanged("Total");
            _orderItems.Add(item);
        }

        private async Task AddOrderItem(OrderItemVM orderItemVM)
        {
            if (IsNew) await SaveAsync();
            orderItemVM.OrderID = ID;
            await orderItemVM.SaveAsync();
            _orderItems.Add(orderItemVM);
        }

        private async Task UpdateOrderItem(OrderItemVM orderItemVM)
        {
            await orderItemVM.SaveAsync();
        }
    }
}
