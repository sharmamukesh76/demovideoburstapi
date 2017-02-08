using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class MediaFiles
    {
        [IgnoreDataMember]
        public string profileid { get; set; }

        [DataMember]
        public string description { get; set; }  // Description on the basis of Profile Id

        [IgnoreDataMember]
        public string urlid { get; set; }

        [DataMember]
        public string url { get; set; } // DsignedURL on the basis of UrlId

        [DataMember]
        public int width { get; set; }

        [DataMember]
        public int height { get; set; }

        [DataMember]
        public int mediatype { get; set; }
    }
}