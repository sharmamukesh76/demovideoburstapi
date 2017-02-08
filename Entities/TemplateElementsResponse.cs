using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoVideoBurstApi.Entities
{
    public class TemplateElementsResponse
    {
        public TemplateElementsResponse()
        {
            meta = new Meta();
        }
        [DataMember]
        public Meta meta { get; set; }

        [DataMember]
        public multipletemplateelementsresponse data { get; set; }
    }
    [DataContract]
    public class multipletemplateelementsresponse
    {
        [DataMember]
        public List<TemplateElements> TemplateElements { get; set; }
    }

}