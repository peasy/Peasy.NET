using Facile;
using Facile.Core;
using Orders.com.BLL;
using Orders.com.Core.Domain;

namespace Orders.com.WPF
{
    public class CategoryVM : EntityViewModelBase<Category, int>
    {
        public CategoryVM (CategoryService service) : base(service)
        {
        }

        public CategoryVM(Category customer, CategoryService service) : base(customer, service)
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

        protected override void OnCommandExecutionSuccess(ExecutionResult<Category> result)
        {
            OnPropertyChanged("ID");
            //Name = CurrentEntity.Name;
        }
    }
}
