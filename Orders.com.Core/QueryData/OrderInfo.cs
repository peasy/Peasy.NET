using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.QueryData
{
    public class OrderInfo
    {
        public long OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public long CustomerID { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public bool HasShippedItems { get; set; }
    }
}
