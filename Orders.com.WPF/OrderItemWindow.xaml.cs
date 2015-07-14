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
using Orders.com.WPF.VM;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        public OrderItemWindow()
        {
            InitializeComponent();
        }

        //public OrderItemWindow(OrderItemService orderItemService, CategoryService categoryService, ProductService productService)
        //{
        //    InitializeComponent();
        //    DataContext = new OrderItemVM(orderItemService, categoryService, productService);
        //}

        public OrderItemVM VM
        {
            get { return DataContext as OrderItemVM; }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
