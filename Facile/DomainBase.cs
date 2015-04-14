using System;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Facile
{
    public abstract class DomainBase : IDomainObject
    {
        // TODO: Include Newtonsoft.json??
        //[JsonIgnore, IgnoreDataMember]
        [IgnoreDataMember]
        public abstract int ID { get; set; }

        //[Required] - TODO: require on update only
        public byte[] Version { get; set; }

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
