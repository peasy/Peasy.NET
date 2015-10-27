using Orders.com.BLL;
using Orders.com.Core.Domain;
using System.Collections.Generic;

namespace Orders.com.WPF.VM
{
    public class ProductVM : OrdersDotComVMBase<Product>
    {
        private MainWindowVM _mainVM;

        public ProductVM(ProductService service, MainWindowVM mainVM) : base(service)
        {
            _mainVM = mainVM;
        }

        public ProductVM(Product product, ProductService service, MainWindowVM mainVM) : base(product, service)
        {
            _mainVM = mainVM;
        }

        public IEnumerable<CategoryVM> Categories
        {
            get { return _mainVM.CategoriesVM.Categories; }
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

        protected override void OnInsertSuccess(Product result)
        {
            OnPropertyChanged("ID");
        }
    }
}
