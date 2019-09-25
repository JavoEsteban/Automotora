using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Automotriz.Models.Parametro_Generico
{
    public class Parametro_Genericos
    {

        public static string Base64Encoder(byte[] plainText)//PASA BASE 64 A STR
        {
            string strIMG = System.Convert.ToBase64String(plainText);
            return strIMG;
        }


        public byte[] cambiarResolucionBase64(string ImgBase64)
        {
            Bitmap ImgBitmap = null;
            Image imagenBase64 = null;
            Image nuevaImagen = null;

            byte[] byteBuffer = Convert.FromBase64String(ImgBase64);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);


            memoryStream.Position = 0;


            ImgBitmap = (Bitmap)Bitmap.FromStream(memoryStream);


            memoryStream.Close();

            imagenBase64 = (Image) ImgBitmap;


            Size nuevoTamaño = new Size(300,220);
         
            nuevaImagen = new Bitmap(nuevoTamaño.Width, nuevoTamaño.Height);

            using (Graphics graficos = Graphics.FromImage(nuevaImagen))
            {
                graficos.DrawImage(imagenBase64, new Rectangle(Point.Empty, nuevoTamaño));
            }


          

            ImageConverter converter = new ImageConverter();
            byte[] imagenBytes = (byte[])converter.ConvertTo(nuevaImagen, typeof(byte[]));


            

            return imagenBytes;
        }

        public void cambiarTamanioDeImagenes()
        {
            string puntoDeParada = "";
            try
            { 
                List<Vehiculos> ListaDeVehiculos = Consultar_VehiculosImagenesPrueba();

                foreach(var vehiculo in ListaDeVehiculos)
                {
                    byte[] nuevaImagen = cambiarResolucionBase64(vehiculo.IMAGEN_PRINCIPAL);
                    Editar_ImagenComprimida(vehiculo.ID_VEHICULO, nuevaImagen);
                }


            }catch(Exception ex)
            {
                puntoDeParada = ex.Message;
            }

        }
        public RespuestaServicio Editar_ImagenComprimida(int ID_VEHICULO,byte[] nuevaImagen)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ACTUALIZAR_VEHICULOS_ID_IMAGEN_COMPRIMIDA", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);
                    command.Parameters.AddWithValue("@IMAGEN_COMPRIMIDA", nuevaImagen);
                   



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "se actualizo bien";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro generar imagen";
            }

            return respuestaServicio;
        }

        public List<Vehiculos> Consultar_VehiculosImagenesPrueba()
        {
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_ID_IMAGEN_PRINCIPAL", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehiculos vehiculo = new Vehiculos();
                    vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    byte[] img = (byte[])reader["IMAGEN_PRINCIPAL"];
                    string imgEncoded = Base64Encoder(img);

                    vehiculo.IMAGEN_PRINCIPAL = imgEncoded;

                   

                    listaVehiculos.Add(vehiculo);

                }


            }



            return listaVehiculos;
        }

    }
}