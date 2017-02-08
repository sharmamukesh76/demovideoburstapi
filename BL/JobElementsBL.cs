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
    public class JobElementsBL
    {

        public ServerResponse.ResponseCodes GetJobElementsByJobGuid(out List<JobElements> liJobElements, Guid? gJobGuid)
        {
            liJobElements = null;
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            if (gJobGuid != null)
            {
                liJobElements = GetJobElements(gJobGuid);
                if (liJobElements != null && liJobElements.Count > 0)
                {
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

        internal List<JobElements> GetJobElements(Guid? gJobGuid)
        {
            List<JobElements> liJobElements = new List<JobElements>();
            JobElements oJobElements = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    conn.Open();
                  //  RenderTemplateElementsBL oRenderTemplateElementsBL = new RenderTemplateElementsBL();
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = @"SELECT [tblJobElements].guid,[jobguid]
                      ,[templateelementguid]
                      ,[value] , tblTemplateElements.mode
                      FROM[tblJobElements] INNER JOIN tblTemplateElements
                      on[tblJobElements].templateelementguid = tblTemplateElements.guid where tblTemplateElements.mode != 2 and jobguid='" + gJobGuid + "'";
                    sqlCmd.Connection = conn;
                    reader = sqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        oJobElements = new JobElements();
                        oJobElements.guid = Guid.Parse(reader["guid"].ToString());                        
                        oJobElements.jobguid = Guid.Parse(reader["jobguid"].ToString());
                        oJobElements.templateelementguid = Guid.Parse(reader["templateelementguid"].ToString());
                        oJobElements.value = reader["value"].ToString();
                        liJobElements.Add(oJobElements);
                        //conn.Close();
                    }
                    if (conn != null)
                    {
                        conn.Close();
                    }
                    return liJobElements;                  
                }
            }
            catch (Exception ex)
            {

            }
            return liJobElements;

        }

        internal bool AddJobElements(List<JobElements> oElements, Guid gTemplateGuid, Guid gJobGuid)
        {
            bool bSuccess = true;
            try
            {
                if (oElements.Count > 0 && gJobGuid != null)
                {
                    if (bSuccess)
                    {

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"INSERT INTO [tblJobElements] (guid, [jobguid], [templateelementguid], [value], [created] )
                                    SELECT NEWID(), tblJobs.guid, tblTemplateElements.guid, tblTemplateElements.defaultvalue, GETDATE()
                                    FROM tblJobs,tblTemplateElements 
                                    WHERE tblTemplateElements.templateguid = @Guid and tblJobs.guid = @JobGuid";

                                cmd.Parameters.AddWithValue("@JobGuid", gJobGuid.ToString());
                                cmd.Parameters.AddWithValue("@Guid", gTemplateGuid.ToString());
                                conn.Open();
                                int oReturnValue = cmd.ExecuteNonQuery();
                                if (oReturnValue <= 0)
                                    bSuccess = false;
                            }
                        }
                    }

                    //Updating the Values of the Elements which are passed through the client in request body
                    foreach (JobElements Element in oElements)
                    {
                        if (bSuccess)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = @"UPDATE [tblJobElements]
		                            SET	 [value] = (CASE WHEN @Value IS NULL THEN @Value ELSE @Value END) 
			                            ,[updated] = GETDATE() WHERE [templateelementguid]=@templateelementguid and [jobguid]= @jobguid";

                                    cmd.Parameters.AddWithValue("@Value", Element.value);
                                    cmd.Parameters.AddWithValue("@templateelementguid", Element.templateelementguid);
                                    cmd.Parameters.AddWithValue("@jobguid", gJobGuid.ToString());
                                    conn.Open();

                                    int oReturnValue = cmd.ExecuteNonQuery();
                                    if (oReturnValue <= 0)
                                        bSuccess = false;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return bSuccess;
        }
        internal bool EditJobElements(List<JobElements> oElements, Guid gJobGuid)
        {
            bool bSuccess = true;
            try
            {
                if (oElements.Count > 0 && gJobGuid != null)
                {

                    foreach (JobElements Element in oElements)
                    {
                        if (bSuccess)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = @"UPDATE [tblJobElements]
		                            SET	 [value] = (CASE WHEN @Value IS NULL THEN @Value ELSE @Value END) 
			                            ,[updated] = GETDATE() WHERE [templateelementguid]=@templateelementguid and [jobguid]= @jobguid";

                                    cmd.Parameters.AddWithValue("@Value", Element.value);
                                    cmd.Parameters.AddWithValue("@templateelementguid", Element.templateelementguid);
                                    cmd.Parameters.AddWithValue("@jobguid", gJobGuid.ToString());

                                    conn.Open();
                                    int oReturnValue = cmd.ExecuteNonQuery();

                                    if (oReturnValue <= 0)
                                        bSuccess = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return bSuccess;
        }
    }
}