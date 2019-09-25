using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class ContratoConsignacion
    {
        public int ID_CONTRATO_CONSIGNACION { get; set; }
        public int ID_VEHICULO { get; set; }
        public int ID_SEGURO { get; set; }
        public int PLAZO_CONSIGNACION { get; set; }
        public decimal COMISION_VENDEDOR { get; set; }
        public int MULTA { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public string PATENTE { get; set; }
        public int ANO { get; set; }
        public string MOTOR { get; set; }
        public string CHASIS { get; set; }
        public string TIPO_VEHICULO { get; set; }
        public int PRECIO_VENTA { get; set; }
        public string MARCA { get; set; }
        public string NOMBRE_MODELO { get; set; }
        public string COLOR { get; set; }
        public int PRECIO_COMPRA { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string APELLIDO_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string COMUNA_CLIENTE { get; set; }
        public string CIUDAD_CLIENTE { get; set; }
        public string RUT_CLIENTE { get; set; }



        public RespuestaServicio agregarContrato(ContratoConsignacion contrato)
        {

            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();



            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_CONTRATO_CONSIGNACION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_VEHICULO", contrato.ID_VEHICULO);
                    command.Parameters.AddWithValue("@ID_SEGURO", contrato.ID_SEGURO);
                    command.Parameters.AddWithValue("@PLAZO_CONSIGNACION", contrato.PLAZO_CONSIGNACION);
                    command.Parameters.AddWithValue("@COMISION_VENDEDOR", contrato.COMISION_VENDEDOR);
                    command.Parameters.AddWithValue("@MULTA", contrato.MULTA);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        contrato.ID_CONTRATO_CONSIGNACION = Convert.ToInt32(reader["ID_CONTRATO_CONSIGNACION"]);
                    }


                }
                sql.CerrarConnection(conn);


                respuesta.Respuesta = "OK";
                respuesta.Descripcion = contrato.ID_CONTRATO_CONSIGNACION.ToString();
                respuesta.Detalle_Error = "Se agrego correctamente El contrato";
                return respuesta;



            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar contrato ,Error: " + ex.Message;
                return respuesta;
            }
        }

        public ContratoConsignacion Consulta_Contrato_por_ID(int idContrato)
        {
            ContratoConsignacion contratoConsultado = new ContratoConsignacion();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_CONTRATO_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_CONTRATO_CONSIGNACION", idContrato);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    contratoConsultado.ID_CONTRATO_CONSIGNACION = Convert.ToInt32(reader["ID_CONTRATO_CONSIGNACION"].ToString());
                    contratoConsultado.ID_SEGURO = Convert.ToInt32(reader["ID_SEGURO"].ToString());
                    contratoConsultado.PLAZO_CONSIGNACION = Convert.ToInt32(reader["PLAZO_CONSIGNACION"].ToString());
                    contratoConsultado.COMISION_VENDEDOR = Convert.ToInt32(reader["COMISION_VENDEDOR"].ToString());
                    contratoConsultado.MULTA = Convert.ToInt32(reader["MULTA"].ToString());
                    contratoConsultado.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];

                   
                    contratoConsultado.ANO = Convert.ToInt32(reader["ANO"].ToString());
                    contratoConsultado.PATENTE = reader["PATENTE"].ToString();
                    contratoConsultado.MOTOR = (reader["MOTOR"]).ToString();
                    contratoConsultado.CHASIS = (reader["CHASIS"]).ToString();
                    contratoConsultado.TIPO_VEHICULO = (reader["CARROCERIA"]).ToString();
                    if (reader["PRECIO_COMPRA"] != DBNull.Value)
                    {
                        contratoConsultado.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"].ToString());
                    }
                    else
                    {
                        contratoConsultado.PRECIO_COMPRA = -1;
                    }
                    contratoConsultado.MARCA = reader["MARCA"].ToString();
                    contratoConsultado.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    contratoConsultado.COLOR = reader["COLOR"].ToString();
                    contratoConsultado.NOMBRE_CLIENTE = reader["NOMBRES"].ToString();
                    contratoConsultado.APELLIDO_CLIENTE = reader["APELLIDOS"].ToString();
                    contratoConsultado.RUT_CLIENTE = reader["RUT"].ToString();
                    contratoConsultado.DIRECCION_CLIENTE = reader["DIRECCION"].ToString();
                    contratoConsultado.COMUNA_CLIENTE = reader["COMUNA"].ToString();
                    contratoConsultado.CIUDAD_CLIENTE = reader["CIUDAD"].ToString();

                }


            }

            return contratoConsultado;
        }
    }
}