using Facile.Core;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF
{
    public class OrderItemVM : EntityViewModelBase<OrderItem, int>
    {
        private ProductService _productService;
        private IEnumerable<Product> _products;

        public OrderItemVM (OrderItemService service, ProductService productService) : base(service)
        {
            _productService = productService;
        }

        public OrderItemVM(OrderItem customer, OrderItemService service, ProductService productService) : base(customer, service)
        {
            _productService = productService;
        }

        public int ID
        {
            get { return CurrentEntity.ID; }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                if (_products == null)
                    LoadProducts();
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
        }

        //public string Name
        //{
        //    get { return CurrentEntity.Name; }
        //    set
        //    {
        //        CurrentEntity.Name = value;
        //        IsDirty = true;
        //        OnPropertyChanged("Name");
        //    }
        //}

        protected override void OnCommandExecutionSuccess(ExecutionResult<OrderItem> result)
        {
            OnPropertyChanged("ID");
            //Name = CurrentEntity.Name;
        }
    }
}
