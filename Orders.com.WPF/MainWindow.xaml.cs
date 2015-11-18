using Orders.com.BLL;
using Orders.com.DAL.Http;
using Orders.com.DAL.InMemory;
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
            var productsDataProxy = new ProductsHttpServiceProxy();
            var inventoryDataProxy = new InventoryItemsHttpServiceProxy();
            var customerDataProxy = new CustomersHttpServiceProxy();
            var orderItemDataProxy = new OrderItemsHttpServiceProxy();
            var orderRepository = new OrdersHttpServiceProxy();
            var categoriesDataProxy = new CategoriesHttpServiceProxy();
            _inventoryService = new InventoryItemService(inventoryDataProxy);
            _orderItemsService = new OrderItemClientService(orderItemDataProxy, productsDataProxy, inventoryDataProxy, new DTCTransactionContext());
            _ordersService = new OrderService(orderRepository, _orderItemsService, new DTCTransactionContext());
            _customersService = new CustomerService(customerDataProxy, _ordersService);
            _productsService = new ProductClientService(productsDataProxy, orderRepository, _inventoryService, new DTCTransactionContext());
            _categoriesService = new CategoryService(categoriesDataProxy, productsDataProxy);
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
