using Orders.com.BLL;
using Orders.com.DAL.EF;
using Orders.com.WPF.VM;
using System.Windows;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private CustomerService _customersService;
        private OrderService _ordersService;
        private OrderItemService _orderItemsService;
        private ProductService _productsService;
        private CategoryService _categoriesService;
        private EventAggregator _eventAggregator = new EventAggregator();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var productsDataProxy = new ProductRepository();
            var inventoryDataProxy = new InventoryItemRepository();
            var inventoryService = new InventoryItemService(inventoryDataProxy);
            _orderItemsService = new OrderItemService(new OrderItemRepository(), productsDataProxy, inventoryService);
            _ordersService = new OrderService(new OrderRepository(), _orderItemsService);
            _customersService = new CustomerService(new CustomerRepository());
            _productsService = new ProductService(productsDataProxy);
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
            //customerOrderWindow.ShowDialog();
            customerOrderWindow.Show();
        }
   }
}
