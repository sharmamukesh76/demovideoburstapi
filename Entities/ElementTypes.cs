using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class ElementTypes
    {
        [DataMember]
        public int type { get; set; }
        [IgnoreDataMember]
        public int engineid { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string description { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string tag { get; set; }
        [IgnoreDataMember]
        public string foldername { get; set; }
    }

    public enum ElementTypesTags
    {
        text,
        image,
        audio,
        video,
        color_picker,
        aep
    }
}