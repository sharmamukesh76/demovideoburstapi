using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoVideoBurstApi.Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DemoVideoBurstApi.BL
{
    public class ElementTypesBL
    {
        public ElementTypes GetElementTypesByElementType(int? iElementTypeId)
        {
            ElementTypes oElementTypes = null;
            if (iElementTypeId != null)
            {
                List<ElementTypes> liElementTypes = GetTemplatesElements(iElementTypeId);
                if (liElementTypes != null && liElementTypes.Count > 0)
                {
                    oElementTypes = liElementTypes[0];
                }
            }
            return oElementTypes;
        }


        public List<ElementTypes> GetTemplatesElements(int? iElementTypeId = null)
        {
            List<ElementTypes> liElementTypes = new List<ElementTypes>();
            ElementTypes oElementTypes = null;
            try
            { 
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoVideoBurstConnection"].ConnectionString))
                {
                    conn.Open();
                    RenderTemplateElementsBL oRenderTemplateElementsBL = new RenderTemplateElementsBL();
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = " SELECT [type],[engineid],[description],[tag],[foldername] FROM [tblElementTypes] WHERE [type]=" + iElementTypeId + "";
                    sqlCmd.Connection = conn;
                    reader = sqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        oElementTypes = new ElementTypes();
                        oElementTypes.type = Convert.ToInt32(reader["type"]);
                        oElementTypes.engineid = Convert.ToInt32(reader["engineid"]);
                        oElementTypes.description = Convert.ToString(reader["description"]);
                        oElementTypes.tag = Convert.ToString(reader["tag"]);
                        oElementTypes.foldername = Convert.ToString(reader["foldername"]);
                        liElementTypes.Add(oElementTypes);
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
            }
            return liElementTypes;
        }
    }
}