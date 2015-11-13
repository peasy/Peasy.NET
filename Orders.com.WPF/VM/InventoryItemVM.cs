using System.Linq;
using Orders.com.BLL;
using Orders.com.Domain;

namespace Orders.com.WPF.VM
{
    public class InventoryItemVM : OrdersDotComVMBase<InventoryItem>
    {
        private MainWindowVM _mainVM;

        public InventoryItemVM (InventoryItemService service) : base(service)
        {
        }

        public InventoryItemVM(InventoryItem item, InventoryItemService service, MainWindowVM mainVM) : base(item, service)
        {
            _mainVM = mainVM;
        }

        public string Name
        {
            get
            {
                return _mainVM.ProductsVM.Products.First(p => p.ID == CurrentEntity.ProductID).Name;
            }
        }

        public decimal QuantityOnHand
        {
            get { return CurrentEntity.QuantityOnHand; }
            set
            {
                CurrentEntity.QuantityOnHand = value;
                IsDirty = true;
                OnPropertyChanged("QuantityOnHand");
            }
        }
    }
}
