using Facile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.Core.Domain
{
    public class Customer : IDomainObject<int>
    {
        public int CustomerID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int ID
        {
            set { CustomerID = value; }
            get { return CustomerID; }
        }
    }
}
