using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DemoVideoBurstApi.Models
{
    [DataContract]
    public class Templates
    {   
        [DataMember]
        public Guid guid { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]      
        public string link { get; set; }
        [DataMember]      
        public string previewimage { get; set; }
        [DataMember]
        public string previewimagethumb { get; set; }    

    }
}