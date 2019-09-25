using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Usuarios
    {
        public int ID_USUARIOS { get; set; }
        public int ID_ROL { get; set; }
        public string ROL { get; set; }

        public string DESCRIPCION { get; set; }
        public int ID_SUCURSAL { get; set; }
        public string SUCURSAL { get; set; }
        public string DIRECCION_SUCURSAL { get; set; }
        public string RUT { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string IMAGEN { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public string EXTENSION { get; set; }
        public int VIGENCIA { get; set; }

        public int TOTAL_VENDIDO { get; set; }
        public string MES_VENTA { get; set; }

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

        public RespuestaServicio AgregarUsuarios(Usuarios usuarios) //Agrega un usuario a la tabla usuarios
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {
               


                string imagenCompleta = usuarios.IMAGEN;
                string[] IMAGENarr = imagenCompleta.Split(',');
                string imagenSinExtension = IMAGENarr[1];
                usuarios.TIPO_IMAGEN = IMAGENarr[0];



                byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_USUARIO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_ROL", usuarios.ID_ROL);
                    command.Parameters.AddWithValue("@ID_SUCURSAL", usuarios.ID_SUCURSAL);
                    command.Parameters.AddWithValue("@RUT", usuarios.RUT);
                    command.Parameters.AddWithValue("@PASSWORD", usuarios.PASSWORD);
                    command.Parameters.AddWithValue("@EMAIL", usuarios.EMAIL.ToLower());
                    command.Parameters.AddWithValue("@NOMBRE_USUARIO", usuarios.NOMBRE_USUARIO.ToUpper());
                    command.Parameters.AddWithValue("@IMAGEN", imgBaseDatos);
                    command.Parameters.AddWithValue("@TIPO_IMAGEN", usuarios.TIPO_IMAGEN);
                    command.Parameters.AddWithValue("@EXTENSION", usuarios.EXTENSION);
                    command.Parameters.AddWithValue("@VIGENCIA", usuarios.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el usuario";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = ex.Message;
                return respuesta;
            }
        }

        





        public List<Usuarios> ConsultarUsuarios() //traer todos los usuarios de la tabla usuarios
        { 
            List<Usuarios> listaUsuarios = new List<Usuarios>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_USUARIOS", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuarios usuario = new Usuarios();
                    usuario.ID_USUARIOS = Convert.ToInt32(reader["ID_USUARIOS"].ToString());
                    usuario.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    usuario.SUCURSAL = reader["SUCURSAL"].ToString();
                    usuario.RUT = reader["RUT"].ToString();
                    usuario.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                    usuario.PASSWORD = reader["PASSWORD"].ToString();
                    usuario.EMAIL = reader["EMAIL"].ToString();
                    usuario.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"]);


                    listaUsuarios.Add(usuario);

                }


            }

            return listaUsuarios;
        }

        public List<Usuarios> ConsultarUsuariosVendedores() //traer todos los usuarios de la tabla usuarios
        {
            List<Usuarios> listaUsuarios = new List<Usuarios>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_USUARIOS_VENDEDORES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuarios usuario = new Usuarios();
                    usuario.ID_USUARIOS = Convert.ToInt32(reader["ID_USUARIOS"].ToString());
                    usuario.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    usuario.SUCURSAL = reader["SUCURSAL"].ToString();
                    usuario.RUT = reader["RUT"].ToString();
                    usuario.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                    usuario.PASSWORD = reader["PASSWORD"].ToString();
                    usuario.EMAIL = reader["EMAIL"].ToString();
                    usuario.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"]);


                    listaUsuarios.Add(usuario);

                }


            }

            return listaUsuarios;
        }

        public Usuarios Consulta_Usuario_Por_Id(string idUsuario)
        { //traer los datos de un usuario en base a su ID
            Usuarios usuarioConsultado = new Usuarios();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_USUARIO_POR_ID ", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_USUARIOS", idUsuario);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuarioConsultado.ID_USUARIOS = Convert.ToInt32(reader["ID_USUARIOS"].ToString());
                    usuarioConsultado.ID_ROL = Convert.ToInt32(reader["ID_ROL"].ToString());
                    usuarioConsultado.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"].ToString());
                    usuarioConsultado.RUT = reader["RUT"].ToString();
                    usuarioConsultado.PASSWORD = reader["PASSWORD"].ToString();
                    usuarioConsultado.EMAIL = reader["EMAIL"].ToString();
                    usuarioConsultado.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                    usuarioConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                    
                    usuarioConsultado.EXTENSION = reader["EXTENSION"].ToString();
                    usuarioConsultado.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();


                    if (usuarioConsultado.TIPO_IMAGEN != "")
                    {


                        string tipoImagen = usuarioConsultado.TIPO_IMAGEN;
                        byte[] img = (byte[])reader["IMAGEN"];
                        string IMG = Base64Encoder(img);

                        usuarioConsultado.IMAGEN = tipoImagen + "," + IMG;

                    }
                    else
                    {
                        usuarioConsultado.IMAGEN = "/assets/img/default-avatar.png";

                    }


                }


            }

            return usuarioConsultado;
        }

        public RespuestaServicio Editar_Usuario(Usuarios usuarios)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                string imagenCompleta = usuarios.IMAGEN;
                string[] IMAGENarr = imagenCompleta.Split(',');
                string imagenSinExtension = IMAGENarr[1];
                usuarios.TIPO_IMAGEN = IMAGENarr[0];
                byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                if (usuarios.EXTENSION == null) {
                    string [] manejoExtension = usuarios.TIPO_IMAGEN.Split(';');
                    string[] elimninarBasuraExtension = manejoExtension[0].Split('/');
                    string extensionLimpia = elimninarBasuraExtension[1];
                    usuarios.EXTENSION = extensionLimpia;
                }

                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_USUARIO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_USUARIOS", usuarios.ID_USUARIOS);
                    command.Parameters.AddWithValue("@ID_ROL", usuarios.ID_ROL);
                    command.Parameters.AddWithValue("@ID_SUCURSAL", usuarios.ID_SUCURSAL);
                    command.Parameters.AddWithValue("@RUT", usuarios.RUT);
                    command.Parameters.AddWithValue("@NOMBRE_USUARIO", usuarios.NOMBRE_USUARIO.ToUpper());
                    command.Parameters.AddWithValue("@PASSWORD", usuarios.PASSWORD);
                    command.Parameters.AddWithValue("@EMAIL", usuarios.EMAIL.ToLower());
                    command.Parameters.AddWithValue("@IMAGEN", imgBaseDatos);
                    command.Parameters.AddWithValue("@TIPO_IMAGEN", usuarios.TIPO_IMAGEN);
                    command.Parameters.AddWithValue("@EXTENSION", usuarios.EXTENSION);
                    command.Parameters.AddWithValue("@VIGENCIA", usuarios.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el usuario satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar el Usuario"+ ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarUsuario(Usuarios usuario)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_USUARIO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_USUARIOS", usuario.ID_USUARIOS);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se elimino el usuario satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el usuario seleccionado";
            }

            return respuestaServicio;
        }

        public RespuestaServicio ValidarRutUsuario(string rut)
        {


            RespuestaServicio respuesta = new RespuestaServicio();
            SQLconn sql = new SQLconn();
            int rutsEncontrados = 0;

            try
            {

                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTA_EXISTENCIA_RUT_USUARIO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RUT", rut);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        rutsEncontrados = Convert.ToInt32(reader["ENCONTRADOS"].ToString());
                    }

                    if (rutsEncontrados == 0)
                    {
                        respuesta.Respuesta = "OK";
                        respuesta.Detalle_Error = "No se encontro ningun rut que se paresca";
                        return respuesta;
                    }
                    else
                    {
                        respuesta.Respuesta = "NOK";
                        respuesta.Detalle_Error = "El rut ya existe";
                        return respuesta;
                    }

                }
                else
                {
                    respuesta.Respuesta = "NOK";
                    respuesta.Detalle_Error = "No se logro establecer comunicacion con el servidor al validar el rut";
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                respuesta.Respuesta = "NOK";
                respuesta.Detalle_Error = "No se logro validar el rut del cliente,Error: " + ex.Message;
                return respuesta;
            }


        }

        public List<Usuarios> ConsultaTop3Vendedores()
        {
            List<Usuarios> listaUsuarios = new List<Usuarios>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TOP3_MEJORES_VENDEDORES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuarios usuario = new Usuarios();
                    usuario.ID_USUARIOS = Int32.Parse(reader["ID_USUARIOS"].ToString());
                    usuario.NOMBRE_USUARIO = reader["NOMBRE_VENDEDOR"].ToString();
                    usuario.TOTAL_VENDIDO = Int32.Parse(reader["TOTAL_VENDIDO"].ToString());


                    listaUsuarios.Add(usuario);

                }


            }

            return listaUsuarios;
        }

    }

   
}