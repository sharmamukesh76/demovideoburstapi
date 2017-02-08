using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DemoVideoBurstApi.Entities
{
    public class JobElementsResponse
    {
        public JobElementsResponse()
        {
            meta = new Meta();
        }
        [DataMember]
        public Meta meta { get; set; }

        [DataMember]
        public multiplejobelementsresponse data { get; set; }
    }

    [DataContract]
    public class multiplejobelementsresponse
    {
        [DataMember]
        public List<JobElements> JobElements { get; set; }
    }
}