using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DemoVideoBurstApi.Models;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class TemplateResponse
    {
        public TemplateResponse()
        {
            meta = new Meta();
        }
        [DataMember]
        public Meta meta { get; set; }

        [DataMember]
        public singleTemplateresponse data { get; set; }

        [DataContract]
        public class singleTemplateresponse
        {
            [DataMember]
            public Templates Template { get; set; }
        }



        [DataContract]
        public class TemplatesResponse
        {
            public TemplatesResponse()
            {
                meta = new Meta();
            }
            [DataMember]
            public Meta meta { get; set; }

            [DataMember]
            public multipleTemplateresponse data { get; set; }
        }
        [DataContract]
        public class multipleTemplateresponse
        {
            [DataMember]
            public List<Templates> Templates { get; set; }
        }



    }
}