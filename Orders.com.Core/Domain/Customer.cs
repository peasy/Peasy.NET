using Facile;
using System.ComponentModel.DataAnnotations;
using System;

namespace Orders.com.Core.Domain
{
    public class Customer : DomainBase
    {
        public long CustomerID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public override long ID
        {
            set { CustomerID = value; }
            get { return CustomerID; }
        }
    }
}
