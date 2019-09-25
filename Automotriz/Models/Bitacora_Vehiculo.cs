using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Bitacora_Vehiculo
    {
        public int BITACORA_ID { get; set; }
        public int ID_VEHICULO { get; set; }
        public string ESTADO { get; set; }
        public string FECHA { get; set; }
        public string DETALLE { get; set; }
        public int TIPO { get; set; }
        public int ID_USUARIO { get; set; }
        public string NOMBRE_USUARIO { get; set; }


        //procedimiento encargado de agregar informacion realizada a un vehiculo
        //en pocas palabras se agregara a la bitacora la informacion relevante
        public RespuestaServicio AgregarInformacionBitacora(Bitacora_Vehiculo Bitacora) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_BITACORA_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_VEHICULO", Bitacora.ID_VEHICULO);
                    command.Parameters.AddWithValue("@ESTADO", Bitacora.ESTADO);
                    command.Parameters.AddWithValue("@DETALLE", Bitacora.DETALLE);
                    command.Parameters.AddWithValue("@TIPO", Bitacora.TIPO);
                    command.Parameters.AddWithValue("@ID_USUARIO", Bitacora.ID_USUARIO);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente informacion la bitácora";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar informacion a la bitácora";
                return respuesta;
            }
        }

        //Se consultara informacion sobre un auto en especifico
        public List<Bitacora_Vehiculo> Consultar_Bitacora(int ID_VEHICULO)
        {
            List<Bitacora_Vehiculo> HistorialBitacora = new List<Bitacora_Vehiculo>();
            try
            {


                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTA_BITACORA", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Bitacora_Vehiculo bitacorea = new Bitacora_Vehiculo();
                        bitacorea.BITACORA_ID = Convert.ToInt32(reader["BITACORA_ID"].ToString());
                        bitacorea.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"].ToString());
                        bitacorea.ESTADO = reader["ESTADO"].ToString();
                        bitacorea.FECHA = reader["FECHA"].ToString();
                        bitacorea.DETALLE = reader["DETALLE"].ToString();
                        bitacorea.TIPO = Convert.ToInt32(reader["TIPO"].ToString());
                        bitacorea.ID_USUARIO = Convert.ToInt32(reader["ID_USUARIO"].ToString());
                        bitacorea.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        HistorialBitacora.Add(bitacorea);

                    }
                }

                return HistorialBitacora;
            }
            catch (Exception ex)
            {
                return HistorialBitacora;
            }
        }
    }
}