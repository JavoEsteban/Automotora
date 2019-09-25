using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Sucursal
    {   public int ID_SUCURSAL { get; set; }
        public string NOMBRE_SUCURSAL { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string IMAGEN { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public string RUT_SUCURSAL { get; set; }
        public string EXTENSION { get; set; }
        public int VIGENCIA { get; set; }
        public List<NotaVenta> MesesDeVenta { get; set; }

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



        public List<Sucursal> Consultar_sucursales_preview()
        { //trae un preview de los datos de la tabla sucursal
            List<Sucursal> listaSucursales = new List<Sucursal>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_SUCURSALES_PREVIEW", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"].ToString());
                    sucursal.NOMBRE_SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                    sucursal.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaSucursales.Add(sucursal);

                }


            }

            return listaSucursales;
        }

        public List<Sucursal> Consultar_Sucursales()
        { //trae las sucursales de la tabla sucursal
            List<Sucursal> listaSucursales = new List<Sucursal>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_SUCURSALES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"].ToString());
                    sucursal.NOMBRE_SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                    sucursal.DIRECCION = reader["DIRECCION"].ToString();
                    sucursal.TELEFONO = reader["TELEFONO"].ToString();
                    sucursal.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaSucursales.Add(sucursal);

                }


            }

            return listaSucursales;
        }

        public RespuestaServicio AgregarSucursales(Sucursal sucursal) //Agrega una sucursal a la tabla sucursales
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {
 

                string imagenCompleta = sucursal.IMAGEN;
                string[] IMAGENarr = imagenCompleta.Split(',');
                string imagenSinExtension = IMAGENarr[1];
                sucursal.TIPO_IMAGEN = IMAGENarr[0];

                byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_SUCURSAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NOMBRE_SUCURSAL", sucursal.NOMBRE_SUCURSAL.ToUpper());
                    command.Parameters.AddWithValue("@DIRECCION", sucursal.DIRECCION.ToUpper());
                    command.Parameters.AddWithValue("@TELEFONO", sucursal.TELEFONO);
                    command.Parameters.AddWithValue("@IMAGEN", imgBaseDatos);
                    command.Parameters.AddWithValue("@TIPO_IMAGEN", sucursal.TIPO_IMAGEN);
                    command.Parameters.AddWithValue("@EXTENSION", sucursal.EXTENSION);
                    command.Parameters.AddWithValue("@VIGENCIA", sucursal.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente La sucursal";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Oops, fue posible agregar la sucursal "+ ex.Message;
                return respuesta;
            }
        }

        public Sucursal Consulta_Sucursal_Por_Id(int idSucursal)
        { //traer los datos de un usuario en base a su ID
            Sucursal sucursalConsultada = new Sucursal();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_SUCURSAL_POR_ID ", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_SUCURSAL", idSucursal);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sucursalConsultada.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"].ToString());
                    sucursalConsultada.NOMBRE_SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                    sucursalConsultada.DIRECCION = reader["DIRECCION"].ToString();
                    sucursalConsultada.TELEFONO = reader["TELEFONO"].ToString();
                    sucursalConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                    sucursalConsultada.EXTENSION = reader["EXTENSION"].ToString();
                    sucursalConsultada.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();
                    sucursalConsultada.RUT_SUCURSAL = reader["RUT_SUCURSAL"].ToString();

                    if (sucursalConsultada.TIPO_IMAGEN != "")
                    {


                        string tipoImagen = sucursalConsultada.TIPO_IMAGEN;
                        byte[] img = (byte[])reader["IMAGEN"];
                        string IMG = Base64Encoder(img);

                        sucursalConsultada.IMAGEN = tipoImagen + "," + IMG;

                    }
                    else
                    {
                        sucursalConsultada.IMAGEN = "/assets/img/avatar_default.jpg";

                    }
                }


            }

            return sucursalConsultada;
        }

        public RespuestaServicio Editar_Sucursal(Sucursal sucursal)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                string imagenCompleta = sucursal.IMAGEN;
                string[] IMAGENarr = imagenCompleta.Split(',');
                string imagenSinExtension = IMAGENarr[1];
                sucursal.TIPO_IMAGEN = IMAGENarr[0];
                byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                if (sucursal.EXTENSION == null)
                {
                    string[] manejoExtension = sucursal.TIPO_IMAGEN.Split(';');
                    string[] elimninarBasuraExtension = manejoExtension[0].Split('/');
                    string extensionLimpia = elimninarBasuraExtension[1];
                    sucursal.EXTENSION = extensionLimpia;
                }

                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_SUCURSAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_SUCURSAL", sucursal.ID_SUCURSAL);
                    command.Parameters.AddWithValue("@NOMBRE_SUCURSAL", sucursal.NOMBRE_SUCURSAL.ToUpper());
                    command.Parameters.AddWithValue("@DIRECCION", sucursal.DIRECCION.ToUpper());
                    command.Parameters.AddWithValue("@TELEFONO", sucursal.TELEFONO);
                    command.Parameters.AddWithValue("@IMAGEN", imgBaseDatos);
                    command.Parameters.AddWithValue("@TIPO_IMAGEN", sucursal.TIPO_IMAGEN);
                    command.Parameters.AddWithValue("@EXTENSION", sucursal.EXTENSION);
                    command.Parameters.AddWithValue("@VIGENCIA", sucursal.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el usuario satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar el Usuario" + ex.Message;
            }

            return respuestaServicio;
        }


        public RespuestaServicio EliminarSucursal(Sucursal sucursal)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_SUCURSAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_SUCURSAL", sucursal.ID_SUCURSAL);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se elimino la Sucursal satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar la Sucursal seleccionada";
            }

            return respuestaServicio;
        }




    }




}