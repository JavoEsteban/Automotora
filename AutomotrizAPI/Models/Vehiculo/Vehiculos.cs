using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Automotriz.connection;

namespace AutomotrizAPI.Models
{
    public class Vehiculos
    {
        public int ID_VEHICULO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public string SUCURSAL { get; set; }
        public string SUCURSAL_VENTA { get; set; }
        public int ID_USUARIOS { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public int ID_DISPONIBILIDAD { get; set; }
        public string DISPONIBILIDAD { get; set; }
        public int ID_MARCA { get; set; }
        public string MARCA { get; set; }
        public int ID_TIPOTRACCION { get; set; }
        public int ID_TIPO_CONSIGNACION { get; set; }
        public int TIPO_CONSIGNACION { get; set; }
        public string TIPO_INGRESO { get; set; }
        public int ID_COLOR { get; set; }
        public string COLOR { get; set; }
        public int ID_TIPO_TRANSMICION { get; set; }
        public string TIPO_TRANSMICION { get; set; }
        public int ID_TIPO_VEHICULO { get; set; }
        public string TIPO_VEHICULO { get; set; }
        public int ID_TIPO_COMBUSTIBLE { get; set; }
        public string COMBUSTIBLE { get; set; }
        public int ID_ESTADO { get; set; }
        public string ESTADO { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_CLIENTE { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string PATENTE { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public int DIAS_STOCK { get; set; }
        public string VERSION { get; set; }
        public string MOTOR { get; set; }
        public int ANO { get; set; }
        public string CILINDRADA { get; set; }
        public int PRECIO_VENTA { get; set; }
        public int PRECIO_COMPRA { get; set; }
        public int PRECIO_VENDIDO { get; set; }
        public int PRECIO_CONSIGNACION { get; set; }
        public int PRECIO_MINIMO_VENTA { get; set; }
        public int PRECIO_MINIMO_VENDIDO { get; set; }
        public int KILOMETRAJE { get; set; }
        public string KILOMETRAJE_FORMATEADO { get; set; }
        public string CHASIS { get; set; }
        public int CANTIDAD_DUENIOS { get; set; }
        public int STOCK { get; set; }
        public string NOMBRE_MODELO { get; set; }
        public string NOMBRE_TRANSMISION { get; set; }
        public string IMAGEN_PRINCIPAL { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public string PRECIO_FORMATEADO { get; set; }
        public List<ImagenVehiculo> IMAGENES_VEHICULO { get; set; }
        public List<Models.EquipamientoVehiculo.EquipamientosVehiculo> LISTA_EQUIPAMIENTOS { get; set; }

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

        public string BuscarImagenPrincipalDelVehiculo(int ID_VEHICULO)
        {
            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
            string ImagenPrincipal = "";

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_IMAGEN_PRINCIPAL_POR_ID_VEHICULO", conn);
                command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                        if (reader["TIPO_IMAGEN"].ToString() != "" && reader["TIPO_IMAGEN"] != DBNull.Value)
                        {
                            string tipoImagen = reader["TIPO_IMAGEN"].ToString();
                            byte[] img = (byte[])reader["IMAGEN_PRINCIPAL"];
                            string imgEncoded = Base64Encoder(img);

                        ImagenPrincipal = tipoImagen + "," + imgEncoded;

                        }
                        else
                        {
                        ImagenPrincipal = "/assets/img/default-avatar.png";
                        }      
                }

            }

            return ImagenPrincipal;
        }

        //Busca información del vehiculo, necesaria para mostrar en web larrain
        public List<Vehiculos> Consultar_Vehiculos()
        {
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_WEB_LARRAIN", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.MARCA = reader["MARCA"].ToString();

                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"].ToString());
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.PRECIO_FORMATEADO = vehiculo.PRECIO_VENTA.ToString("N0");
                        vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        vehiculo.KILOMETRAJE_FORMATEADO = vehiculo.KILOMETRAJE.ToString("N0");
                        vehiculo.ANO = Convert.ToInt32(reader["ANO"].ToString());
                        vehiculo.SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                        vehiculo.ID_TIPO_VEHICULO = Convert.ToInt32(reader["ID_TIPO_VEHICULO"]);
                        vehiculo.TIPO_VEHICULO = reader["NOMBRE_TIPO_VEHICULO"].ToString();
                        vehiculo.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"]);
                        vehiculo.MARCA = reader["NOMBRE_MARCA"].ToString();
                        vehiculo.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                        vehiculo.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                        vehiculo.COLOR = reader["NOMBRE_COLOR"].ToString();
                        vehiculo.ID_COLOR = Convert.ToInt32(reader["ID_COLOR"]);
                        vehiculo.ID_TIPO_TRANSMICION = Convert.ToInt32(reader["ID_TIPO_TRANSMICION"]);
                        vehiculo.TIPO_TRANSMICION = reader["NOMBRE_TIPO_TRANSMICION"].ToString();

                        if (reader["TIPO_IMAGEN"].ToString() != "" && reader["TIPO_IMAGEN"] != DBNull.Value)
                        {
                            string tipoImagen = reader["TIPO_IMAGEN"].ToString();
                            byte[] img = (byte[])reader["IMAGEN_COMPRIMIDA"];
                            string imgEncoded = Base64Encoder(img);

                            vehiculo.IMAGEN_PRINCIPAL = tipoImagen + "," + imgEncoded;

                        }
                        else
                        {
                            vehiculo.IMAGEN_PRINCIPAL = "/assets/img/default-avatar.png";
                        }
                        listaVehiculos.Add(vehiculo);
                    }
                    catch (Exception ex) { }
                   

                }

            }

            return listaVehiculos;
        }
        public Vehiculos Consultar_Informacion_vehiculo(int ID_VEHICULO)
        {
            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection()); 
            Vehiculos vehiculo = new Vehiculos();

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_WEB_LARRAIN_POR_ID_VEHICULO", conn);

                command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    vehiculo.MARCA = reader["MARCA"].ToString();

                    vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                    vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"].ToString());
                    vehiculo.PATENTE = reader["PATENTE"].ToString();
                    vehiculo.PRECIO_FORMATEADO = vehiculo.PRECIO_VENTA.ToString("N0");
                    vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                    vehiculo.KILOMETRAJE_FORMATEADO = vehiculo.KILOMETRAJE.ToString("N0");
                    vehiculo.ANO = Convert.ToInt32(reader["ANO"].ToString());
                    vehiculo.SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                    vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();

                    if (reader["TIPO_IMAGEN"].ToString() != "" && reader["TIPO_IMAGEN"] != DBNull.Value)
                    {
                        string tipoImagen = reader["TIPO_IMAGEN"].ToString();
                        byte[] img = (byte[])reader["IMAGEN_PRINCIPAL"];
                        string imgEncoded = Base64Encoder(img);

                        vehiculo.IMAGEN_PRINCIPAL = tipoImagen + "," + imgEncoded;

                    }
                    else
                    {
                        vehiculo.IMAGEN_PRINCIPAL = "/assets/img/default-avatar.png";
                    }
                   
                }

            }

            return vehiculo;
        }

        public List<Vehiculos> Consultar_Vehiculos_Asociados_TipoVehiculo(int ID_TIPO_VEHICULO)
        {
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();

            EquipamientoVehiculo.EquipamientosVehiculo OBJEquipamientoVehiculo = new EquipamientoVehiculo.EquipamientosVehiculo();
            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_WEB_LARRAIN_POR_TIPO_VEHICULO", conn);
                command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", ID_TIPO_VEHICULO);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.MARCA = reader["MARCA"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"].ToString());
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.ANO = Convert.ToInt32(reader["ANO"].ToString());
                        vehiculo.PRECIO_FORMATEADO = vehiculo.PRECIO_VENTA.ToString("N0");
                        if (reader["TIPO_IMAGEN"].ToString() != "" && reader["TIPO_IMAGEN"] != DBNull.Value)
                        {
                            string tipoImagen = reader["TIPO_IMAGEN"].ToString();
                            byte[] img = (byte[])reader["IMAGEN_COMPRIMIDA"];
                            string imgEncoded = Base64Encoder(img);

                            vehiculo.IMAGEN_PRINCIPAL = tipoImagen + "," + imgEncoded;

                        }
                        else
                        {
                            vehiculo.IMAGEN_PRINCIPAL = "/assets/img/default-avatar.png";
                        }
                        vehiculo.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"]);


                        vehiculo.LISTA_EQUIPAMIENTOS =  OBJEquipamientoVehiculo.Consultar_Equipamiento_Vehiculo_Por_Id_Vehiculo(vehiculo.ID_VEHICULO);

                        listaVehiculos.Add(vehiculo);
                    }
                    catch (Exception ex){}
                }
                conn.Close();
              
            }
          

            return listaVehiculos;
        }

        public List<Vehiculos> Consultar_Vehiculos_Estado_Disponibles()
        {
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CON_IMAGEN", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehiculos vehiculo = new Vehiculos();
                    vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];

                    vehiculo.PATENTE = reader["PATENTE"].ToString();
                    vehiculo.MARCA = reader["MARCA"].ToString();
                    vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();

                    if (reader["ANO"] == DBNull.Value)
                    {
                        vehiculo.ANO = 0;
                    }
                    else
                    {
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["ANO"]);
                    }

                    if (reader["PRECIO_VENTA"] == DBNull.Value)
                    {
                        vehiculo.PRECIO_VENTA = 0;
                    }
                    else
                    {
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                    }

                    vehiculo.SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                    vehiculo.IMAGEN_PRINCIPAL = reader["IMAGEN_PRINCIPAL"].ToString();

                    if (reader["ID_TIPO_CONSIGNACION"] == DBNull.Value)
                    {
                        vehiculo.ID_TIPO_CONSIGNACION = 0;
                    }
                    else
                    {
                        vehiculo.ID_TIPO_CONSIGNACION = Convert.ToInt32(reader["ID_TIPO_CONSIGNACION"]);
                    }


                    if (reader["TIPO_IMAGEN"].ToString() != "" && reader["TIPO_IMAGEN"] != DBNull.Value)
                    {
                        string tipoImagen = reader["TIPO_IMAGEN"].ToString();
                        byte[] img = (byte[])reader["IMAGEN_PRINCIPAL"];
                        string imgEncoded = Base64Encoder(img);

                        vehiculo.IMAGEN_PRINCIPAL = tipoImagen + "," + imgEncoded;

                    }
                    else
                    {
                        vehiculo.IMAGEN_PRINCIPAL = "/assets/img/default-avatar.png";
                    }


                    listaVehiculos.Add(vehiculo);

                }


            }



            return listaVehiculos;
        }
       
      
        public Vehiculos BuscarVehiculoID(int idVehiculo)
        {
            Vehiculos nuevoVehiculo = new Vehiculos();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULO_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    nuevoVehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    nuevoVehiculo.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"]);
                    nuevoVehiculo.SUCURSAL = reader["NOMBRE_SUCURSAL"].ToString();
                    nuevoVehiculo.ID_DISPONIBILIDAD = Convert.ToInt32(reader["ID_DISPONIBILIDAD"]);
                    nuevoVehiculo.ID_USUARIOS = Convert.ToInt32(reader["ID_USUARIOS"]);
                    nuevoVehiculo.PATENTE = reader["PATENTE"].ToString();
                    nuevoVehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                    nuevoVehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                    if (reader["KILOMETRAJE"] == DBNull.Value)
                    {
                        nuevoVehiculo.KILOMETRAJE = 0;
                    }
                    else
                    {
                        nuevoVehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                    }

                    if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                    {
                        nuevoVehiculo.CANTIDAD_DUENIOS = 0;
                    }
                    else
                    {
                        nuevoVehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                    }

                    if (reader["ID_MARCA"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_MARCA = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"]);
                    }
                    if (reader["MARCA"] == DBNull.Value)
                    {
                        nuevoVehiculo.MARCA = "";
                    }
                    else
                    {
                        nuevoVehiculo.MARCA = reader["MARCA"].ToString();
                    }

                    if (reader["ID_TIPOTRACCION"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_TIPOTRACCION = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_TIPOTRACCION = Convert.ToInt32(reader["ID_TIPOTRACCION"]);
                    }

                    if (reader["ID_TIPO_CONSIGNACION"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_TIPO_CONSIGNACION = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_TIPO_CONSIGNACION = Convert.ToInt32(reader["ID_TIPO_CONSIGNACION"]);
                    }

                    if (reader["ID_COLOR"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_COLOR = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_COLOR = Convert.ToInt32(reader["ID_COLOR"]);
                    }

                    if (reader["COLOR"] == DBNull.Value)
                    {
                        nuevoVehiculo.COLOR = "";
                    }
                    else
                    {
                        nuevoVehiculo.COLOR = reader["COLOR"].ToString();
                    }

                    if (reader["ID_TIPO_TRANSMICION"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_TIPO_TRANSMICION = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_TIPO_TRANSMICION = Convert.ToInt32(reader["ID_TIPO_TRANSMICION"]);
                    }

                    if (reader["ID_TIPO_COMBUSTIBLE"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_TIPO_COMBUSTIBLE = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_TIPO_COMBUSTIBLE = Convert.ToInt32(reader["ID_TIPO_COMBUSTIBLE"]);
                    }

                    if (reader["ID_ESTADO"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_ESTADO = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_ESTADO = Convert.ToInt32(reader["ID_ESTADO"]);
                    }

                    if (reader["ID_MODELO"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_MODELO = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                    }
                    if (reader["MODELO"] == DBNull.Value)
                    {
                        nuevoVehiculo.NOMBRE_MODELO = "";
                    }
                    else
                    {
                        nuevoVehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                    }
                    if (reader["ID_TIPO_VEHICULO"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_TIPO_VEHICULO = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_TIPO_VEHICULO = Convert.ToInt32(reader["ID_TIPO_VEHICULO"]);
                    }

                    if (reader["VERSION"] == DBNull.Value)
                    {
                        nuevoVehiculo.VERSION = null;

                    }
                    else
                    {
                        nuevoVehiculo.VERSION = reader["VERSION"].ToString();
                    }

                    if (reader["MOTOR"] == DBNull.Value)
                    {
                        nuevoVehiculo.MOTOR = "";
                    }
                    else
                    {
                        nuevoVehiculo.MOTOR = reader["MOTOR"].ToString();
                    }

                    if (reader["ANO"] == DBNull.Value)
                    {
                        nuevoVehiculo.ANO = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                    }

                    if (reader["CILINDRADA"] == DBNull.Value)
                    {
                        nuevoVehiculo.CILINDRADA = null;
                    }
                    else
                    {
                        nuevoVehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                    }

                    if (reader["PRECIO_VENTA"] == DBNull.Value)
                    {
                        nuevoVehiculo.PRECIO_VENTA = 0;
                    }
                    else
                    {
                        nuevoVehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                    }

                    if (reader["PRECIO_COMPRA"] == DBNull.Value)
                    {
                        nuevoVehiculo.PRECIO_COMPRA = 0;
                    }
                    else
                    {
                        nuevoVehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                    }

                    if (reader["PRECIO_CONSIGNACION"] == DBNull.Value)
                    {
                        nuevoVehiculo.PRECIO_CONSIGNACION = 0;
                    }
                    else
                    {
                        nuevoVehiculo.PRECIO_CONSIGNACION = Convert.ToInt32(reader["PRECIO_CONSIGNACION"]);
                    }

                    if (reader["PRECIO_MINIMO_VENTA"] == DBNull.Value)
                    {
                        nuevoVehiculo.PRECIO_MINIMO_VENTA = 0;
                    }
                    else
                    {
                        nuevoVehiculo.PRECIO_MINIMO_VENTA = Convert.ToInt32(reader["PRECIO_MINIMO_VENTA"]);
                    }

                    if (reader["CHASIS"] == DBNull.Value)
                    {
                        nuevoVehiculo.CHASIS = null;
                    }
                    else
                    {
                        nuevoVehiculo.CHASIS = reader["CHASIS"].ToString();
                    }

                    if (reader["TIPO_IMAGEN"] == DBNull.Value)
                    {
                        nuevoVehiculo.TIPO_IMAGEN = null;
                    }
                    else
                    {
                        nuevoVehiculo.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();
                    }

                    if (nuevoVehiculo.TIPO_IMAGEN != "" && nuevoVehiculo.TIPO_IMAGEN != null)
                    {
                        string tipoImagen = nuevoVehiculo.TIPO_IMAGEN;
                        byte[] img = (byte[])reader["IMAGEN_PRINCIPAL"];
                        string IMG = Base64Encoder(img);

                        nuevoVehiculo.IMAGEN_PRINCIPAL = tipoImagen + "," + IMG;

                    }
                    else
                    {
                        nuevoVehiculo.IMAGEN_PRINCIPAL = "/assets/img/default-avatar.png";

                    }

                    if (reader["ID_CLIENTE"] == DBNull.Value)
                    {
                        nuevoVehiculo.ID_CLIENTE = 0;
                    }
                    else
                    {
                        nuevoVehiculo.ID_CLIENTE = Convert.ToInt32(reader["ID_CLIENTE"]);
                    }

                    if (reader["RUT"] == DBNull.Value)
                    {
                        nuevoVehiculo.RUT_CLIENTE = null;
                    }
                    else
                    {
                        nuevoVehiculo.RUT_CLIENTE = reader["RUT"].ToString();
                    }

                }

            }


            return nuevoVehiculo;

        }


        public int ValidaVehiculoPatente(string Patente)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();

            int ExisteVehiculo = 0;

            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("VALIDA_VEHICULO_PATENTE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PATENTE", Patente);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ExisteVehiculo = Int32.Parse(reader["EXISTE_VEHICULO"].ToString());

                    }


                }
            }
            catch (Exception ex)
            {

                return -1;
            }


            return ExisteVehiculo;
        }

        public List<Vehiculos> Consultar_Vehiculos_Dashboard(DateTime fechaFinal)
        { //traer todas los vehiculos para ser presentados en el dashboard

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_STOCK_VEHICULOS", conn);
                    command.Parameters.AddWithValue("@FECHA_FINAL", fechaFinal);

                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();



                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.DIAS_STOCK = Convert.ToInt32(reader["CANTIDAD_DIAS"]);



                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Consignados_Clientes(DateTime fechaFinal)
        { //traer todas los vehiculos para ser presentados en el dashboard

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_STOCK_VEHICULOS_CONSIGNADOS_CLIENTES", conn);
                    command.Parameters.AddWithValue("@FECHA_FINAL", fechaFinal);

                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();



                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.DIAS_STOCK = Convert.ToInt32(reader["CANTIDAD_DIAS"]);



                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Retirados(DateTime fechaFinal)
        { //traer todas los vehiculos para ser presentados en el dashboard

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_STOCK_VEHICULOS_RETIRADOS", conn);


                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FECHA_FINAL", fechaFinal);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();



                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.DIAS_STOCK = Convert.ToInt32(reader["CANTIDAD_DIAS"]);



                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

     

        public List<Vehiculos> Consultar_Vehiculos_Sucursal(int idSucursal)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_SUCURSAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_SUCURSAL", idSucursal);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();

                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Filtro_Sucursal(int idSucursal, int idUsuario, int idIngreso, int idDisponibilidad, int idEstado, string fechaInicial, string fechaFinal)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_FILTRO_SUCURSAL", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_SUCURSAL", idSucursal);


                    if (idUsuario == 0)
                    {
                        command.Parameters.AddWithValue("@ID_USUARIOS", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_USUARIOS", idUsuario);

                    }

                    if (idIngreso == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", idIngreso);

                    }

                    if (idDisponibilidad == 0)
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", idDisponibilidad);


                    }

                    if (idEstado == 0)
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", idEstado);

                    }

                    if (fechaInicial == "")
                    {
                        command.Parameters.AddWithValue("@FECHA_INICIAL", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("FECHA_INICIAL", Convert.ToDateTime(fechaInicial));

                    }

                    if (fechaFinal == "")
                    {
                        command.Parameters.AddWithValue("@FECHA_FINAL", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@FECHA_FINAL", Convert.ToDateTime(fechaFinal));

                    }

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Reportes_Vehiculos(int idSucursal, int idUsuario, int idIngreso, int idDisponibilidad, int idEstado, string fechaInicial, string fechaFinal)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_STOCK_TOTAL", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    if (idSucursal == 0)
                    {
                        command.Parameters.AddWithValue("@ID_SUCURSAL", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_SUCURSAL", idSucursal);
                    }

                    if (idUsuario == 0)
                    {
                        command.Parameters.AddWithValue("@ID_USUARIOS", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_USUARIOS", idUsuario);

                    }

                    if (idIngreso == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", idIngreso);

                    }

                    if (idDisponibilidad == 0)
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", idDisponibilidad);


                    }

                    if (idEstado == 0)
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", idEstado);

                    }

                    if (fechaInicial == "")
                    {
                        command.Parameters.AddWithValue("@FECHA_INICIAL", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("FECHA_INICIAL", Convert.ToDateTime(fechaInicial));

                    }

                    if (fechaFinal == "")
                    {
                        command.Parameters.AddWithValue("@FECHA_FINAL", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@FECHA_FINAL", Convert.ToDateTime(fechaFinal));

                    }

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }



        public List<Vehiculos> Consultar_Vehiculos_Usuario(int idUsuario)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CONSIGNADOS_POR_USUARIO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Por_IDCliente(int idCliente)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_CLIENTE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }


                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Estado(int ID_Estado)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_ESTADO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_ESTADO", ID_Estado);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();


                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Tipo_Consignacion(int ID_TipoConsigna)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_TIPO_CONSIGNACION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", ID_TipoConsigna);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();

                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public Vehiculos Consulta_Vehiculo_Por_Patente(string patente)
        { //traer los datos de la marca consultada
            Vehiculos vehiculo = new Vehiculos();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_PATENTE", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PATENTE", patente);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                    {
                        vehiculo.CANTIDAD_DUENIOS = 0;
                    }

                    else
                    {
                        vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                    }

                    if (reader["CILINDRADA"] == DBNull.Value)
                    {
                        vehiculo.CILINDRADA = "";
                    }

                    else
                    {
                        vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                    }

                    if (reader["VERSION"] == DBNull.Value)
                    {
                        vehiculo.VERSION = "";
                    }

                    else
                    {
                        vehiculo.VERSION = reader["VERSION"].ToString();
                    }

                    if (reader["ANO"] == DBNull.Value)
                    {
                        vehiculo.ANO = 0;
                    }
                    else
                    {
                        vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                    }
                    if (reader["PRECIO_COMPRA"] == DBNull.Value)
                    {
                        vehiculo.PRECIO_COMPRA = 0;
                    }
                    else
                    {
                        vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                    }
                    if (reader["PRECIO_VENTA"] == DBNull.Value)
                    {
                        vehiculo.PRECIO_VENTA = 0;
                    }
                    else
                    {
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                    }

                    if (reader["KILOMETRAJE"] == DBNull.Value)
                    {
                        vehiculo.KILOMETRAJE = 0;
                    }

                    else
                    {
                        vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                    }
                    if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                    {
                        vehiculo.NOMBRE_TRANSMISION = "";
                    }
                    else
                    {
                        vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                    }
                    if (reader["COMBUSTIBLE"] == DBNull.Value)
                    {
                        vehiculo.COMBUSTIBLE = "";
                    }

                    else
                    {
                        vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                    }
                    if (reader["ESTADO"] == DBNull.Value)
                    {
                        vehiculo.ESTADO = "Aún no definido";
                    }
                    else
                    {
                        vehiculo.ESTADO = reader["ESTADO"].ToString();
                    }
                    if (reader["COLOR"] == DBNull.Value)
                    {
                        vehiculo.COLOR = "";
                    }
                    else
                    {
                        vehiculo.COLOR = reader["COLOR"].ToString();
                    }

                    if (reader["MARCA"] == DBNull.Value)
                    {
                        vehiculo.MARCA = "";
                    }
                    else
                    {
                        vehiculo.MARCA = reader["MARCA"].ToString();
                    }

                    vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                    vehiculo.PATENTE = reader["PATENTE"].ToString();
                    vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                    vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                    vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                    vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                }


            }

            return vehiculo;
        }

        public List<Vehiculos> Consultar_Vehiculos_Por_FechaIngreso(DateTime FechaInicial, DateTime FechaFinal)
        { //traer todas los vehiculos filtrado por fecha ingreso

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_FECHA", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FECHA_INICIAL", FechaInicial);
                    command.Parameters.AddWithValue("@FECHA_FINAL", FechaFinal);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();

                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Por_Disponibilidad(int ID_disponibilidad)
        { //traer todas los vehiculos filtrado por fecha ingreso

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_POR_DISPONIBILIDAD", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", ID_disponibilidad);



                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();

                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }

                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();

                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }

        public List<Vehiculos> Consultar_Vehiculos_Vendidos()
        { //traer todas los vehiculos filtrado por fecha ingreso

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();



            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_VEHICULOS_VENDIDOS", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;




                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Vehiculos vehiculo = new Vehiculos();
                    try
                    {
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_DOCUMENTO"];
                        vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"].ToString());
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"].ToString());
                        vehiculo.PRECIO_VENDIDO = Convert.ToInt32(reader["PRECIO_VENDIDO"].ToString());
                        vehiculo.SUCURSAL = reader["SUCURSAL_INGRESO"].ToString();
                        vehiculo.SUCURSAL_VENTA = reader["SUCURSAL_VENTA"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_VENDEDOR"].ToString();
                        vehiculo.NOMBRE_CLIENTE = reader["NOMBRE_COMPRADOR"].ToString();
                        listaVehiculos.Add(vehiculo);
                    }
                    catch (Exception) { }

                }


            }
            sql.CerrarConnection(conn);


            return listaVehiculos;
        }

        public List<Vehiculos> Consultar_Vehiculos_Vendidos_Por_Usuario(int idUsuarioVendedor)
        { //traer todas los vehiculos filtrado por fecha ingreso

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();



            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_VEHICULOS_VENDIDOS_POR_USUARIO_VENDEDOR", conn);
                command.Parameters.AddWithValue("@ID_VENDEDOR", idUsuarioVendedor);
                command.CommandType = System.Data.CommandType.StoredProcedure;




                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Vehiculos vehiculo = new Vehiculos();
                    try
                    {
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_DOCUMENTO"];
                        vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"].ToString());
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"].ToString());
                        vehiculo.PRECIO_VENDIDO = Convert.ToInt32(reader["PRECIO_VENDIDO"].ToString());
                        vehiculo.SUCURSAL = reader["SUCURSAL_INGRESO"].ToString();
                        vehiculo.SUCURSAL_VENTA = reader["SUCURSAL_VENTA"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_VENDEDOR"].ToString();
                        vehiculo.NOMBRE_CLIENTE = reader["NOMBRE_COMPRADOR"].ToString();
                        listaVehiculos.Add(vehiculo);
                    }
                    catch (Exception) { }

                }


            }
            sql.CerrarConnection(conn);


            return listaVehiculos;
        }




        //Metodo encargado de consultar la cantidad de vehiculos totales (sin importar sucursal)
        public int Consultar_Stock_Total_De_Vehiculos()
        {
            int cantidadVehiculo = 0;

            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_CANTIDAD_VEHICULOS_EXISTENTES", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cantidadVehiculo = Convert.ToInt32(reader["STOCK"]);

                    }

                    sql.CerrarConnection(conn);

                }

                sql.CerrarConnection(conn);

            }
            catch (Exception ex)
            {

                return -1; // si retorna -1 es por que se cayó la consulta
            }


            return cantidadVehiculo;
        }

        //Metodo encargado de consultar la cantidad de vehiculos totales de una sucursal
        public int Consultar_Stock_Total_De_Vehiculos_Por_Sucursal(int idSucursal)
        {
            int cantidadVehiculo = 0;

            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_CANTIDAD_VEHICULOS_SUCURSAL", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_SUCURSAL", idSucursal);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cantidadVehiculo = Convert.ToInt32(reader["STOCK"]);

                    }

                    sql.CerrarConnection(conn);

                }

                sql.CerrarConnection(conn);

            }
            catch (Exception ex)
            {

                return -1; // si retorna -1 es por que se cayó la consulta
            }


            return cantidadVehiculo;
        }

        //Metodo encargado de buscar la cantidad de vehiculos propios total (no por sucursal)
        public int Consultar_Cantidad_Vehiculos_Por_Tipo_Consignacion(int id_Consignacion)
        {

            int cantidadVehiculos = 0;
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_CANTIDAD_VEHICULOS_CONSIGNACION", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", id_Consignacion);



                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {


                        cantidadVehiculos = Convert.ToInt32(reader["CONSIGNADOS"]);


                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "se encontró " + cantidadVehiculos + " vehículos";
                respuestaServicio.Detalle_Error = "";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return cantidadVehiculos;

        }

        public List<Vehiculos> Consultar_Reportes_Vehiculos_Cliente(int idCliente, int idIngreso, int idDisponibilidad, int idEstado)
        { //traer todas los vehiculos filtrado por sucursal

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_STOCK_VEHICULOS_CLIENTE", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    if (idCliente == 0)
                    {
                        command.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    }


                    if (idIngreso == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", idIngreso);

                    }

                    if (idDisponibilidad == 0)
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", idDisponibilidad);


                    }

                    if (idEstado == 0)
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", DBNull.Value);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", idEstado);

                    }



                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();
                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.RUT_CLIENTE = reader["CLIENTE"].ToString();


                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }



        public List<Vehiculos> Consultar_Vehiculos_Consignados(DateTime fechaActual)
        { //traer todas los vehiculos para ser presentados en el dashboard

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CONSIGNADOS", conn);


                    command.Parameters.AddWithValue("@FECHA_FINAL", fechaActual);
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();



                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.DIAS_STOCK = Convert.ToInt32(reader["CANTIDAD_DIAS"]);



                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }
        public List<Vehiculos> Consultar_Vehiculos_Consignados_Por_Usuario(int ID_USUARIO)
        { //traer todas los vehiculos para ser presentados en el dashboard

            List<Vehiculos> listaVehiculos = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {


                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CONSIGNADOS_POR_USUARIO", conn);

                    command.Parameters.AddWithValue("@ID_USUARIO", ID_USUARIO);

                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculos vehiculo = new Vehiculos();



                        if (reader["CANTIDAD_DUENIOS"] == DBNull.Value)
                        {
                            vehiculo.CANTIDAD_DUENIOS = 0;
                        }

                        else
                        {
                            vehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["CANTIDAD_DUENIOS"]);
                        }

                        if (reader["CILINDRADA"] == DBNull.Value)
                        {
                            vehiculo.CILINDRADA = "";
                        }

                        else
                        {
                            vehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
                        }

                        if (reader["VERSION"] == DBNull.Value)
                        {
                            vehiculo.VERSION = "";
                        }

                        else
                        {
                            vehiculo.VERSION = reader["VERSION"].ToString();
                        }

                        if (reader["ANO"] == DBNull.Value)
                        {
                            vehiculo.ANO = 0;
                        }
                        else
                        {
                            vehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                        }
                        if (reader["PRECIO_COMPRA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_COMPRA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                        }
                        if (reader["PRECIO_VENTA"] == DBNull.Value)
                        {
                            vehiculo.PRECIO_VENTA = 0;
                        }
                        else
                        {
                            vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                        }

                        if (reader["KILOMETRAJE"] == DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"] == DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"] == DBNull.Value)
                        {
                            vehiculo.COMBUSTIBLE = "";
                        }

                        else
                        {
                            vehiculo.COMBUSTIBLE = reader["COMBUSTIBLE"].ToString();
                        }
                        if (reader["ESTADO"] == DBNull.Value)
                        {
                            vehiculo.ESTADO = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.ESTADO = reader["ESTADO"].ToString();
                        }
                        if (reader["COLOR"] == DBNull.Value)
                        {
                            vehiculo.COLOR = "";
                        }
                        else
                        {
                            vehiculo.COLOR = reader["COLOR"].ToString();
                        }

                        if (reader["MARCA"] == DBNull.Value)
                        {
                            vehiculo.MARCA = "";
                        }
                        else
                        {
                            vehiculo.MARCA = reader["MARCA"].ToString();
                        }
                        if (reader["DISPONIBILIDAD"] == DBNull.Value)
                        {
                            vehiculo.DISPONIBILIDAD = "Aún no definido";
                        }
                        else
                        {
                            vehiculo.DISPONIBILIDAD = reader["DISPONIBILIDAD"].ToString();
                        }

                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        vehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                        vehiculo.PATENTE = reader["PATENTE"].ToString();
                        vehiculo.SUCURSAL = reader["SUCURSAL"].ToString();
                        vehiculo.NOMBRE_MODELO = reader["MODELO"].ToString();
                        vehiculo.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                        vehiculo.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();
                        vehiculo.DIAS_STOCK = Convert.ToInt32(reader["CANTIDAD_DIAS"]);



                        listaVehiculos.Add(vehiculo);

                    }


                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se encontro el vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se encontro " + ex.Message;

            }

            return listaVehiculos;

        }



        public string EliminarImagenesVehiculo(int idVehiculo)
        {

            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {
                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_IMAGENES_VEHICULO", conn);
                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                    }

                }
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se agrego correctamente El autito";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se logro agregar el autito" + ex.Message;

            }
            string respuesta = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            return respuesta;
        }


    }

}