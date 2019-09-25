using Automotriz.connection;
using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Login
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            Session["NOMBRE_USUARIO"] = "";
            Session["IMAGEN"] = "";
            Session["SUCURSAL"] = "";

            return View();
        }



        //Metodo encargado de Verificar si el usuario y contraseña existen
        public string ValidaUsuario(Usuarios usuario)
        {
            Usuarios usuariosAutenticado = new Usuarios();

            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                connection.SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("LOGIN_USUARIO_USUARIOS", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RUT", usuario.RUT);
                    command.Parameters.AddWithValue("@PASSWORD", usuario.PASSWORD);

                    SqlDataReader reader = command.ExecuteReader();

                    bool user = false;
                    bool pass = false;
                    //Si el procedimiento almacenado Obtiene Usuario y contraseña Entrara al While
                    //Si no entra al While user y pass quedaran como false
                    while (reader.Read())
                    {
                        
                        usuariosAutenticado.ID_USUARIOS = Convert.ToInt32(reader["ID_USUARIOS"]);
                        usuariosAutenticado.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        usuariosAutenticado.RUT = reader["RUT"].ToString();
                        usuariosAutenticado.EMAIL = reader["EMAIL"].ToString();
                        usuariosAutenticado.SUCURSAL = reader["SUCURSAL"].ToString();
                        usuariosAutenticado.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"].ToString());
                        usuariosAutenticado.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();
                        usuariosAutenticado.DIRECCION_SUCURSAL = reader["DIRECCION"].ToString();
                        usuariosAutenticado.ID_ROL = Convert.ToInt32(reader["ID_ROL"].ToString());
                        usuariosAutenticado.ROL = reader["ROL"].ToString();





                        if (usuariosAutenticado.TIPO_IMAGEN != "")
                        {


                            string extension = usuariosAutenticado.TIPO_IMAGEN;
                            byte[] img = (byte[])reader["IMAGEN"]; //almacena la imagen de la db en un byte[]



                            string IMG = Base64Encoder(img);

                            usuariosAutenticado.IMAGEN = extension + "," + IMG;

                        }
                        else
                        {
                            usuariosAutenticado.IMAGEN = "/assets/img/avatar_default.jpg";

                        }
                        //Se valida que Exite Usuario y contraseña
                        user = true;
                        pass = true;

                    }

                    //Si el usuario y contraseña existen se guardaran las variables en Session
                    //Caso contrario se mostrara un error
                    if (user == true && pass == true)
                    {

                        Session["ID_USUARIO"] = usuariosAutenticado.ID_USUARIOS;
                        Session["NOMBRE_USUARIO"] = usuariosAutenticado.NOMBRE_USUARIO;
                        Session["IMAGEN"] = usuariosAutenticado.IMAGEN;
                        Session["SUCURSAL"] = usuariosAutenticado.SUCURSAL;
                        Session["ID_SUCURSAL"] = usuariosAutenticado.ID_SUCURSAL;
                        Session["DIRECCION_SUCURSAL"] = usuariosAutenticado.DIRECCION_SUCURSAL;
                        Session["ID_ROL"] = usuariosAutenticado.ID_ROL;
                        Session["ROL"] = usuariosAutenticado.ROL;


                        respuestaServicio.Respuesta = "OK";
                        respuestaServicio.Descripcion = JsonConvert.SerializeObject(usuariosAutenticado, Formatting.Indented);
                        respuestaServicio.Detalle_Error = "";
                    }
                    else if (user == false)
                    {
                        respuestaServicio.Respuesta = "NOK";
                        respuestaServicio.Descripcion = "Usuario y contraseña Incorrectos";
                        respuestaServicio.Detalle_Error = "";
                    }


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "Error" + ex.Message;
                respuestaServicio.Detalle_Error = "";
            }

            string respuesta = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            return respuesta;
        }



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


    }
}