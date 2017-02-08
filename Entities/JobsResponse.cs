using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class JobsResponse
    {
        public JobsResponse()
        {
            meta = new Meta();
        }
        [DataMember]
        public Meta meta { get; set; }

        [DataMember]
        public multiplejobresponse data { get; set; }
    }

    [DataContract]
    public class multiplejobresponse
    {
        [DataMember]
        public List<Jobs> jobs { get; set; }
    }

    [DataContract]
    public class JobsGuidResponse
    {
        public JobsGuidResponse()
        {
            meta = new Meta();
        }
        [DataMember]
        public Meta meta { get; set; }

        [DataMember]
        public singlejobguid data { get; set; }
    }

    [DataContract]
    public class singlejobguid
    {
        [DataMember]
        public Guid guid { get; set; }
    }

}