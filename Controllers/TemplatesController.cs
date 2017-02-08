using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoVideoBurstApi.Models;
using System.Data.SqlClient;
using System.Data;
using DemoVideoBurstApi.Entities;
using static DemoVideoBurstApi.Entities.TemplateResponse;
using System.Configuration;
using DemoVideoBurstApi.BL;

namespace DemoVideoBurstApi.Controllers
{
    public class TemplatesController : ApiController
    {
        /// <summary>
        /// Get the template details by passing passing guid as parameter.
        /// </summary>
        /// <param name="guid">Pass Guid as parameter to retrieve the details of the template</param>
        /// <returns>Template Element</returns>     
        [HttpGet]
        public TemplateResponse GetTemplateByGuid(Guid id)
        {
            TemplateResponse oSingleTemplateResponse = new TemplateResponse();
            {
                TemplatesBL oTemplatesBL = new TemplatesBL();
                Templates oTemplates = new Templates();
                ServerResponse.ResponseCodes eResponse = oTemplatesBL.GetTemplateByTemplateGuid(out oTemplates, id);
                oSingleTemplateResponse.data = new singleTemplateresponse { Template = oTemplates };
                oSingleTemplateResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
                oSingleTemplateResponse.meta.code = Convert.ToInt32(eResponse);
            }
            return oSingleTemplateResponse;
        }

        /// <summary>
        /// Get all the Templates
        /// </summary>
        /// <returns>Templates List</returns>
        [HttpGet]
        public TemplatesResponse GetTemplates()
        {
            TemplatesResponse oTemplatesResponse = new TemplatesResponse();
            {
                TemplatesBL oTemplatesBL = new TemplatesBL();
                List<Templates> liTemplates = new List<Templates>();
                ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
                eResponse = oTemplatesBL.GetAllTemplates(out liTemplates);
                oTemplatesResponse.data = new multipleTemplateresponse { Templates = liTemplates };
                oTemplatesResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
                oTemplatesResponse.meta.code = Convert.ToInt32(eResponse);
            }
            return oTemplatesResponse;
        }


        /// <summary>
        /// Get the template elements list for a particular template.
        /// </summary>
        /// <param name="guid">The template guid</param>
        /// <returns>Template Elements list for specified template</returns>      
        [HttpGet]       
        public TemplateElementsResponse GetTemplateElementsByTemplateGuid(Guid guid)
        {
            TemplateElementsResponse oTemplateElementsResponse = new TemplateElementsResponse();           
            {
                TemplatesBL oTemplateElementsBL = new TemplatesBL();
                List<TemplateElements> liTemplateElements = new List<TemplateElements>();
                ServerResponse.ResponseCodes eResponse = oTemplateElementsBL.GetTemplateElementsByTemplateGuid(out liTemplateElements, guid);
                oTemplateElementsResponse.data = new multipletemplateelementsresponse { TemplateElements = liTemplateElements };
                oTemplateElementsResponse.meta.error_message = ServerResponse.GetResponse(eResponse);
                oTemplateElementsResponse.meta.code = Convert.ToInt32(eResponse);
            }
            return oTemplateElementsResponse;
        }

    }
}
