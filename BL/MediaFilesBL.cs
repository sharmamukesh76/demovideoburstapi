using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoVideoBurstApi.Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DemoVideoBurstApi.BL
{
    public class MediaFilesBL
    {

         internal List<MediaFiles> GetMediaFilesByGuid(Guid? gGuid)
        {
            List<MediaFiles> liMediaFiles = null;
            if (gGuid != null)
            {
                liMediaFiles = GetMediaFiles(gGuid);
                if (liMediaFiles != null && liMediaFiles.Count > 0)
                {
                    return liMediaFiles;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private List<MediaFiles> GetMediaFiles(Guid? gGuid = null)
        {
            List<MediaFiles> liMediaFiles = new List<MediaFiles>();
            MediaFiles oMediaFiles = null;
            try
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    conn.Open();
                    RenderTemplateElementsBL oRenderTemplateElementsBL = new RenderTemplateElementsBL();
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = " Select M.urlid ,MP.description,M.width, M.height, M.mediatype, isnull(Mu.dsignedurl, '') dsignedurl from tblJobs Job INNER JOIN tblMedia M ON Job.id = M.jobid INNER JOIN tblMediaUrls MU On M.urlid = MU.id INNER JOIN tblMediaProfiles MP On M.profile = MP.profileid WHERE MP.active = 1 and M.active=1 and Job.status=100000 and Job.deleted=0 and Job.guid='" + gGuid + "' Order by MU.created desc";
                    sqlCmd.Connection = conn;
                    reader = sqlCmd.ExecuteReader();
                    ElementTypesBL oElementTypesBL = new ElementTypesBL();
                    while (reader.Read())
                    {
                        oMediaFiles = new MediaFiles();
                        oMediaFiles.url = Convert.ToString(reader["dsignedurl"]);
                        oMediaFiles.description = Convert.ToString(reader["description"]);
                        oMediaFiles.width = Convert.ToInt32(reader["width"]);
                        oMediaFiles.height = Convert.ToInt32(reader["height"]);
                        oMediaFiles.mediatype = Convert.ToInt32(reader["mediatype"]);
                        liMediaFiles.Add(oMediaFiles);
                    }                   
                }
            }
            catch (Exception ex)
            {

            }
            return liMediaFiles;
        }
    }
}