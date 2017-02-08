using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class Meta
    {
        public Meta()
        {
            code = -1;
        }
        [DataMember]
        public int code { get; set; }

        [DataMember]
        public string error_message { get; set; }
    }
}