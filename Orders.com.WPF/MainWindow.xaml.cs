using Orders.com.BLL;
using Orders.com.Core.Domain;
using Orders.com.DAL.EF;
using Orders.com.WPF.VM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerService _customersService;
        private OrderService _ordersService;
        private OrderItemService _orderItemsService;
        private ProductService _productsService;
        private CategoryService _categoriesService;
        private EventAggregator _eventAggregator = new EventAggregator();
        private InventoryItemService _inventoryService;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _inventoryService = new InventoryItemService(new InventoryItemRepository());
            _orderItemsService = new OrderItemService(new OrderItemRepository(), _inventoryService);
            _ordersService = new OrderService(new OrderRepository(), _orderItemsService, _inventoryService);
            _customersService = new CustomerService(new CustomerRepository());
            _productsService = new ProductService(new ProductRepository());
            _categoriesService = new CategoryService(new CategoryRepository());
            this.DataContext = new MainWindowVM(_eventAggregator, _customersService, _productsService, _categoriesService, _ordersService);
        }

        public MainWindowVM VM
        {
            get { return this.DataContext as MainWindowVM; }
        }

        private void addCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var customerOrderWindow = new CustomerOrderWindow(_ordersService, _customersService, _orderItemsService, VM, _eventAggregator);
            customerOrderWindow.Owner = this;
            var result = customerOrderWindow.ShowDialog();
        }

        private void editCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var currentOrder = VM.OrdersVM.SelectedOrder;
            var customerOrderWindow = new CustomerOrderWindow(currentOrder, _ordersService, _customersService, _orderItemsService, VM, _eventAggregator);
            customerOrderWindow.Owner = this;
            customerOrderWindow.ShowDialog();
        }
   }
}
