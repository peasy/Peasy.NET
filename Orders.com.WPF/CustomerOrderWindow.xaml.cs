using Orders.com.BLL;
using Orders.com.WPF.VM;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for CustomerOrderWindow.xaml
    /// </summary>
    public partial class CustomerOrderWindow : Window
    {
        private OrderItemService _orderItemService;
        private ProductService _productService;
        private CategoryService _categoryService;
        private OrderService _orderService;
    
        public CustomerOrderWindow()
        {
            InitializeComponent();
        }

        public CustomerOrderWindow(OrderService orderService, CustomerService customerService, OrderItemService orderItemService, ProductService productService, CategoryService categoryService)
        {
            InitializeComponent();
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productService = productService;
            _categoryService = categoryService;
            var vm = new CustomerOrderVM(orderService, customerService, orderItemService, _categoryService, productService);
            DataContext = vm;
        }

        private CustomerOrderVM VM
        {
            get { return DataContext as CustomerOrderVM; }
        }

    }
}
