using Orders.com.BLL;
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
        public CustomerOrderWindow()
        {
            InitializeComponent();
        }

        public CustomerOrderWindow(CustomerService customerService, OrderItemService orderItemService, ProductService productService)
        {
            InitializeComponent();
            var vm = new CustomerOrderVM(customerService, orderItemService, productService);
            DataContext = vm;
        }
    }
}
