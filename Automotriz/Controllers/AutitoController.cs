using Automotriz.connection;
using Automotriz.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers
{
    public class AutitoController : Controller
    {
        // GET: Autito
        public ActionResult MantenedorAutito()
        {
            return View();
        }
        public static string Base64Encoder(byte[] plainText)//PASA BASE 64 A STR
        {
            string strIMG = System.Convert.ToBase64String(plainText);
            return strIMG;
        }

        public static byte[] Base64Decoder(string base64EncodedData) //PASA BASE64 A BYTES[]
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return base64EncodedBytes;
        }
        public RespuestaServicio GuardarAutito(string ID_MARCA,string ID_SUCURSAL,string ID_COMBUSTIBLE, string DUENIO, string IMAGEN)
        {
          
                RespuestaServicio respuesta = new RespuestaServicio();
                SqlConnection conn = null;
                SQLconn sql = new SQLconn();

                try
                {
                    string[] IMAGENarr = IMAGEN.Split(',');
                    string arreglado = IMAGENarr[1];
                    byte[] img = Base64Decoder(arreglado);
                    conn = sql.AbrirConnection(sql.ObtenerConnection());

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand("INGRESAR_AUTITO", conn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ID_MARCA", ID_MARCA);
                        command.Parameters.AddWithValue("@ID_SUCURSAL", ID_SUCURSAL);
                        command.Parameters.AddWithValue("@ID_COMBUSTIBLE", ID_COMBUSTIBLE);
                        command.Parameters.AddWithValue("@DUENIO", DUENIO);
                        command.Parameters.AddWithValue("@IMAGEN", img);

                        guardarEnCarpeta(img, ID_MARCA);

                        command.ExecuteNonQuery();
                    }

                    sql.CerrarConnection(conn);

                    respuesta.Respuesta = "OK";
                    respuesta.Descripcion = "";
                    respuesta.Detalle_Error = "Se agrego correctamente El autito";
                    return respuesta;
                }
                catch (Exception ex)
                {
                    sql.CerrarConnection(conn);
                    respuesta.Respuesta = "NOK";
                    respuesta.Descripcion = "";
                    respuesta.Detalle_Error = "ups, no se logro agregar el autito"+ex.Message;
                    return respuesta;
                }

        }

        public string guardarEnCarpeta(byte[] imagen, string ID_MARCA)
        {
            string url = Server.MapPath("~/imagenesAutito");

            bool saberExistenciaCarpeta;

            saberExistenciaCarpeta = Directory.Exists(url + "/" + ID_MARCA);
            if (!saberExistenciaCarpeta)
            {
                Directory.CreateDirectory(url + "/" + ID_MARCA);
            }
            else
            {
                Directory.Delete(url + "/" + ID_MARCA, true);
                Directory.CreateDirectory(url + "/" + ID_MARCA);
            }

            using (Image image = Image.FromStream(new MemoryStream(imagen)))
            {
                image.Save(url + "/" + ID_MARCA + "/" + ID_MARCA + ".jpg", ImageFormat.Jpeg);
            }

            return "OK";
        }

    }


}
