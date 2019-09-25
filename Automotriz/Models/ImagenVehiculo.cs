using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class ImagenVehiculo
    {
        public int ID_IMAGEN { get; set; }
        public int ID_VEHICULO { get; set; }
        public string IMAGEN { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public string EXTENSION { get; set; }
        public int VIGENCIA { get; set; }
        public int POSICION { get; set; }


        public string Consulta_IMAGEN_PRINCIPAL()
        { //traer los datos de la marca consultada
            Marca marcaConsultada = new Marca();
            string imagenBase64 = "";
            Parametro_Generico.Parametro_Genericos OBJParametroGenerico = new Parametro_Generico.Parametro_Genericos();
            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("PROCEDIMIENTO_VELOCIDAD_IMAGEN_PRINCIPAL", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    byte[] arrayImagen = (byte[])reader["IMAGEN_PRINCIPAL"];
                    imagenBase64 = Base64Encoder(arrayImagen);
                }


            }

            return imagenBase64;
        }

        public string Consulta_IMAGEN_COMPRIMIDA()
        { //traer los datos de la marca consultada
            Marca marcaConsultada = new Marca();
            string imagenBase64 = "";
            Parametro_Generico.Parametro_Genericos OBJParametroGenerico = new Parametro_Generico.Parametro_Genericos();
            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("PROCEDIMIENTO_VELOCIDAD_IMAGEN_REDUCIDA", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;




                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    byte[] arrayImagen = (byte[])reader["IMAGEN_COMPRIMIDA"];
                    imagenBase64 = Base64Encoder(arrayImagen);
                }


            }

            return imagenBase64;
        }

        public static string Base64Encoder(byte[] plainText)//PASA BASE 64 A STR
        {
            string strIMG = System.Convert.ToBase64String(plainText);
            return strIMG;
        }
    }
}