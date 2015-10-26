using Peasy.Core.Extensions;
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
        private ObservableCollection<CustomerVM> _customers = new ObservableCollection<CustomerVM>();
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
            _deleteSelectedCommand = new Command(async () => await DeleteSelectedItemAsync());
        }

        public CustomerVM SelectedCustomer { get; set; }

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
        }

        private async Task LoadCustomersAsync()
        {
            var result = await _customersService.GetAllCommand().ExecuteAsync();
            _customers.Clear();
            var vms = result.Value.Select(c => new CustomerVM(c, _customersService))
                                  .ForEach(vm =>
                                  {
                                      SubscribeHandlers(vm);
                                      _customers.Add(vm);
                                  });
        }

        private async Task SaveCustomersAsync()
        {
            var results = Customers.Select(vm => vm.SaveAsync());
            await Task.WhenAll(results);
        }

        private void AddCustomer()
        {
            var customer = new CustomerVM(_customersService);
            SubscribeHandlers(customer);
            _customers.Add(customer);
        }

        private void SubscribeHandlers(CustomerVM customer)
        {
            customer.EntityDeleted += (s, e) => _customers.Remove(SelectedCustomer);
        }

        private async Task DeleteSelectedItemAsync()
        {
            if (SelectedCustomer.IsNew)
                _customers.Remove(SelectedCustomer);
            else
                await SelectedCustomer.DeleteAsync();
        }
    }
}
