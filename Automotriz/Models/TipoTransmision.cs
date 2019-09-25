using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class TipoTransmision
    {
        public int ID_TIPO_TRANSMICION { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }


        public List<TipoTransmision> Consultar_Transmision()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<TipoTransmision> listaTransmision = new List<TipoTransmision>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TRANSMISION", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TipoTransmision transmision = new TipoTransmision();
                    transmision.ID_TIPO_TRANSMICION = Convert.ToInt32(reader["ID_TIPO_TRANSMICION"].ToString());
                    transmision.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    transmision.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaTransmision.Add(transmision);

                }


            }



            return listaTransmision;
        }

        public RespuestaServicio AgregarTransmision(TipoTransmision transmision) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_TIPO_TRANSMISION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", transmision.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", transmision.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente la Transmisión";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar la Transmisión";
                return respuesta;
            }
        }

        public TipoTransmision Consulta_Transmision_Por_Id(TipoTransmision transmision)
        { //traer los datos de la marca consultada
            TipoTransmision traccionConsultada = new TipoTransmision();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_TRANSMISION_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", transmision.ID_TIPO_TRANSMICION);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    traccionConsultada.ID_TIPO_TRANSMICION = Convert.ToInt32(reader["ID_TIPO_TRANSMICION"].ToString());
                    traccionConsultada.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    traccionConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return traccionConsultada;
        }

        public RespuestaServicio Editar_Transmision(TipoTransmision transmision)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_TIPO_TRANSMISION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", transmision.ID_TIPO_TRANSMICION);
                    command.Parameters.AddWithValue("@DESCRIPCION", transmision.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", transmision.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo la transmisión satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar transmisión : " + transmision.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarTransmision(TipoTransmision transmision)// 
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_TRANSMISION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", transmision.ID_TIPO_TRANSMICION);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo la Transmisión satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar la Transmisión seleccionada";
            }

            return respuestaServicio;
        }


    }

}