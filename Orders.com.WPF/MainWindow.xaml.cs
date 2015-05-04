using Orders.com.BLL;
using Orders.com.DAL.EF;
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
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var customersService = new CustomerService(new CustomerRepository());
            var productsService = new ProductService(new ProductRepository());
            var categoriesService = new CategoryService(new CategoryRepository());
            this.DataContext = new MainWindowVM(customersService, productsService, categoriesService);
        }

        public MainWindowVM VM
        {
            get { return this.DataContext as MainWindowVM; }
        }
   }
}
