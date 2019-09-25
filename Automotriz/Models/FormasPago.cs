using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class FormasPago
    {
        public int ID_FORMA_PAGO { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }

        public RespuestaServicio AgregarFormaPago(FormasPago forma) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_FORMA_PAGO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", forma.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", forma.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();
                }

                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente la Forma de Pago";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar la Forma de Pago";
                return respuesta;
            }
        }

        public List<FormasPago> Consultar_Forma_Pago()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<FormasPago> listaFormasPago = new List<FormasPago>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_FORMAS_PAGO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FormasPago formas = new FormasPago();
                    formas.ID_FORMA_PAGO = Convert.ToInt32(reader["ID_FORMA_PAGO"].ToString());
                    formas.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    formas.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());

                    listaFormasPago.Add(formas);

                }
            }

            return listaFormasPago;
        }

        public FormasPago Consulta_FormaPago_Por_Id(int ID_FORMA_PAGO)
        { //traer los datos de la marca consultada
            FormasPago formaConsultada = new FormasPago();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_FORMA_PAGO_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_FORMA_PAGO", ID_FORMA_PAGO);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    formaConsultada.ID_FORMA_PAGO = Convert.ToInt32(reader["ID_FORMA_PAGO"].ToString());
                    formaConsultada.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    formaConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return formaConsultada;
        }

        public RespuestaServicio Editar_FormaPago(FormasPago forma)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_FORMA_PAGO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_FORMA_PAGO", forma.ID_FORMA_PAGO);
                    command.Parameters.AddWithValue("@DESCRIPCION", forma.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", forma.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo la Forma de Pago satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar Forma de Pago, nombre: " + forma.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarFormaPago(int ID_FORMA_PAGO)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_FORMA_PAGO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_FORMA_PAGO", ID_FORMA_PAGO);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó marca satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar la marca seleccionada";
            }

            return respuestaServicio;
        }

    }
}