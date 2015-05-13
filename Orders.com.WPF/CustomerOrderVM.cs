using Orders.com.BLL;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF
{
    public class CustomerOrderVM : ViewModelBase
    {
        private CustomerService _customerService;
        private OrderItemService _orderItemService;
        private IEnumerable<Customer> _customers;
        private ObservableCollection<OrderItemVM> _orderItems;
        private int _customerID;
        private ICommand _addOrderItemCommand;
        private ProductService _productService;

        public CustomerOrderVM(CustomerService customerService, OrderItemService orderItemService, ProductService productService)
        {
            _customerService = customerService;
            _orderItemService = orderItemService;
            _productService = productService;
            _orderItems = new ObservableCollection<OrderItemVM>();
            _addOrderItemCommand = new Command(() => AddOrderItem());
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

        public int CurrentCustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public IEnumerable<OrderItemVM> OrderItems
        {
            get { return _orderItems; }
        }

        public ICommand AddOrderItemCommand
        {
            get { return _addOrderItemCommand; }
        }
        
        private void AddOrderItem()
        {
            _orderItems.Add(new OrderItemVM(_orderItemService, _productService));
        }

        private async void LoadCustomers()
        {
            var result = await _customerService.GetAllCommand().ExecuteAsync();
            Customers = result.Value;
        }
    }
}
