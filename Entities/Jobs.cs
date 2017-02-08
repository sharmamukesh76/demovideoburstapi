using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class Jobs
    {
        [DataMember]
        public Guid guid { get; set; }

        [IgnoreDataMember]
        public Guid templateguid { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public DateTime updateddate { get; set; }

        [DataMember]
        public string status { get; set; }  // Status should be completed in all cases

        [DataMember]
        public List<MediaFiles> media { get; set; }
      
    }
}