using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class JobElements
    {
        [IgnoreDataMember]
        public long id { get; set; }
        [DataMember(Name = "jobelement")]
        public Guid guid { get; set; }
        [DataMember(Name = "job")]
        public Guid jobguid { get; set; }
        [DataMember]
        public Guid templateelementguid { get; set; }
        [DataMember]
        public string value { get; set; }        
    }
}