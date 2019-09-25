using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Documentos
    {
        public int ID_DOCUMENTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string DOCUMENTO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string EXTENSION { get; set; }



        public static string Base64Encoder(byte[] plainText) //codifica a base 64
        {
            string strIMG = System.Convert.ToBase64String(plainText);
            return strIMG;
        }

        public static byte[] Base64Decoder(string base64EncodedData) // pasa de base 64 a byte []
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return base64EncodedBytes;
        }

        public RespuestaServicio AgregarDocumentos(Documentos documento) //Agrega una sucursal a la tabla sucursales
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {


                string documentoCompleto = documento.DOCUMENTO;
                string[] documentoARR = documentoCompleto.Split(',');
                string documentoSinExtension = documentoARR[1];
                documento.TIPO_DOCUMENTO = documentoARR[0];

                byte[] documentoBaseDatos = Base64Decoder(documentoSinExtension);

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_DOCUMENTOS", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", documento.DESCRIPCION);
                    command.Parameters.AddWithValue("@DOCUMENTO", documentoBaseDatos);
                    command.Parameters.AddWithValue("@TIPO_DOCUMENTO", documento.TIPO_DOCUMENTO);
                    command.Parameters.AddWithValue("@EXTENSION", documento.EXTENSION);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Documento";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Oops, no fue posible agregar el Documento " + ex.Message;
                return respuesta;
            }
        }
        public List<Documentos> Consultar_Documentos()
        { //trae las sucursales de la tabla sucursal
            List<Documentos> listaDocumentos = new List<Documentos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("[CONSULTAR_DOCUMENTOS]", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Documentos documento = new Documentos();
                    documento.ID_DOCUMENTO = Convert.ToInt32(reader["ID_DOCUMENTO"].ToString());
                    documento.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    documento.EXTENSION = reader["EXTENSION"].ToString();



                    listaDocumentos.Add(documento);

                }


            }

            return listaDocumentos;
        }



    }
}