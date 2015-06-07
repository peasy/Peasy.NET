using Facile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.Core.Domain
{
    public class Customer : DomainBase, IVersionContainer
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

        //[Required] - TODO: require on update only
        public byte[] Version { get; set; }
    }
}
