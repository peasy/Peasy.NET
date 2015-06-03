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
    public class ProductVM : EntityViewModelBase<Product, int>
    {
        public ProductVM(ProductService service) : base(service)
        {
        }

        public ProductVM(Product product, ProductService service) : base(product, service)
        {
        }

        public int ID
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

        protected override void OnCommandExecutionSuccess(ExecutionResult<Product> result)
        {
            OnPropertyChanged("ID");
            Name = CurrentEntity.Name;
        }
    }
}
