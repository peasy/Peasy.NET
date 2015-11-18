using MahApps.Metro.Controls.Dialogs;
using Orders.com.BLL;
using Orders.com.Domain;
using Orders.com.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class OrderItemVM : OrdersDotComVMBase<OrderItem>, IOrderStatusIDContainer
    {
        private long _currentCategoryID;
        private ProductVM _currentProduct;
        private MainWindowVM _mainVM;
        private ICommand _shipCommand;
        private InventoryItemService _inventoryService;

        public OrderItemVM(OrderItemService service, InventoryItemService inventoryService, MainWindowVM mainVM)
            : base(service)
        {
            _mainVM = mainVM;
            _shipCommand = new Command(async () => await ShipAsync());
            _inventoryService = inventoryService;
        }

        public OrderItemVM(OrderItem customer, OrderItemService service, InventoryItemService inventoryService, MainWindowVM mainVM)
            : base(customer, service)
        {
            _mainVM = mainVM;
            _shipCommand = new Command(async () => await ShipAsync());
            _inventoryService = inventoryService;
            CurrentProductID = CurrentEntity.ProductID;
            CurrentCategoryID = _currentProduct.CurrentCategoryID;
            IsDirty = false;
        }

        public System.Windows.Input.ICommand ShipCommand
        {
            get { return _shipCommand; }
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
                OnPropertyChanged("CurrentCategoryID", "Products");
            }
        }

        public long CurrentProductID
        {
            get { return CurrentEntity.ProductID; }
            set
            {
                _currentProduct = Products.First(p => p.ID == value);
                CurrentEntity.ProductID = value;
                LoadInventoryItemAsync();
                if (IsNew) 
                    CurrentEntity.Price = _currentProduct.Price.Value;
                CurrentEntity.SetAmount();
                IsDirty = true;
                OnPropertyChanged("CurrentProductID", "Price", "Amount");
            }
        }

        private InventoryItem _currentInventoryItem; 

        private async Task LoadInventoryItemAsync()
        {
            var result = await _inventoryService.GetByProductCommand(CurrentProductID).ExecuteAsync();
            _currentInventoryItem = result.Value;
            OnPropertyChanged("QuantityOnHand");
        }

        public bool CanChangeCategoryAndProduct
        {
            get { return IsNew; }
        }

        public decimal Price
        {
            get
            {
                if (_currentProduct != null)
                    return IsNew? _currentProduct.Price.Value : CurrentEntity.Price;

                return 0;
            }
        }

        public IEnumerable<CategoryVM> Categories
        {
            get { return _mainVM.CategoriesVM.Categories.OrderBy(c => c.Name); }
        }

        public IEnumerable<ProductVM> Products
        {
            get
            {
                var products = _mainVM.ProductsVM.Products;
                if (CurrentCategoryID > 0)
                    products = products.Where(p => p.CurrentCategoryID == CurrentCategoryID).ToArray();

                return products.OrderBy(p => p.Name);
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
                CurrentEntity.SetAmount();
                IsDirty = true;
                OnPropertyChanged("Quantity", "Amount");
            }
        }

        public long OrderID
        {
            get { return CurrentEntity.OrderID; }
            set { CurrentEntity.OrderID = value; }
        }

        public decimal? QuantityOnHand
        {
            get
            {
                if (_currentInventoryItem != null)
                    return _currentInventoryItem.QuantityOnHand;

                return null; 
            }
        }

        public OrderStateBase Status
        {
            get { return CurrentEntity.OrderStatus(); }
        }

        public long StatusID
        {
            get { return CurrentEntity.OrderStatusID; }
        }

        public DateTime? SubmittedOn
        {
            get { return CurrentEntity.SubmittedDate; }
        }

        public DateTime? ShippedOn
        {
            get { return CurrentEntity.ShippedDate; }
        }

        protected override void OnInsertSuccess(OrderItem result)
        {
            OnPropertyChanged("ID", "Status", "CanChangeCategoryAndProduct");
        }

        public bool CanSubmit
        {
            get { return CurrentEntity.OrderStatus().CanSubmit; }
        }

        public async Task SubmitAsync()
        {
            if (CanSubmit)
            {
                var service = _service as OrderItemService;
                var result = await service.SubmitCommand(ID).ExecuteAsync();
                if (result.Success)
                {
                    CurrentEntity = result.Value;
                    OnPropertyChanged("Status", "SubmittedOn");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(result.Errors);
                }
            }
        }

        public bool CanShip
        {
            get { return CurrentEntity.OrderStatus().CanShip; }
        }

        public long OrderStatusID
        {
            get { return StatusID; }
            set { }
        }

        public async Task ShipAsync()
        {
            if (CanShip)
            {
                var service = _service as OrderItemService;
                var result = await service.ShipCommand(ID).ExecuteAsync();
                if (result.Success)
                {
                    await LoadInventoryItemAsync();
                    CurrentEntity = result.Value;
                    OnPropertyChanged("Status", "ShippedOn");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(result.Errors);
                }
            }
        }
    }
}
