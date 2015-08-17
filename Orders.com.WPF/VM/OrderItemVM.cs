using Facile.Core;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.WPF.VM
{
    public class OrderItemVM : OrdersDotComVMBase<OrderItem>
    {
        private long _currentCategoryID;
        private ProductVM _currentProduct;
        private MainWindowVM _mainVM;

        public OrderItemVM(OrderItemService service, MainWindowVM mainVM)
            : base(service)
        {
            _mainVM = mainVM;
        }

        public OrderItemVM(OrderItem customer, OrderItemService service, MainWindowVM mainVM)
            : base(customer, service)
        {
            _mainVM = mainVM;
            CurrentProductID = CurrentEntity.ProductID;
            CurrentCategoryID = _currentProduct.CurrentCategoryID;
            IsDirty = false;
        }

        public long ID
        {
            get { return CurrentEntity.ID; }
        }

        public long CurrentCategoryID
        {
            get { return _currentCategoryID; }
            set
            {
                _currentCategoryID = value;
                OnPropertyChanged("CurrentCategoryID");
                OnPropertyChanged("Products");
            }
        }

        public long CurrentProductID
        {
            get { return CurrentEntity.ProductID; }
            set
            {
                _currentProduct = Products.First(p => p.ID == value);
                CurrentEntity.ProductID = value;
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

        public IEnumerable<CategoryVM> Categories
        {
            get { return _mainVM.CategoriesVM.Categories; }
        }

        public IEnumerable<ProductVM> Products
        {
            get
            {
                if (CurrentCategoryID > 0) return _mainVM.ProductsVM.Products.Where(p => p.CurrentCategoryID == CurrentCategoryID).ToArray();

                return _mainVM.ProductsVM.Products;
            }
        }

        public decimal? Amount
        {
            get
            {
                if (_currentProduct != null)
                    return _currentProduct.Price.Value * Quantity;

                return 0;
            }
        }

        public decimal Quantity
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

        public OrderStateBase Status
        {
            get
            {
                if (IsNew) return null;
                return CurrentEntity.OrderStatus();
            }
        }

        public long StatusID
        {
            get { return CurrentEntity.OrderStatusID; }
        }

        public DateTime? SubmittedOn
        {
            get { return CurrentEntity.SubmittedDate; }
        }

        protected override void OnInsertSuccess(ExecutionResult<OrderItem> result)
        {
            OnPropertyChanged("ID");
            OnPropertyChanged("Status");
        }

        public async Task SubmitAsync()
        {
            if (CanSubmit())
            {
                var service = _service as OrderItemService;
                var result = await service.SubmitCommand(ID).ExecuteAsync();
                CurrentEntity = result.Value;
                OnPropertyChanged("Status");
                OnPropertyChanged("SubmittedOn");
            }
        }

        public bool CanSubmit()
        {
            return CurrentEntity.OrderStatus().CanSubmit;
        }
    }
}
