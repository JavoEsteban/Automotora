using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class TipoConsigna
    {
        public int ID_TIPO_CONSIGNACION { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }

        public List<TipoConsigna> Consultar_tipoConsigna()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<TipoConsigna> listaConsigna = new List<TipoConsigna>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TIPO_CONSIGNACION", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TipoConsigna tipoConsigna = new TipoConsigna();
                    tipoConsigna.ID_TIPO_CONSIGNACION = Convert.ToInt32(reader["ID_TIPO_CONSIGNACION"].ToString());
                    tipoConsigna.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    tipoConsigna.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaConsigna.Add(tipoConsigna);

                }


            }



            return listaConsigna;
        }

        public RespuestaServicio AgregarTipoConsignacion(TipoConsigna tipoConsigna) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_TIPO_CONSIGNACION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", tipoConsigna.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", tipoConsigna.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente la Consignacion";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar la Consignacion";
                return respuesta;
            }
        }

        public TipoConsigna Consulta_TipoConsignacion_Por_Id(TipoConsigna tipoConsigna)
        { //traer los datos de la marca consultada
            TipoConsigna tipoConsignaConsultada = new TipoConsigna();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TIPO_CONSIGNACION_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", tipoConsigna.ID_TIPO_CONSIGNACION);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tipoConsignaConsultada.ID_TIPO_CONSIGNACION = Convert.ToInt32(reader["ID_TIPO_CONSIGNACION"].ToString());
                    tipoConsignaConsultada.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    tipoConsignaConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return tipoConsignaConsultada;
        }

        public RespuestaServicio Editar_TipoConsigna(TipoConsigna tipoConsigna)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_TIPO_CONSIGNACION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", tipoConsigna.ID_TIPO_CONSIGNACION);
                    command.Parameters.AddWithValue("@DESCRIPCION", tipoConsigna.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", tipoConsigna.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el Tipo de Ingreso satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar el Tipo de Ingreso "+ tipoConsigna.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarTipoConsignacion(TipoConsigna tipoConsigna)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_TIPO_CONSIGNACION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", tipoConsigna.ID_TIPO_CONSIGNACION);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó el Tipo de Ingreso satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar Tipo de Ingreso seleccionado";
            }

            return respuestaServicio;
        }
    }

    
}