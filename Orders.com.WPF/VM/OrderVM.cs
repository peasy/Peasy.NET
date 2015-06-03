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
    public class OrderVM : EntityViewModelBase<Order, int>
    {
        public OrderVM (OrderService service) : base(service)
        {
        }

        public OrderVM(Order customer, OrderService service) : base(customer, service)
        {
        }

        public int ID
        {
            get { return CurrentEntity.ID; }
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

        protected override void OnCommandExecutionSuccess(ExecutionResult<Order> result)
        {
            OnPropertyChanged("ID");
            //Name = CurrentEntity.Name;
        }
    }
}
