using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using System.Linq;
using System.Web;

namespace tovuti_api
{
    public static class Common
    {

        public static NpgsqlConnection GetConnection()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            NpgsqlConnection DBconn = null;

            try
            {

                DBconn = new NpgsqlConnection(connectionString);
                DBconn.Open();
            }
            catch (Exception ex)
            {
                //ex should be written into a error log
                ex.Data.Clear();
                // dispose  to avoid connections leak
                if (DBconn != null)
                {
                    DBconn.Dispose();
                }
            }
            return DBconn;
        }
    }
}