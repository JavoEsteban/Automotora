using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Disponibilidad
    {
        public int ID_DISPONIBILIDAD { set; get; }
        public string DESCRIPCION { set; get; }
        public int  VIGENCIA { set; get; }


        public List<Disponibilidad> Consultar_Disponibilidad()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<Disponibilidad> listaDisponibilidad = new List<Disponibilidad>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_DISPONIBILIDAD", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Disponibilidad disponibilidad = new Disponibilidad();
                    disponibilidad.ID_DISPONIBILIDAD = Convert.ToInt32(reader["ID_DISPONIBILIDAD"].ToString());
                    disponibilidad.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    disponibilidad.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaDisponibilidad.Add(disponibilidad);

                }


            }



            return listaDisponibilidad;
        }

        public RespuestaServicio AgregarDisponibilidad(Disponibilidad disponibilidad) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_DISPONIBILIDAD", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", disponibilidad.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", disponibilidad.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente la Disponibilidas";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar la Disponibilidas";
                return respuesta;
            }
        }


        public Disponibilidad Consulta_Disponibilidad_Por_Id(Disponibilidad disponibilidad)
        { //traer los datos de la marca consultada
            Disponibilidad disponibilidadConsultada = new Disponibilidad();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_DISPONIBILIDAD_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", disponibilidad.ID_DISPONIBILIDAD);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    disponibilidadConsultada.ID_DISPONIBILIDAD = Convert.ToInt32(reader["ID_DISPONIBILIDAD"].ToString());
                    disponibilidadConsultada.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    disponibilidadConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return disponibilidadConsultada;
        }

        public RespuestaServicio Editar_Disponibilidad(Disponibilidad disponibilidad)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_DISPONIBILIDAD", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", disponibilidad.ID_DISPONIBILIDAD);
                    command.Parameters.AddWithValue("@DESCRIPCION", disponibilidad.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", disponibilidad.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo la Disponibilidad satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar Disponibilidad nombre: " + disponibilidad.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarDisponibilidad(Disponibilidad disponibilidad)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_DISPONIBILIDAD", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", disponibilidad.ID_DISPONIBILIDAD);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó la Disponibilidad satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar la Disponibilidad seleccionada";
            }

            return respuestaServicio;
        }
    }
}