using Orders.com.BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Facile.Core.Extensions;

namespace Orders.com.WPF
{
    public class ProductsVM : ViewModelBase
    {
        private ProductService _productsService;
        private ObservableCollection<ProductVM> _products;
        private ICommand _addProductCommand;
        private ICommand _saveProductsCommand;
        private ICommand _loadProductsCommand;
        private ICommand _deleteSelectedCommand;

        public ProductsVM(ProductService customersService)
        {
            _productsService = customersService;
            _addProductCommand = new Command(() => AddProduct());
            _saveProductsCommand = new Command(() => SaveProducts());
            _loadProductsCommand = new Command(() => LoadProducts());
            _deleteSelectedCommand = new Command(() => DeleteSelectedVM());
        }

        public ProductVM SelectedProduct
        {
            get;
            set;
        }

        public ICommand AddProductCommand
        {
            get { return _addProductCommand; }
        }
        
        public ICommand SaveProductsCommand
        {
            get { return _saveProductsCommand; }
        }

        public ICommand LoadProductsCommand
        {
            get { return _loadProductsCommand; }
        }

        public ICommand DeleteSelectedCommand
        {
            get { return _deleteSelectedCommand; }
        }
        
        public IEnumerable<ProductVM> Products
        {
            get { return _products; }
            set
            {
                _products = new ObservableCollection<ProductVM>(value);
                OnPropertyChanged("Products");
            }
        }

        private async Task LoadProducts()
        {
            var result = await _productsService.GetAllCommand().ExecuteAsync();
            var vms = result.Value.Select(c => new ProductVM(c, _productsService));
            Products = vms.ToArray();
        }

        private async Task SaveProducts()
        {
            var results = Products.Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }

        private void AddProduct()
        {
            _products.Add(new ProductVM(_productsService));
        }

        private async Task DeleteSelectedVM()
        {
            if (SelectedProduct != null && !SelectedProduct.IsNew)
            {
                await _productsService.DeleteCommand(SelectedProduct.ID).ExecuteAsync();
                _products.Remove(SelectedProduct);
                SelectedProduct = null;
            }
        }
    }
}
