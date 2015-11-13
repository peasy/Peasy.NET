using System.ComponentModel.DataAnnotations;

namespace Orders.com.Domain
{
    public class Category : DomainBase
    {
        public long CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        public override long ID
        {
            get { return CategoryID; }
            set { CategoryID = value; }
        }
    }
}
