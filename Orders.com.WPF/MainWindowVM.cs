using Orders.com.BLL;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facile.Extensions;
using System.Windows.Input;

namespace Orders.com.WPF
{
    public class MainWindowVM : ViewModelBase
    {
        private CustomerService _customersService;
        private CustomersVM _customersVM;
        private ProductsVM _productsVM;

        public MainWindowVM(CustomerService customersService, ProductService productService)
        {
            _customersService = customersService;
            _customersVM = new CustomersVM(_customersService);
            _customersVM.LoadCustomersCommand.Execute(null);
            _productsVM = new ProductsVM(productService);
            _productsVM.LoadProductsCommand.Execute(null);
        }

        public CustomersVM CustomersVM
        {
            get { return _customersVM; }
        }

       public ProductsVM ProductsVM
        {
            get { return _productsVM; }
        }

    }
}
