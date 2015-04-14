using Facile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class Product : IDomainObject
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public int ID
        {
            get { return ProductID; }
            set { ProductID = value; }
        }
    }
}
