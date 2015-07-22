using Facile.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.Domain
{
    public class InventoryItem : DomainBase
    {
        public long InventoryItemID { get; set; }

        [FacileRequired]
        public decimal QuantityOnHand { get; set; }

        public long ProductID { get; set; }

        public override long ID
        {
            get { return InventoryItemID; }
            set { InventoryItemID = value; }
        }
    }
}
