using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
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
