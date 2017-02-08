using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class TemplateElements
    {
        [DataMember]
        public Guid guid { get; set; }
       
        public Guid templateguid { get; set; }         
        [DataMember]
        public RenderTemplateElements rendertemplateelement { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string helptext { get; set; }
        [DataMember]
        public int mode { get; set; }
        [DataMember]
        public string defaultvalue { get; set; }
        [DataMember]
        public string groupname { get; set; }
       
        public bool active { get; set; }
        [DataMember]
        public int sortorder { get; set; }   
        [DataMember]
        public string DefaultThumbnail { get; set; }       
    }
}