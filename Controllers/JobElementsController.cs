using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoVideoBurstApi.BL;
using DemoVideoBurstApi.Entities;

namespace DemoVideoBurstApi.Controllers
{
    public class JobElementsController : ApiController
    {
        JobElementsBL oJobsBL = new JobElementsBL();

        /// <summary>
        /// Get the Job elements list for a particular Job.
        /// </summary>
        /// <param name="guid">The Job guid</param>
        /// <returns>Job Elements list which are not fixed for specified Job Guid</returns>      
        [HttpGet]
        public JobElementsResponse GetJobElementsByJobGuid(Guid guid)
        {
            JobElementsResponse oJobElementsResponse = new JobElementsResponse();
            {
                JobElementsBL oTemplateElementsBL = new JobElementsBL();
                List<JobElements> liJobElements = new List<JobElements>();
                ServerResponse.ResponseCodes eResponse = oTemplateElementsBL.GetJobElementsByJobGuid(out liJobElements, guid);
                oJobElementsResponse.data = new multiplejobelementsresponse { JobElements = liJobElements };
                oJobElementsResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
                oJobElementsResponse.meta.code = Convert.ToInt32(eResponse);
            }
            return oJobElementsResponse;
        }

    }
}
