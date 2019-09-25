using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.connection
{
    public class SQLconn
    {
        public SqlConnection AbrirConnection(string coneccion)
        {
            try
            {
                SqlConnection con = new SqlConnection(coneccion);
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void CerrarConnection(SqlConnection con)
        {
            try
            {
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
               
            }
        }

        public SqlDataReader Ejecuta(SqlCommand command)
        {
            try
            {
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ObtenerConnection()
        {
            try
            {
                try
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["automotriz_larrain"].ConnectionString;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
