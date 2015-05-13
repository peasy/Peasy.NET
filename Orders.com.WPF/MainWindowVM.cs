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
        private CustomersVM _customersVM;
        private ProductsVM _productsVM;
        private CategoriesVM _categoriesVM;
        private OrdersVM _ordersVM;

        public MainWindowVM(CustomerService customerService, 
                            ProductService productService, 
                            CategoryService categoryService,
                            OrderService orderService)
        {
            _customersVM = new CustomersVM(customerService);
            _customersVM.LoadCustomersCommand.Execute(null);
            _productsVM = new ProductsVM(productService);
            _productsVM.LoadProductsCommand.Execute(null);
            _categoriesVM = new CategoriesVM(categoryService);
            _categoriesVM.LoadCategoriesCommand.Execute(null);
            _ordersVM = new OrdersVM(orderService);
            _ordersVM.LoadOrdersCommand.Execute(null);
        }

        public CustomersVM CustomersVM
        {
            get { return _customersVM; }
        }

        public ProductsVM ProductsVM
        {
            get { return _productsVM; }
        }

        public CategoriesVM CategoriesVM
        {
            get { return _categoriesVM; }
        }

        public OrdersVM OrdersVM
        {
            get { return _ordersVM; }
        }

    }
}
