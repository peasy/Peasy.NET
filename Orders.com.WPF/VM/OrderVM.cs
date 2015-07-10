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
        private long _orderID;
        private DateTime _orderDate;
        private string _name;
        private decimal _total;
        private CustomerOrderVM customerOrderVM;

        public OrderVM(OrderInfo order) 
        {
            _orderID = order.OrderID;
            _orderDate = order.OrderDate;
            _name = order.CustomerName;
            _total = order.Total;
        }

        public OrderVM(CustomerOrderVM order, CustomerService customerService)
        {
            _orderID = order.ID;
            _orderDate = order.CurrentEntity.OrderDate;
            _name = customerService.GetAllCommand().Execute().Value.First(c => c.CustomerID == order.CurrentCustomerID).Name;
            _total = order.OrderItems.Sum(i => i.Amount.Value);
        }

        public long ID
        {
            get { return _orderID; }
            set
            {
                _orderID = value;
                OnPropertyChanged("ID");
            }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                _orderDate = value;
                OnPropertyChanged("OrderDate");
            }
        }

        public string Customer
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Customer");
            }
        }

        public decimal Total 
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }
    }
}
