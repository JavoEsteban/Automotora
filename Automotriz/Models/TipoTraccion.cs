using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class TipoTraccion
    {
        public int ID_TIPOTRACCION { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }

        public List<TipoTraccion> Consultar_Traccion()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<TipoTraccion> listaTraccion = new List<TipoTraccion>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TIPO_TRACCION", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TipoTraccion traccion = new TipoTraccion();
                    traccion.ID_TIPOTRACCION = Convert.ToInt32(reader["ID_TIPOTRACCION"].ToString());
                    traccion.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    traccion.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaTraccion.Add(traccion);

                }


            }



            return listaTraccion;
        }

        public RespuestaServicio agregarTraccion(TipoTraccion traccion) //Agrega un tipo de traccion a la base datos en la tabla TIPO_TRACCION
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_TIPO_TRACCION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", traccion.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", traccion.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                    }

                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente la Traccion";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar la Traccion";
                return respuesta;
            }
        }

        public TipoTraccion Consulta_Traccion_Por_Id(TipoTraccion traccion)
        { //traer los datos de la marca consultada
            TipoTraccion traccionConsultada = new TipoTraccion();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_TRACCION_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_TIPOTRACCION", traccion.ID_TIPOTRACCION);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    traccionConsultada.ID_TIPOTRACCION = Convert.ToInt32(reader["ID_TIPOTRACCION"].ToString());
                    traccionConsultada.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    traccionConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return traccionConsultada;
        }

        public RespuestaServicio Editar_Traccion(TipoTraccion traccion)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_TIPO_TRACCION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPOTRACCION", traccion.ID_TIPOTRACCION);
                    command.Parameters.AddWithValue("@DESCRIPCION", traccion.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", traccion.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo la traccion satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar con nombre: " + traccion.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarTraccion(TipoTraccion traccion)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_TIPO_TRACCION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPOTRACCION", traccion.ID_TIPOTRACCION);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se elimino la tracción satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar la tracción  seleccionada";
            }

            return respuestaServicio;
        }
    }
}