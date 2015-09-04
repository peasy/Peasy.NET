using Orders.com.BLL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class InventoryItemsVM : ViewModelBase
    {
        private InventoryItemService _inventoryService;
        private ObservableCollection<InventoryItemVM> _inventoryItems;
        private ICommand _saveInventoryItemsCommand;
        private ICommand _loadInventoryItemsCommand;
        private MainWindowVM _mainVM;

        public InventoryItemsVM(InventoryItemService inventoryService, MainWindowVM mainVM)
        {
            _inventoryService = inventoryService;
            _mainVM = mainVM;
            _saveInventoryItemsCommand = new Command(async () => await SaveInventoryItemsAsync());
            _loadInventoryItemsCommand = new Command(async () => await LoadInventoryItemsAsync());
        }

        public InventoryItemVM SelectedInventoryItem
        {
            get;
            set;
        }

        public ICommand SaveInventoryCommand
        {
            get { return _saveInventoryItemsCommand; }
        }

        public ICommand LoadInventoryCommand
        {
            get { return _loadInventoryItemsCommand; }
        }

        public IEnumerable<InventoryItemVM> InventoryItems
        {
            get { return _inventoryItems; }
            set
            {
                _inventoryItems = new ObservableCollection<InventoryItemVM>(value);
                OnPropertyChanged("InventoryItems");
            }
        }

        private async Task LoadInventoryItemsAsync()
        {
            var result = await _inventoryService.GetAllCommand().ExecuteAsync();
            var vms = result.Value.Select(i => new InventoryItemVM(i, _inventoryService, _mainVM));
            InventoryItems = vms.ToArray();
        }

        private async Task SaveInventoryItemsAsync()
        {
            var results = InventoryItems.Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }
    }
}
