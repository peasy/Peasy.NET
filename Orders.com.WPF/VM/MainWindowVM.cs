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

namespace Orders.com.WPF.VM
{
    public class MainWindowVM : ViewModelBase
    {
        private CustomersVM _customersVM;
        private ProductsVM _productsVM;
        private CategoriesVM _categoriesVM;
        private OrdersVM _ordersVM;
        private CategoryService _categoryService;
        private ProductService _productService;
        private CustomerService _customerService;

        public MainWindowVM(EventAggregator eventAggregator,
                            CustomerService customerService, 
                            ProductService productService, 
                            CategoryService categoryService,
                            OrderService orderService)
        {
            _customerService = customerService;
            _categoryService = categoryService;
            _productService = productService;
            _customersVM = new CustomersVM(customerService);
            _customersVM.LoadCustomersCommand.Execute(null);
            _productsVM = new ProductsVM(productService, this);
            _productsVM.LoadProductsCommand.Execute(null);
            _categoriesVM = new CategoriesVM(categoryService);
            _categoriesVM.LoadCategoriesCommand.Execute(null);
            _ordersVM = new OrdersVM(orderService, this, eventAggregator);
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
