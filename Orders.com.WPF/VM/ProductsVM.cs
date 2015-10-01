using Peasy.Core.Extensions;
using Orders.com.BLL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class ProductsVM : ViewModelBase
    {
        private ProductService _productsService;
        private ObservableCollection<ProductVM> _products = new ObservableCollection<ProductVM>();
        private ICommand _addProductCommand;
        private ICommand _saveProductsCommand;
        private ICommand _loadProductsCommand;
        private ICommand _deleteSelectedCommand;
        private MainWindowVM _mainVM;

        public ProductsVM(ProductService productsService, MainWindowVM mainVM)
        {
            _productsService = productsService;
            _mainVM = mainVM;
            _addProductCommand = new Command(() => AddProduct());
            _saveProductsCommand = new Command(async () => await SaveProductsAsync());
            _loadProductsCommand = new Command(async () => await LoadProductsAsync());
            _deleteSelectedCommand = new Command(async () => await DeleteSelectedItemAsync());
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
        }

        private async Task LoadProductsAsync()
        {
            var result = await _productsService.GetAllCommand().ExecuteAsync();
            _products.Clear();
            var vms = result.Value.Select(p => new ProductVM(p, _productsService, _mainVM))
                                  .ForEach(vm =>
                                  {
                                      SubscribeHandlers(vm);
                                      _products.Add(vm);
                                  });
        }

        private async Task SaveProductsAsync()
        {
            var results = Products.Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }

        private void AddProduct()
        {
            _products.Add(new ProductVM(_productsService, _mainVM));
        }

        private void SubscribeHandlers(ProductVM product)
        {
            product.EntityDeleted += (s, e) => _products.Remove(SelectedProduct);
        }

        private async Task DeleteSelectedItemAsync()
        {
            if (SelectedProduct.IsNew)
                _products.Remove(SelectedProduct);
            else
                await SelectedProduct.DeleteAsync();
        }
    }
}
