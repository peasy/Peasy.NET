using Orders.com.BLL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class CustomersVM : ViewModelBase
    {
        private CustomerService _customersService;
        private ObservableCollection<CustomerVM> _customers;
        private ICommand _addCustomerCommand;
        private ICommand _saveCustomersCommand;
        private ICommand _loadCustomersCommand;
        private ICommand _deleteSelectedCommand;

        public CustomersVM(CustomerService customersService)
        {
            _customersService = customersService;
            _addCustomerCommand = new Command(() => AddCustomer());
            _saveCustomersCommand = new Command(async () => await SaveCustomersAsync());
            _loadCustomersCommand = new Command(async () => await LoadCustomersAsync());
            _deleteSelectedCommand = new Command(async () => await DeleteSelectedVMAsync());
        }

        public CustomerVM SelectedCustomer
        {
            get;
            set;
        }

        public ICommand AddCustomerCommand
        {
            get { return _addCustomerCommand; }
        }
        
        public ICommand SaveCustomersCommand
        {
            get { return _saveCustomersCommand; }
        }

        public ICommand LoadCustomersCommand
        {
            get { return _loadCustomersCommand; }
        }

        public ICommand DeleteSelectedCommand
        {
            get { return _deleteSelectedCommand; }
        }
        
        public IEnumerable<CustomerVM> Customers
        {
            get { return _customers; }
            set
            {
                _customers = new ObservableCollection<CustomerVM>(value);
                OnPropertyChanged("Customers");
            }
        }

        private async Task LoadCustomersAsync()
        {
            var result = await _customersService.GetAllCommand().ExecuteAsync();
            var vms = result.Value.Select(c => new CustomerVM(c, _customersService));
            Customers = vms.ToArray();
        }

        private async Task SaveCustomersAsync()
        {
            var results = Customers.Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }

        private void AddCustomer()
        {
            _customers.Add(new CustomerVM(_customersService));
        }

        private async Task DeleteSelectedVMAsync()
        {
            if (SelectedCustomer != null && !SelectedCustomer.IsNew)
            {
                await _customersService.DeleteCommand(SelectedCustomer.ID).ExecuteAsync();
                _customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }
    }
}
