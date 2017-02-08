using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoVideoBurstApi.Entities;
using DemoVideoBurstApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Web.Http;

namespace DemoVideoBurstApi.BL
{
    public class TemplatesBL
    {
        public ServerResponse.ResponseCodes GetTemplateByTemplateGuid(out Templates pTemplates, Guid id)
        {
            pTemplates = null;
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;

            List<Templates> liTemplates = GetTemplates(id);
            if (liTemplates != null && liTemplates.Count > 0)
            {
                pTemplates = liTemplates[0];
                eResponse = ServerResponse.ResponseCodes.Success;
            }
            else
            {
                eResponse = ServerResponse.ResponseCodes.NoResultFound;
            }

            return eResponse;
        }

        private List<Templates> GetTemplates(Guid id)
        {
            List<Templates> liTemplates = new List<Templates>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblTemplates where deleted=0 and guid='" + id + "'", conn);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            liTemplates.Add(new Templates { guid = Guid.Parse(dt.Rows[0][1].ToString()), title = dt.Rows[0][2].ToString(), link = dt.Rows[0][9].ToString(), previewimage = dt.Rows[0][16].ToString(), previewimagethumb = dt.Rows[0][17].ToString() });
                        }
                        return liTemplates;
                    }

                    else
                    {
                        return liTemplates.ToList();
                    }

                }
                catch (Exception ex)
                {
                    return liTemplates.ToList();
                }

            }

        }


        public ServerResponse.ResponseCodes GetAllTemplates(out List<Templates> pTemplates)
        {
            pTemplates = null;
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;

            List<Templates> liTemplates = GetAllTemplates();
            if (liTemplates != null && liTemplates.Count > 0)
            {
                pTemplates = liTemplates;
                eResponse = ServerResponse.ResponseCodes.Success;
            }
            else
            {
                eResponse = ServerResponse.ResponseCodes.NoResultFound;
            }

            return eResponse;
        }
        private List<Templates> GetAllTemplates()
        {
            List<Templates> liTemplates = new List<Templates>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblTemplates where deleted=0", conn);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            liTemplates.Add(new Templates { guid = Guid.Parse(dr["guid"].ToString()), title = dr["title"].ToString(), link = dr["link"].ToString(), previewimage = dr["previewimage"].ToString(), previewimagethumb = dr["previewimagethumb"].ToString() });
                        }
                        return liTemplates;
                    }

                    else
                    {
                        return liTemplates.ToList();
                    }

                }
                catch (Exception ex)
                {
                    return liTemplates.ToList();
                }

            }

        }


        public ServerResponse.ResponseCodes GetTemplateElementsByTemplateGuid(out List<TemplateElements> liTemplateElements, Guid? gTemplateGuid)
        {
            liTemplateElements = null;
            ServerResponse.ResponseCodes eResponse = ServerResponse.ResponseCodes.Internal_Error;
            if (gTemplateGuid != null)
            {
                liTemplateElements = GetTemplatesElements(gTemplateGuid);
                if (liTemplateElements != null && liTemplateElements.Count > 0)
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


        internal List<TemplateElements> GetTemplatesElements(Guid? gTemplateGuid)
        {
            List<TemplateElements> liTemplateElements = new List<TemplateElements>();
            TemplateElements oTemplateElements = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    conn.Open();
                    RenderTemplateElementsBL oRenderTemplateElementsBL = new RenderTemplateElementsBL();
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.Text;                  
                    sqlCmd.CommandText = "Select * from tblTemplateElements where active=1 and templateguid='" + gTemplateGuid + "'";
                    sqlCmd.Connection = conn;
                    reader = sqlCmd.ExecuteReader();
                   
                    while (reader.Read())
                    {
                        oTemplateElements = new TemplateElements();
                        oTemplateElements.guid = Guid.Parse(reader["guid"].ToString());
                        oTemplateElements.templateguid = Guid.Parse(reader["templateguid"].ToString());
                        oTemplateElements.rendertemplateelement = oRenderTemplateElementsBL.GetRenderTemplateElementByGuid(Guid.Parse(reader["renderelementguid"].ToString()));
                        oTemplateElements.title = reader["title"].ToString();
                        oTemplateElements.helptext = Convert.ToString(reader.GetValue(3));
                        oTemplateElements.mode = Convert.ToInt16(reader["mode"]);
                        oTemplateElements.defaultvalue = reader["defaultvalue"].ToString();
                        oTemplateElements.active = Convert.ToBoolean(reader["active"]);
                        oTemplateElements.sortorder = Convert.ToInt16(reader["sortorder"]);
                        //reader["sortorder"] != DBNull.Value ? Convert.ToInt32(reader["sortorder"]) : 9999;
                        oTemplateElements.groupname = Convert.ToString(reader["groupname"]);
                        oTemplateElements.DefaultThumbnail = reader["DefaultThumbnail"].ToString();
                        liTemplateElements.Add(oTemplateElements);
                        //conn.Close();
                    }
                    return liTemplateElements;
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return liTemplateElements;

        }


    }

}