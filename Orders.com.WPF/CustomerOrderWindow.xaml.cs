using Orders.com.BLL;
using Orders.com.Core.Domain;
using Orders.com.Core.QueryData;
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
        public CustomerOrderWindow()
        {
            InitializeComponent();
        }

        public CustomerOrderWindow(OrderService orderService,
                                   CustomerService customerService,
                                   OrderItemService orderItemService,
                                   MainWindowVM mainVM,
                                   EventAggregator eventAggregator)
            : this()
        {
            var vm = new CustomerOrderVM(eventAggregator, orderService, orderItemService, mainVM);
            DataContext = vm;
        }

        public CustomerOrderWindow(OrderVM currentOrder,
                                   OrderService orderService,
                                   CustomerService customerService,
                                   OrderItemService orderItemService,
                                   MainWindowVM mainVM,
                                   EventAggregator eventAggregator)
            : this()
        {
            var order = new Order()
            {
                ID = currentOrder.ID,
                CustomerID = currentOrder.CustomerID,
                OrderDate = currentOrder.OrderDate,
                OrderStatusID = currentOrder.OrderStatusID
            };
            var vm = new CustomerOrderVM(eventAggregator, order, orderService, orderItemService, mainVM);
            DataContext = vm;
        }

        private void SaveOrderButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitOrderButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
