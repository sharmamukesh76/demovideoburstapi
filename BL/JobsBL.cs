using DemoVideoBurstApi.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoVideoBurstApi.BL
{
    public class JobsBL
    {
        public ServerResponse.ResponseCodes GetJobByJobId(out List<Jobs> oJob, Guid lJobId)
        {
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            oJob = null;
            if (lJobId != null)
            {
                oJob = GetJobs(lJobId);
                if (oJob != null && oJob.Count > 0)
                {
                    eResponse = ServerResponse.ResponseCodes.Success;
                    //oJob = liJobs[0];
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

        private List<Jobs> GetJobs(Guid lJobId)
        {
            List<Jobs> liJobs = new List<Jobs>();
            Jobs oJobs = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    conn.Open();
                    MediaFilesBL oMediaFilesBL = new MediaFilesBL();
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "Select distinct [guid],[templateguid],Job.title,[createdby],[updated],JS.title as status from tblJobs Job INNER JOIN tblMedia M ON Job.id = M.jobid INNER JOIN tblMediaUrls MU On M.urlid = MU.id INNER JOIN tblMediaProfiles MP On M.profile = MP.profileid INNER JOIN tblJobStatusOptions JS on Job.status = JS.status  WHERE MP.active = 1 and M.active=1 and Job.status=100000 and Job.deleted=0 and Job.guid ='" + lJobId + "'";
                    sqlCmd.Connection = conn;
                    reader = sqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        oJobs = new Jobs();
                        oJobs.guid = Guid.Parse(reader["guid"].ToString());
                        oJobs.templateguid = Guid.Parse(reader["templateguid"].ToString());
                        oJobs.title = reader["title"].ToString();
                        oJobs.updateddate = Convert.ToDateTime(reader["updated"].ToString());
                        oJobs.status = reader["status"].ToString();                      
                        oJobs.media = oMediaFilesBL.GetMediaFilesByGuid(Guid.Parse(reader["guid"].ToString()));
                        liJobs.Add(oJobs);
                    }
                    return liJobs.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return liJobs.ToList();

        }

        public ServerResponse.ResponseCodes AddJob(out Guid gJobGuid, Job oJob)
        {
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            gJobGuid = Guid.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"INSERT INTO [tblJobs] ( [title],[callback],[clickurl],[callbackformat],[templateguid],[companyid],[createdby]) VALUES (@Title, @CallBack, @ClickUrl, 'JSON', @templateguid, 817, 10721) IF(@@ROWCOUNT>0)
		                    BEGIN
			                    SELECT [guid] FROM [tblJobs] WHERE id=SCOPE_IDENTITY() 
                            END
                           ";
                        cmd.Parameters.AddWithValue("@Title", oJob.title);
                        cmd.Parameters.AddWithValue("@CallBack", oJob.callback.ToString());                       
                        cmd.Parameters.AddWithValue("@ClickUrl", oJob.clickurl);
                        cmd.Parameters.AddWithValue("@templateguid", oJob.templateguid);

                        conn.Open();
                        object oReturnValue = cmd.ExecuteScalar();

                        JobElementsBL oJobElementsBL = new JobElementsBL();
                        // Newly Added Code

                        if(oReturnValue != null && !String.IsNullOrEmpty(Convert.ToString(oReturnValue)) && oJobElementsBL.AddJobElements(oJob.elements, oJob.templateguid, new Guid(oReturnValue.ToString())))
                        {
                            //oJobElementsBL.AddJobElements(oJob.elements, oJob.templateguid, new Guid(oReturnValue.ToString()));
                            gJobGuid = new Guid(Convert.ToString(oReturnValue));
                            eResponse = ServerResponse.ResponseCodes.Success;
                         
                        }
                        else
                        {
                            eResponse = ServerResponse.ResponseCodes.DatabaseInsertionError;                          
                        }
                        
                        //Previous Code
                        //if (oReturnValue != null
                        //    && !String.IsNullOrEmpty(Convert.ToString(oReturnValue))
                        //    && oJobElementsBL.EditJobElements(oJob.elements, new Guid(oReturnValue.ToString())))
                        //{
                        //    gJobGuid = new Guid(Convert.ToString(oReturnValue));
                        //    eResponse = ServerResponse.ResponseCodes.Success;

                        //    ServerResponse.ResponseCodes eResponseCode = GetJobByJobGuid(out oJob, null, lUserId, gJobGuid);
                        //    //Add Stats
                        //    if (oStats != null && oStats.contentid == null)
                        //    {
                        //        if (eResponseCode == ServerResponse.ResponseCodes.Success && oJob != null)
                        //            oStats.contentid = oJob.id;
                        //    }
                        //}
                        //else
                        //{
                        //    eResponse = ServerResponse.ResponseCodes.DatabaseInsertionError;
                        //    if (oStats != null && oStats.details != null)
                        //    {
                        //        oStats.details.message = "Failed to add the job.";
                        //    }
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return eResponse;
        }
        
        public ServerResponse.ResponseCodes EditJob(out Guid gJobGuid, Guid guid, Job oJob)
        {
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            gJobGuid = Guid.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Update [tblJobs] set [title]= @Title,[callback]=@CallBack,[clickurl]=@ClickUrl,[templateguid]=@templateguid where guid=@jobguid IF(@@ROWCOUNT>0)
		                    BEGIN
			                    SELECT [guid] FROM [tblJobs] WHERE guid=@jobguid
                            END
                           ";
                        cmd.Parameters.AddWithValue("@Title", oJob.title);
                        cmd.Parameters.AddWithValue("@CallBack", oJob.callback.ToString());
                        cmd.Parameters.AddWithValue("@ClickUrl", oJob.clickurl);
                        cmd.Parameters.AddWithValue("@templateguid", oJob.templateguid);
                        cmd.Parameters.AddWithValue("@jobguid", guid);

                        conn.Open();
                        object oReturnValue = cmd.ExecuteScalar();

                        JobElementsBL oJobElementsBL = new JobElementsBL();
                        // Newly Added Code

                        if (oReturnValue != null && !String.IsNullOrEmpty(Convert.ToString(oReturnValue)) && oJobElementsBL.EditJobElements(oJob.elements, guid))
                        {                            
                            gJobGuid = new Guid(Convert.ToString(oReturnValue));
                            eResponse = ServerResponse.ResponseCodes.Success;
                        }
                        else
                        {
                            eResponse = ServerResponse.ResponseCodes.DatabaseInsertionError;
                        }
           
                    }
                }

            }
            catch (Exception ex)
            {
                eResponse = ServerResponse.ResponseCodes.Internal_Error;
            }
            return eResponse;
        }


    }
}