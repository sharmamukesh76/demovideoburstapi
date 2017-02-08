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
    public class RenderTemplateElementsBL
    {
        internal RenderTemplateElements GetRenderTemplateElementByGuid(Guid? gElementGuid)
        {
            List<RenderTemplateElements> liRenderTemplateElements = null;
            if (gElementGuid != null)
            {
                liRenderTemplateElements = GetRenderTemplateElements(gElementGuid);
                if (liRenderTemplateElements != null && liRenderTemplateElements.Count > 0)
                {
                    return liRenderTemplateElements[0];
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

        private List<RenderTemplateElements> GetRenderTemplateElements(Guid? gElementGuid = null)
        {
            List<RenderTemplateElements> liRenderTemplateElements = new List<RenderTemplateElements>();
            RenderTemplateElements oRenderTemplateElements = null;
            try
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    conn.Open();
                    RenderTemplateElementsBL oRenderTemplateElementsBL = new RenderTemplateElementsBL();
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = " SELECT [elements].[type],[types].[tag] typetext,[starttime],[stoptime],[width],[height],[minlength],[maxlength],[group] FROM dbo.[tblRenderTemplateElements] [elements] INNER JOIN dbo.[tblElementTypes][types] ON[types].[type] = [elements].[type] where guid='" + gElementGuid + "'";
                    sqlCmd.Connection = conn;
                    reader = sqlCmd.ExecuteReader();
                    ElementTypesBL oElementTypesBL = new ElementTypesBL();
                    while (reader.Read())
                    {   
                        oRenderTemplateElements = new RenderTemplateElements();            
                        oRenderTemplateElements.elementtype = oElementTypesBL.GetElementTypesByElementType(Convert.ToInt32(reader["type"]));
                        oRenderTemplateElements.typetext = Convert.ToString(reader["typetext"]);
                        oRenderTemplateElements.starttime = reader["starttime"] != DBNull.Value ? Math.Round(Convert.ToDecimal(reader["starttime"]), 0).ToString() : "0";
                        oRenderTemplateElements.stoptime = reader["stoptime"] != DBNull.Value ? Math.Round(Convert.ToDecimal(reader["stoptime"]), 0).ToString() : "0";
                        oRenderTemplateElements.width = !String.IsNullOrEmpty(Convert.ToString(reader["width"])) ? (int?)(reader["width"]) : null;
                        oRenderTemplateElements.height = !String.IsNullOrEmpty(Convert.ToString(reader["height"])) ? (int?)(reader["height"]) : null;
                        oRenderTemplateElements.minlength = !String.IsNullOrEmpty(Convert.ToString(reader["minlength"])) ? (int?)(reader["minlength"]) : null;
                        oRenderTemplateElements.maxlength = !String.IsNullOrEmpty(Convert.ToString(reader["maxlength"])) ? (int?)(reader["maxlength"]) : null;
                        oRenderTemplateElements.group = Convert.ToInt32(reader["group"]);                        
                        liRenderTemplateElements.Add(oRenderTemplateElements);
                    }                   
                }
            }
            catch (Exception ex)
            {

            }
            return liRenderTemplateElements;
        }

    }
}