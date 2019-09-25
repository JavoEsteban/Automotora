using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Color
    {
        public int ID_COLOR { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }


        public List<Color> Consultar_Color()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<Color> listaColor = new List<Color>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_COLOR", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Color colores = new Color();
                    colores.ID_COLOR = Convert.ToInt32(reader["ID_COLOR"].ToString());
                    colores.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    colores.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaColor.Add(colores);

                }


            }

            return listaColor;
        }

        public RespuestaServicio AgregarColor(Color color) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_COLOR", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", color.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", color.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Color";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar el Color";
                return respuesta;
            }
        }

        public Color Consulta_Color_Por_Id(Color color)
        { //traer los datos de la marca consultada
            Color colorConsultado = new Color();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_COLOR_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_COLOR", color.ID_COLOR);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    colorConsultado.ID_COLOR = Convert.ToInt32(reader["ID_COLOR"].ToString());
                    colorConsultado.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    colorConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return colorConsultado;
        }

        public RespuestaServicio Editar_Color(Color color)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_COLOR", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_COLOR", color.ID_COLOR);
                    command.Parameters.AddWithValue("@DESCRIPCION", color.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", color.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el Color satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar Color " + color.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarColor(Color color)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_COLOR", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_COLOR", color.ID_COLOR);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó  el Color satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el Color seleccionado";
            }

            return respuestaServicio;
        }
    }

   
}