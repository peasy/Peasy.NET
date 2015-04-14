using Facile;
using Orders.com.BLL;
using Orders.com.Core.Domain;

namespace Orders.com.WPF
{
    public class CustomerVM : EntityViewModelBase<Customer>
    {
        public CustomerVM (CustomerService service) : base(service)
        {
        }

        public CustomerVM(Customer customer, CustomerService service) : base(customer, service)
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

        protected override void OnCommandExecutionSuccess(ExecutionResult<Customer> result)
        {
            OnPropertyChanged("ID");
            Name = CurrentEntity.Name;
        }
    }
}
