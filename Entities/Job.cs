using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoVideoBurstApi.Entities
{
    [DataContract]
    public class Job
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public Guid guid { get; set; }
        /// <summary>
        /// Guid of the template from which video is created or is to be created
        /// <remarks>Required while creating new video.</remarks>
        /// </summary>
        [DataMember]
        public Guid templateguid { get; set; }
        /// <summary>
        /// Title of the video
        /// <remarks>Required while creating new video.</remarks>
        /// </summary>
        [DataMember]
        public string title { get; set; }
        /// <summary>
        /// Weblink to open in video player when clicked on play.
        /// <remarks>Optional while creating new video.</remarks>
        /// </summary>
        [DataMember]
        public string clickurl { get; set; }     
        [DataMember]
        public string callback { get; set; }
        [DataMember]
        public List<JobElements> elements { get; set; }

    }
}