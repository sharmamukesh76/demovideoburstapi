using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoVideoBurstApi.Entities;
using DemoVideoBurstApi.Models;

namespace DemoVideoBurstApi.VideoBurstAPIBAL
{
    public class templatesBL
    {

        public ServerResponse.ResponseCodes GetTemplateByTemplateGuid(out Templates pTemplates, Guid? gTemplateGuid, bool? ActiveOnly = true, int? iStatus = null, bool bCleanObject = true, [CallerMemberName] string callingMethod = "", [CallerLineNumber] int callingFileLineNumber = 0, long jobid = 0, string jobStatus = "")
        {
            pTemplates = null;
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            if (gTemplateGuid != null)
            {
                if (oStats != null && oStats.contentid == null)
                {
                    Templates oTemplatesTemp = null;
                    ServerResponse.ResponseCodes eResponseCode = GetTemplateByTemplateGuid(out oTemplatesTemp, null, lUserId, gTemplateGuid, ActiveOnly, iStatus);
                    if (eResponseCode == ServerResponse.ResponseCodes.Success && oTemplatesTemp != null)
                        oStats.contentid = oTemplatesTemp.id;
                }
                if (oStats != null && oStats.details != null)
                {
                    oStats.details.jsonobjectname = "gTemplateGuid";
                    oStats.details.json = Newtonsoft.Json.JsonConvert.SerializeObject(gTemplateGuid);
                }
                List<Templates> liTemplates = GetTemplates(oStats, lUserId, iStatus, ActiveOnly, gTemplateGuid, null, null, null, false, false, null, null, null, null, null, bCleanObject, callingMethod, callingFileLineNumber, 0, 9, "created", "DESC", "web", jobid, jobStatus);
                if (liTemplates != null && liTemplates.Count > 0)
                {
                    pTemplates = liTemplates[0];
                    eResponse = ServerResponse.ResponseCodes.Success;
                }
                else
                {
                    eResponse = ServerResponse.ResponseCodes.NoResultFound;
                }
            }
            else
            {
                eResponse = ServerResponse.ResponseCodes.InvalidParams;
            }
            return eResponse;
        }

        public ServerResponse.ResponseCodes GetTemplates(out List<Templates> liTemplates, bool? ActiveOnly = true, long? lCreatedFor = null, bool? bAutoGenerateOnly = null, int? iCategoryId = null, double? dMaxDuration = null, string SearchTerm = null, bool bCleanObject = true, [CallerMemberName] string callingMethod = "", [CallerLineNumber] int callingFileLineNumber = 0, int pageNo = 0, int pageSize = 9, string sortColumn = "created", string sortOrder = "DESC", string siteType = "web", int jobid = 0)
        {
            liTemplates = null;
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            liTemplates = GetTemplates(null, ActiveOnly, null, null, lCreatedFor, null, false, false, bAutoGenerateOnly, iCategoryId, null, dMaxDuration, SearchTerm, bCleanObject, callingMethod, callingFileLineNumber, pageNo, pageSize, sortColumn, sortOrder, siteType);
            if (liTemplates != null && liTemplates.Count > 0)
            {
                eResponse = ServerResponse.ResponseCodes.Success;
            }
            else
            {
                eResponse = ServerResponse.ResponseCodes.NoResultFound;
            }
            return eResponse;
        }


    }
}