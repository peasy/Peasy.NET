using Orders.com.BLL;
using Orders.com.DAL.Mock;
using Orders.com.WPF.VM;
using System.Windows;
using System.Windows.Controls;

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
            var customerDataProxy = new CustomerRepository();
            var orderItemDataProxy = new OrderItemRepository();
            var orderRepository = new OrderRepository(customerDataProxy, orderItemDataProxy);
            _inventoryService = new InventoryItemService(inventoryDataProxy);
            _orderItemsService = new OrderItemService(orderItemDataProxy, productsDataProxy, inventoryDataProxy, new DTCTransactionContext());
            _ordersService = new OrderService(orderRepository, _orderItemsService, new DTCTransactionContext());
            _customersService = new CustomerService(customerDataProxy, _ordersService);
            _productsService = new ProductService(productsDataProxy, _ordersService, _inventoryService, new DTCTransactionContext());
            _categoriesService = new CategoryService(new CategoryRepository(), productsDataProxy);
            this.DataContext = new MainWindowVM(_eventAggregator, _customersService, _productsService, _categoriesService, _ordersService, _inventoryService);
        }

        public MainWindowVM VM
        {
            get { return this.DataContext as MainWindowVM; }
        }

        public InventoryItemService _inventoryService { get; private set; }

        private void addCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var customerOrderWindow = new CustomerOrderWindow(_ordersService, _customersService, _orderItemsService, _inventoryService, VM, _eventAggregator);
            customerOrderWindow.Owner = this;
            customerOrderWindow.Show();
        }

        private void editCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var currentOrder = VM.OrdersVM.SelectedOrder;
            var customerOrderWindow = new CustomerOrderWindow(currentOrder, _ordersService, _customersService, _orderItemsService, _inventoryService, VM, _eventAggregator);
            customerOrderWindow.Owner = this;
            customerOrderWindow.Show();
        }

        private void InventoryTab_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source is TabItem)
                VM.InventoryItemsVM.LoadInventoryCommand.Execute(null);
        }
    }
}
