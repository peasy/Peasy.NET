using Facile;
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
    public class ProductVM : OrdersDotComVMBase<Product>
    {
        private CategoryService _categoriesService;
        private IEnumerable<Category> _categories;

        public ProductVM(ProductService service, CategoryService categoriesService) : base(service)
        {
            _categoriesService = categoriesService;
        }

        public ProductVM(Product product, ProductService service, CategoryService categoriesService) : base(product, service)
        {
            _categoriesService = categoriesService;
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                if (_categories == null)
                    LoadCategories();

                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public async void LoadCategories()
        {
            var categories = await _categoriesService.GetAllCommand().ExecuteAsync();
            Categories = categories.Value;
        }

        public long ID
        {
            get { return CurrentEntity.ID; }
        }

        public string Name
        {
            get { return CurrentEntity.Name; }
            set
            {
                CurrentEntity.Name = value;
                IsDirty = true;
                OnPropertyChanged("Name");
            }
        }

        public decimal? Price
        {
            get { return CurrentEntity.Price; }
            set
            {
                CurrentEntity.Price = value;
                IsDirty = true;
                OnPropertyChanged("Price");
            }
        }

        public string Description
        {
            get { return CurrentEntity.Description; }
            set
            {
                CurrentEntity.Description = value;
                IsDirty = true;
                OnPropertyChanged("Description");
            }
        }

        public long CurrentCategoryID
        {
            get { return CurrentEntity.CategoryID; }
            set
            {
                CurrentEntity.CategoryID = value;
                IsDirty = true;
                OnPropertyChanged("CurrentCategoryID");
            }
        }

        protected override void OnCommandExecutionSuccess(ExecutionResult<Product> result)
        {
            OnPropertyChanged("ID");
            Name = CurrentEntity.Name;
        }
    }
}
