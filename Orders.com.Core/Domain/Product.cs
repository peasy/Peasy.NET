using Facile;
using Facile.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class Product : DomainBase
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [FacileForeignKey, FacileRequired, Display(Name="Category")]
        public int CategoryID { get; set; }

        public override int ID
        {
            get { return ProductID; }
            set { ProductID = value; }
        }
    }
}
