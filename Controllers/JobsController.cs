using DemoVideoBurstApi.BL;
using DemoVideoBurstApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoVideoBurstApi.Controllers
{
    public class JobsController : ApiController
    {
        JobsBL oJobsBL = new JobsBL();
        /// <summary>
        /// Get a particular job
        /// </summary>
        /// <param name="Id">Guid of the job to find</param>
        /// <returns>A single job json object</returns>       
        [HttpGet]
        // [Route("{guid}")] 
        public JobsResponse GetJobByGuid(Guid guid)
        {
            JobsResponse oMultipleJobResponse = new JobsResponse();
            {               
                List<Jobs> liJobs = new List<Jobs>();
                ServerResponse.ResponseCodes eResponse = oJobsBL.GetJobByJobId(out liJobs, guid);
                oMultipleJobResponse.data = new multiplejobresponse { jobs = liJobs };
                oMultipleJobResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
                oMultipleJobResponse.meta.code = Convert.ToInt32(eResponse);
                return oMultipleJobResponse;
            }
        }


        /// <summary>
        /// Add new job in queue.
        /// </summary>
        /// <param name="oJob">Job json object to add</param>
        /// <returns>Result of the Add job action along with newely added job Guid.</returns>       
        [HttpPost]
      //  [Route("")]
        public JobsGuidResponse AddJob([FromBody]Job oJob)
        {
            JobsGuidResponse oJobsGuidResponse = new JobsGuidResponse();           
            Guid gJobGuid = new Guid();
            ServerResponse.ResponseCodes eResponse = oJobsBL.AddJob(out gJobGuid, oJob);
            oJobsGuidResponse.data = new singlejobguid { guid = gJobGuid };
            oJobsGuidResponse.meta.code = Convert.ToInt32(eResponse);
            oJobsGuidResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
            return oJobsGuidResponse;
        }


        /// <summary>
        /// Edit an existing job. You can edit a job only if it has not been started yet.
        /// </summary>
        /// <param name="guid">Guid of job</param>
        /// <param name="oJob">Result of the Edit job action along with updated job Guid.</param>
        /// <returns></returns>      
        [HttpPut]      
        public JobsGuidResponse EditJob(Guid guid, [FromBody]Job oJob)
        {
            JobsGuidResponse oJobsGuidResponse = new JobsGuidResponse();
            Guid gJobGuid = new Guid();
            ServerResponse.ResponseCodes eResponse = oJobsBL.EditJob(out gJobGuid, guid, oJob);
            oJobsGuidResponse.data = new singlejobguid { guid = gJobGuid };
            oJobsGuidResponse.meta.code = Convert.ToInt32(eResponse);
            oJobsGuidResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
            return oJobsGuidResponse;
        }
    }
}
