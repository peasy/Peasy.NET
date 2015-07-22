using Facile.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
//using Newtonsoft.Json;

namespace Orders.com.Core
{
    public abstract class DomainBase : IDomainObject<long> 
    {
        // TODO: Include Newtonsoft.json??
        //[JsonIgnore, IgnoreDataMember]
        [IgnoreDataMember]
        public abstract long ID { get; set; }

        public string Self { get; set; }

        [Editable(false)]
        public int? CreatedBy { get; set; }

        [Editable(false)]
        public DateTime CreatedDatetime { get; set; }

        [Editable(false)]
        public int? LastModifiedBy { get; set; }

        [Editable(false)]
        public DateTime? LastModifiedDatetime { get; set; }
    }
}
