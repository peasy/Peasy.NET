using Facile.Core;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using Orders.com.Core.QueryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF.VM
{
    public class OrderVM : ViewModelBase 
    {
        private string _customerName;
        private decimal _total;
        private string _status;
        private DateTime? _submittedOn;

        public OrderVM(OrderInfo order) 
        {
            ID = order.OrderID;
            OrderDate = order.OrderDate;
            Customer = order.CustomerName;
            CustomerID = order.CustomerID;
            Total = order.Total;
            Status = order.Status;
            StatusID = order.StatusID;
            SubmittedOn = order.SubmittedOn;
        }

        public OrderVM(CustomerOrderVM order, MainWindowVM vm)
        {
            ID = order.ID;
            OrderDate = order.CurrentEntity.OrderDate;
            Customer = vm.CustomersVM.Customers.First(c => c.ID == order.CurrentCustomerID).Name;
            CustomerID = order.CurrentCustomerID;
            Total = order.OrderItems.Sum(i => i.Amount.Value);
            Status = order.Status.Name;
            StatusID = order.StatusID;
            SubmittedOn = order.SubmittedOn;
        }

        public long ID { get; set; }

        public DateTime OrderDate { get; set; }

        public string Customer
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                OnPropertyChanged("Customer");
            }
        }

        public long CustomerID { get; set; }

        public decimal Total 
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        
        public long StatusID { get; set; }

        public DateTime? SubmittedOn
        {
            get { return _submittedOn; }
            set
            {
                _submittedOn = value;
                OnPropertyChanged("SubmittedOn");
            }
        }
    }
}
