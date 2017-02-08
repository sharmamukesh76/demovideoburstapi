using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DemoVideoBurstApi.Entities
{
    public class RenderTemplateElements
    {
        [IgnoreDataMember]
        public ElementTypes elementtype { get; set; }
        [DataMember]
        public string typetext { get; set; }
        [DataMember]
        public string starttime { get; set; }
        [DataMember]
        public string stoptime { get; set; }
        [DataMember]
        public int? width { get; set; }
        [DataMember]
        public int? height { get; set; }
        [DataMember]
        public int? minlength { get; set; }
        [DataMember]
        public int? maxlength { get; set; }
        [DataMember]
        public int group { get; set; }
    }
}