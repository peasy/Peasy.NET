using Facile;
using Facile.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.Core.Domain
{
    public class Product : DomainBase
    {
        public long ProductID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [FacileRequired]
        public decimal QuantityOnHand { get; set; }

        [FacileForeignKey, FacileRequired, Display(Name="Category")]
        public long CategoryID { get; set; }

        public override long ID
        {
            get { return ProductID; }
            set { ProductID = value; }
        }
    }
}
