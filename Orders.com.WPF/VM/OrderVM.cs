using Facile.Core;
using Orders.com.BLL;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.Core.QueryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF.VM
{
    public class OrderVM : ViewModelBase, IOrderStatusIDContainer
    {
        private string _customerName;
        private decimal _total;
        private string _status;
        private DateTime? _submittedOn;
        private System.Windows.Input.ICommand _submitCommand;
        private OrderService _orderService;

        public OrderVM(OrderInfo order, OrderService orderService)
        {
            ID = order.OrderID;
            OrderDate = order.OrderDate;
            Customer = order.CustomerName;
            CustomerID = order.CustomerID;
            Total = order.Total;
            Status = order.Status;
            OrderStatusID = order.StatusID;
            SubmittedOn = order.SubmittedOn;
            _submitCommand = new Command(() => SubmitAsync(), () => this.OrderStatus().CanSubmit);
            _orderService = orderService;
        }

        public OrderVM(CustomerOrderVM order, MainWindowVM vm, OrderService orderService)
        {
            ID = order.ID;
            OrderDate = order.CurrentEntity.OrderDate;
            Customer = vm.CustomersVM.Customers.First(c => c.ID == order.CurrentCustomerID).Name;
            CustomerID = order.CurrentCustomerID;
            Total = order.OrderItems.Sum(i => i.Amount.Value);
            Status = order.Status.Name;
            OrderStatusID = order.StatusID;
            SubmittedOn = order.SubmittedOn;
            _submitCommand = new Command(() => SubmitAsync(), () => this.OrderStatus().CanSubmit);
            _orderService = orderService;
        }

        public System.Windows.Input.ICommand SubmitCommand
        {
            get { return _submitCommand; }
        }

        private async Task SubmitAsync()
        {
            var result = await _orderService.SubmitCommand(ID).ExecuteAsync();
            Status = result.Value.OrderStatus().Name;
            OrderStatusID = result.Value.OrderStatusID;
            SubmittedOn = result.Value.SubmittedDate;
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
        
        public long OrderStatusID { get; set; }

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
