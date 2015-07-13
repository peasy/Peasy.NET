using Facile.Core;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF.VM
{
    public class OrderItemVM : OrdersDotComVMBase<OrderItem>
    {
        private CategoryService _categoryService;
        private ProductService _productService;
        private IEnumerable<Category> _categories;
        private IEnumerable<Product> _products;
        private long _currentCategoryID;
        private Product _currentProduct;
        //private System.Windows.Input.ICommand _saveCustomersCommand;

        public OrderItemVM(OrderItemService service, CategoryService categoryService, ProductService productService)
            : base(service)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public OrderItemVM(OrderItem customer, CategoryService categoryService, OrderItemService service, ProductService productService)
            : base(customer, service)
        {
            _productService = productService;
            _categoryService = categoryService;
            OnPropertyChanged("Price");
        }

        public long ID
        {
            get { return CurrentEntity.ID; }
        }

        public long CurrentCategoryID
        {
            get
            {
                return _currentCategoryID;
            }
            set
            {
                _currentCategoryID = value;
                OnPropertyChanged("CurrentCategoryID");
            }
        }

        public long CurrentProductID
        {
            get { return CurrentEntity.ProductID; }
            set
            {
                _currentProduct = Products.First(p => p.ProductID == value);
                CurrentEntity.ProductID = value;
                CurrentCategoryID = _currentProduct.CategoryID;
                IsDirty = true;
                OnPropertyChanged("CurrentProductID");
                OnPropertyChanged("Price");
                OnPropertyChanged("Amount");
            }
        }

        public decimal Price
        {
            get
            {
                if (_currentProduct != null)
                    return _currentProduct.Price.Value;

                return 0;
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                if (_categories == null)
                    LoadCategories();
                return _categories;
            }
            private set
            {
                _categories = value;
                OnPropertyChanged("Categories");
                //CurrentCategoryID = _categories.First(c => c.CategoryID = CurrentEntity.pr)
            }
        }

        private async Task LoadCategories()
        {
            var result = await _categoryService.GetAllCommand().ExecuteAsync();
            Categories = result.Value;
            OnPropertyChanged("CurrentCategoryID");
        }

        public IEnumerable<Product> Products
        {
            get
            {
                if (_products == null)
                    LoadProducts();

                if (CurrentCategoryID > 0) return _products.Where(p => p.CategoryID == CurrentCategoryID).ToArray();

                return _products;
            }
            private set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        private async Task LoadProducts()
        {
            var result = await _productService.GetAllCommand().ExecuteAsync();
            Products = result.Value;
            CurrentProductID = CurrentEntity.ProductID;
        }

        public decimal? Amount
        {
            get
            {
                if (_currentProduct != null)
                    return _currentProduct.Price.Value * Quantity.GetValueOrDefault();

                return 0;
            }
        }

        public decimal? Quantity
        {
            get { return CurrentEntity.Quantity; }
            set
            {
                CurrentEntity.Quantity = value;
                IsDirty = true;
                OnPropertyChanged("Quantity");
                OnPropertyChanged("Amount");
            }
        }

        public long OrderID
        {
            get { return CurrentEntity.OrderID; }
            set { CurrentEntity.OrderID = value; }
        }

        protected override void OnInsertSuccess(ExecutionResult<OrderItem> result)
        {
            OnPropertyChanged("ID");
        }

    }
}
