using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ders1Api.Models
{
    public class GenelModel
    {
        public bool success { get; set; } = true;
        public string mesaj { get; set; } = "Başarılı";

        public object OgrenciData { get; set; }
        public object OgrenciDetailData { get; set; }
        public object resimData { get; set; }



        public DataTable getQueryToDataTable(string query, DbContext context)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = context.Database.Connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }


    }
}