using Automotriz.connection;
using Newtonsoft.Json;
using Automotriz.Models;
using Automotriz.Models.EquipamientoVehiculo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Automotriz.Models
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
        public string APELLIDO_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string COMUNA_CLIENTE { get; set; }
        public string CIUDAD_CLIENTE { get; set; }
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
        public string CHASIS { get; set; }
        public int CANTIDAD_DUENIOS { get; set; }
        public int STOCK { get; set; }
        public string NOMBRE_MODELO { get; set; }
        public string NOMBRE_TRANSMISION { get; set; }
        public string IMAGEN_PRINCIPAL { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public List<ImagenVehiculo> IMAGENES_VEHICULO { get; set; }
        public bool PUBLICADO {get;set;}

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



        //Busca información del vehiculo, necesaria para mostrar
        public List<Vehiculos> Consultar_Vehiculos()
        { 
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CON_IMAGEN_PRINCIPAL_COMPRIMIDA", conn);

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
                    vehiculo.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"].ToString());

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


                    vehiculo.PUBLICADO = Convert.ToBoolean(reader["PUBLICADO"]);
                    vehiculo.ID_DISPONIBILIDAD = Convert.ToInt32(reader["ID_DISPONIBILIDAD"]);


                    listaVehiculos.Add(vehiculo);

                }


            }



            return listaVehiculos;
        }
        public List<Vehiculos> Consultar_Vehiculos_Por_Sucursal(int ID_VEHICULO)
        {
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CON_IMAGEN_POR_SUCURSAL", conn);

                command.Parameters.AddWithValue("@ID_SUCURSAL", ID_VEHICULO);
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
        public string IngresarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes,string NombreUsuario, int IdUsuario, List<EquipamientosVehiculo> ListaDeEquipamientosDeVehiculo)
        {

            RespuestaServicio respuestaServicio = new RespuestaServicio();
            EquipamientosVehiculo OBJEquipamientoVehiculo = new EquipamientosVehiculo();
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            Models.Parametro_Generico.Parametro_Genericos OBJParametros = new Parametro_Generico.Parametro_Genericos();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            Correo correoNotificacion = new Correo();
            bool envioCorreo = true;
            int ID_AUTO_INGRESADO = 0;
            string nombreUsuario = NombreUsuario;
            int idUsuario = IdUsuario;


            //El vehiculo ingresado de ingresa con estado 7 (INGRESADO)
            vehiculo.ID_DISPONIBILIDAD = 7;
            vehiculo.DISPONIBILIDAD = "INGRESADO";

            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("INSERTAR_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_USUARIOS", vehiculo.ID_USUARIOS);
                    command.Parameters.AddWithValue("@PATENTE", vehiculo.PATENTE);
                    command.Parameters.AddWithValue("@FECHA_INGRESO", vehiculo.FECHA_INGRESO);

                    if (vehiculo.ID_SUCURSAL == 0)
                    {
                        command.Parameters.AddWithValue("@ID_SUCURSAL", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_SUCURSAL", vehiculo.ID_SUCURSAL);
                    }

                    if (vehiculo.ID_DISPONIBILIDAD == 0)
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", vehiculo.ID_DISPONIBILIDAD);
                    }

                    if (vehiculo.ID_MARCA == 0)
                    {
                        command.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_MARCA", vehiculo.ID_MARCA);
                    }

                    if (vehiculo.ID_TIPOTRACCION == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPOTRACCION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPOTRACCION", vehiculo.ID_TIPOTRACCION);
                    }

                    if (vehiculo.ID_TIPO_CONSIGNACION == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", vehiculo.ID_TIPO_CONSIGNACION);
                    }

                    if (vehiculo.ID_COLOR == 0)
                    {
                        command.Parameters.AddWithValue("@ID_COLOR", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_COLOR", vehiculo.ID_COLOR);
                    }

                    if (vehiculo.ID_TIPO_TRANSMICION == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", vehiculo.ID_TIPO_TRANSMICION);
                    }

                    if (vehiculo.ID_TIPO_VEHICULO == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", vehiculo.ID_TIPO_VEHICULO);
                    }

                    if (vehiculo.ID_TIPO_COMBUSTIBLE == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", vehiculo.ID_TIPO_COMBUSTIBLE);
                    }

                    if (vehiculo.ID_ESTADO == 0)
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", vehiculo.ID_ESTADO);
                    }

                    if (vehiculo.ID_MODELO == 0)
                    {
                        command.Parameters.AddWithValue("@ID_MODELO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_MODELO", vehiculo.ID_MODELO);
                    }


                    if (vehiculo.VERSION == null)
                    {
                        command.Parameters.AddWithValue("@VERSION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@VERSION", vehiculo.VERSION);
                    }

                    if (vehiculo.MOTOR == null)
                    {
                        command.Parameters.AddWithValue("@MOTOR", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@MOTOR", vehiculo.MOTOR);
                    }

                    if (vehiculo.CHASIS == null)
                    {
                        command.Parameters.AddWithValue("@CHASIS", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CHASIS", vehiculo.CHASIS);
                    }

                    if (vehiculo.CILINDRADA == null)
                    {
                        command.Parameters.AddWithValue("@CILINDRADA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CILINDRADA", vehiculo.CILINDRADA);
                    }

                    if (vehiculo.ANO == 0)
                    {
                        command.Parameters.AddWithValue("@ANO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ANO", vehiculo.ANO);
                    }

                    
                    command.Parameters.AddWithValue("@PRECIO_VENTA", vehiculo.PRECIO_VENTA);
                    
                    command.Parameters.AddWithValue("@PRECIO_COMPRA", vehiculo.PRECIO_COMPRA);
                    

                    if (vehiculo.PRECIO_CONSIGNACION == 0)
                    {
                        command.Parameters.AddWithValue("@PRECIO_CONSIGNACION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PRECIO_CONSIGNACION", vehiculo.PRECIO_CONSIGNACION);
                    }

                    if (vehiculo.PRECIO_MINIMO_VENTA == 0)
                    {
                        command.Parameters.AddWithValue("@PRECIO_MINIMO_VENTA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PRECIO_MINIMO_VENTA", vehiculo.PRECIO_MINIMO_VENTA);
                    }

                    if (vehiculo.ID_CLIENTE == 0)
                    {
                        command.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_CLIENTE", vehiculo.ID_CLIENTE);
                    }



                    command.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.KILOMETRAJE);




                    command.Parameters.AddWithValue("@CANTIDAD_DUENIOS", vehiculo.CANTIDAD_DUENIOS);

                    //--------------------------------------------------------------------------

                    if (vehiculo.IMAGEN_PRINCIPAL != null)
                    {
                        string imagenCompleta = vehiculo.IMAGEN_PRINCIPAL;
                        string[] arrImagen = imagenCompleta.Split(',');
                        string imagenSinExtension = arrImagen[1];


                        byte[] imagenReducida = OBJParametros.cambiarResolucionBase64(imagenSinExtension);

                        byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                        command.Parameters.AddWithValue("@IMAGEN_COMPRIMIDA", imagenReducida);


                        command.Parameters.AddWithValue("@IMAGEN_PRINCIPAL", imgBaseDatos);
                        command.Parameters.AddWithValue("@TIPO_IMAGEN", vehiculo.TIPO_IMAGEN);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@IMAGEN_PRINCIPAL", DBNull.Value);
                        command.Parameters.AddWithValue("@TIPO_IMAGEN", DBNull.Value);
                    }



                    //se ejecuta el metodo y toma la respuesta 
                    SqlDataReader reader = command.ExecuteReader();

                    //lee la respuesta que envia 
                    while (reader.Read())
                    {

                        //tomo el valor de de la bd (ve el procedimiento almacenado INSERTAR_VEHICULO )
                        ID_AUTO_INGRESADO = Int32.Parse(reader["ID_AUTO"].ToString());

                        OBJbitacora.ID_VEHICULO = ID_AUTO_INGRESADO;
                        OBJbitacora.ESTADO = vehiculo.DISPONIBILIDAD;
                        OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", Ha agregado un nuevo Vehículo Como " + vehiculo.TIPO_INGRESO;
                        OBJbitacora.TIPO = 1;
                        OBJbitacora.ID_USUARIO = idUsuario;


                        OBJbitacora.AgregarInformacionBitacora(OBJbitacora);
                    }

                    //Se encarga de eliminar el equipamiento del vehiculo
                    OBJEquipamientoVehiculo.Eliminar_Equipamiento_Vehiculo(ID_AUTO_INGRESADO);
                    
                    foreach (var categorias in ListaDeEquipamientosDeVehiculo)
                    {
                        //Encargado de agregar equipamiento del vehiculo
                        OBJEquipamientoVehiculo.Agregar_Equipamiento_Vehiculo(categorias.ID_EQUIPAMIENTO, ID_AUTO_INGRESADO);
                    }

                    //verificio que la lista de imagenes no venga vacia (si no viene vacia ingresara imagenes)
                    if (imagenes != null)
                    {
                        if (imagenes.Count > 0)
                        {
                            int ordenImagenes = 1;
                            foreach (var imagen in imagenes)
                            {
                                imagen.ID_VEHICULO = ID_AUTO_INGRESADO;
                                InsertarImagenVehiculo(imagen, ID_AUTO_INGRESADO, ordenImagenes);
                                ordenImagenes++;
                            }
                        }

                    }

                    if (envioCorreo == true)
                    {


                        string destinatario = "jbecerra@automotrizlarrain.cl";
                        string asunto = " NUEVO VEHÍCULO INGRESADO";
                        envioCorreo = correoNotificacion.EnviarMail(destinatario, asunto, vehiculo);
                    }


                    sql.CerrarConnection(conn);


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = ID_AUTO_INGRESADO.ToString();
                    respuestaServicio.Detalle_Error = "";

                }
            }
            catch (Exception ex)
            {
                envioCorreo = false;
                sql.CerrarConnection(conn);

                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = ex.Message;
            }
            string respuesta = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            return respuesta;
        }

        public string InsertarImagenVehiculo(ImagenVehiculo imagen, int idVehiculo, int orden)
        {
            List<ImagenVehiculo> listaImagen = new List<ImagenVehiculo>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            try
            {
                string imagenCompleta = imagen.IMAGEN;
                string[] IMAGENarr = imagenCompleta.Split(',');
                string imagenSinExtension = IMAGENarr[1];
                imagen.TIPO_IMAGEN = IMAGENarr[0];
                imagen.EXTENSION = IMAGENarr[0];

                byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_IMAGEN_IMAGENES_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);
                    command.Parameters.AddWithValue("@IMAGEN", imgBaseDatos);
                    command.Parameters.AddWithValue("@TIPO_IMAGEN", imagen.TIPO_IMAGEN);
                    command.Parameters.AddWithValue("@VIGENCIA", imagen.VIGENCIA);
                    command.Parameters.AddWithValue("@EXTENSION", imagen.EXTENSION);
                    command.Parameters.AddWithValue("@ORDEN_IMAGEN", orden);


                    //guardarEnCarpeta(img, ID_MARCA);



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

                    if (reader["ID_MARCA"] == DBNull.Value )
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

                    if (reader["TIPO_INGRESO"] == DBNull.Value)
                    {
                        nuevoVehiculo.TIPO_INGRESO = "";
                    }
                    else
                    {
                        nuevoVehiculo.TIPO_INGRESO = (reader["TIPO_INGRESO"].ToString());
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
                        if (reader["PRECIO_COMPRA"]== DBNull.Value)
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

                        if (reader["KILOMETRAJE"]== DBNull.Value)
                        {
                            vehiculo.KILOMETRAJE = 0;
                        }

                        else
                        {
                            vehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
                        }
                        if (reader["NOMBRE_TRANSMISION"]== DBNull.Value)
                        {
                            vehiculo.NOMBRE_TRANSMISION = "";
                        }
                        else
                        {
                            vehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();
                        }
                        if (reader["COMBUSTIBLE"]== DBNull.Value)
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
                            vehiculo.COLOR ="";
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

        public RespuestaServicio EliminarVehiculo(int idVehiculo,string nombreUsuario, string razonCambios, int idUsuario,string patenteVehiculo)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_VEHICULO_DE_BD", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);


                    command.ExecuteNonQuery();
                    //======================================================================================================================
                    //===============================================INGRESO A BITACORA=====================================================

                 

                    OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", Ha Eliminado el vehículo con patente."+ patenteVehiculo + ""+
                                         "\n Razon de los cambios: " + razonCambios;

                    OBJbitacora.ID_VEHICULO = idVehiculo;
                    OBJbitacora.ESTADO = "ELIMINADO-BD";

                    OBJbitacora.TIPO = 1;
                    OBJbitacora.ID_USUARIO = idUsuario;


                    OBJbitacora.AgregarInformacionBitacora(OBJbitacora);



                    //======================================================================================================================
                    //======================================================================================================================

                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "El vehículo se eliminó exitosamente";
                    respuestaServicio.Detalle_Error = "";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No se logro eliminar el Vehiculo seleccionado";
            }

            return respuestaServicio;
        }
        public RespuestaServicio VenderVehiculo(int idVehiculo, string nombreUsuario, int idUsuario, string patenteVehiculo)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_DISPONIBILIDAD_VEHICULO_ESTADO_VENDIDO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);


                    command.ExecuteNonQuery();
                    //======================================================================================================================
                    //===============================================INGRESO A BITACORA=====================================================



                    OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", Ha Vendido el vehículo con patente." + patenteVehiculo;

                    OBJbitacora.ID_VEHICULO = idVehiculo;
                    OBJbitacora.ESTADO = "";

                    OBJbitacora.TIPO = 1;
                    OBJbitacora.ID_USUARIO = idUsuario;


                    OBJbitacora.AgregarInformacionBitacora(OBJbitacora);



                    //======================================================================================================================
                    //======================================================================================================================

                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "El vehículo se vendió";
                    respuestaServicio.Detalle_Error = "";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No se logro vender el vehículo seleccionado";
            }

            return respuestaServicio;
        }

        public RespuestaServicio VenderVehiculo_Sin_Bitacora(int idVehiculo)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_DISPONIBILIDAD_VEHICULO_ESTADO_VENDIDO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);


                    command.ExecuteNonQuery();
                
                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "El vehículo se vendió exitosamente";
                    respuestaServicio.Detalle_Error = "";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No se logro vender el Vehiculo seleccionado";
            }

            return respuestaServicio;
        }
        public RespuestaServicio RetirarVehiculo(int IdVehiculo,string RazonDeElimninacion,string Patente,string nombreUsuario,int idUsuario)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_DISPONIBILIDAD_VEHICULO_ESTADO_RETIRADO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", IdVehiculo);
  


                    command.ExecuteNonQuery();
                    //======================================================================================================================
                    //===============================================INGRESO A BITACORA=====================================================



                    OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", Ha retirado el vehículo con patente." + Patente + "" +
                                         "\n Razon de los cambios: " + RazonDeElimninacion;

                    OBJbitacora.ID_VEHICULO = IdVehiculo;
                    OBJbitacora.ESTADO = "RETIRADO";

                    OBJbitacora.TIPO = 1;
                    OBJbitacora.ID_USUARIO = idUsuario;


                    OBJbitacora.AgregarInformacionBitacora(OBJbitacora);



                    //======================================================================================================================
                    //======================================================================================================================

                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "El vehículo se eliminó exitosamente";
                    respuestaServicio.Detalle_Error = "";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No se logro eliminar el vehículo seleccionado";
            }

            return respuestaServicio;
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

        public List<Vehiculos> Consultar_Vehiculos_Filtro_Sucursal(int idSucursal, int idUsuario, int idIngreso, int idDisponibilidad, int idEstado,string fechaInicial,string fechaFinal)
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

        public List<Vehiculos> Consultar_Vehiculos_Propios_PartePago()
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
                    SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_PROPIOS_Y_PARTE_DE_PAGO", conn);

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

        public List<Vehiculos> Consultar_Vehiculos_Por_FechaIngreso(DateTime FechaInicial,DateTime FechaFinal)
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
                        vehiculo.DISPONIBILIDAD= reader["DISPONIBILIDAD"].ToString();

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
                        catch(Exception){ }

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

        public int Consultar_Cantidad_Vehiculos_Propios_Y_ParteDePago()
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
                    SqlCommand command = new SqlCommand("CONSULTAR_CANTIDAD_VEHICULOS_CONSIGNACION_Y_PARTE_PAGO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

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


        public string EditarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes,string razonCambios,string nombreUsuario,int idUsuario, List<EquipamientosVehiculo> ListaDeEquipamientosDeVehiculo)
        {
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            EquipamientosVehiculo OBJEquipamientoVehiculo = new EquipamientosVehiculo();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();
            Vehiculos OBJ_VehiculoDatosAntiguos = new Vehiculos();

            try
            {
                OBJ_VehiculoDatosAntiguos = OBJ_VehiculoDatosAntiguos.BuscarVehiculoID(vehiculo.ID_VEHICULO);

                 conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("EDITAR_VEHICULO", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;



                    command.Parameters.AddWithValue("@ID_VEHICULO", vehiculo.ID_VEHICULO);
                    command.Parameters.AddWithValue("@ID_USUARIO", vehiculo.ID_USUARIOS);
                    command.Parameters.AddWithValue("@PATENTE", vehiculo.PATENTE);
                    command.Parameters.AddWithValue("@FECHA_INGRESO", vehiculo.FECHA_INGRESO);

                    if (vehiculo.ID_SUCURSAL == 0)
                    {
                        command.Parameters.AddWithValue("@ID_SUCURSAL", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_SUCURSAL", vehiculo.ID_SUCURSAL);
                    }

                    if (vehiculo.ID_DISPONIBILIDAD == 0)
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_DISPONIBILIDAD", vehiculo.ID_DISPONIBILIDAD);
                    }

                    if (vehiculo.ID_MARCA == 0)
                    {
                        command.Parameters.AddWithValue("@ID_MARCA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_MARCA", vehiculo.ID_MARCA);
                    }

                    if (vehiculo.ID_TIPOTRACCION == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPOTRACCION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPOTRACCION", vehiculo.ID_TIPOTRACCION);
                    }

                    if (vehiculo.ID_TIPO_CONSIGNACION == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", vehiculo.ID_TIPO_CONSIGNACION);
                    }

                    if (vehiculo.ID_COLOR == 0)
                    {
                        command.Parameters.AddWithValue("@ID_COLOR", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_COLOR", vehiculo.ID_COLOR);
                    }

                    if (vehiculo.ID_TIPO_TRANSMICION == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_TRANSMICION", vehiculo.ID_TIPO_TRANSMICION);
                    }

                    if (vehiculo.ID_TIPO_VEHICULO == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", vehiculo.ID_TIPO_VEHICULO);
                    }

                    if (vehiculo.ID_TIPO_COMBUSTIBLE == 0)
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", vehiculo.ID_TIPO_COMBUSTIBLE);
                    }

                    if (vehiculo.ID_ESTADO == 0)
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_ESTADO", vehiculo.ID_ESTADO);
                    }

                    if (vehiculo.ID_MODELO == 0)
                    {
                        command.Parameters.AddWithValue("@ID_MODELO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_MODELO", vehiculo.ID_MODELO);
                    }


                    if (vehiculo.VERSION == null)
                    {
                        command.Parameters.AddWithValue("@VERSION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@VERSION", vehiculo.VERSION);
                    }

                    if (vehiculo.MOTOR == null)
                    {
                        command.Parameters.AddWithValue("@MOTOR", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@MOTOR", vehiculo.MOTOR);
                    }

                    if (vehiculo.CHASIS == null)
                    {
                        command.Parameters.AddWithValue("@CHASIS", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CHASIS", vehiculo.CHASIS);
                    }

                    if (vehiculo.CILINDRADA == null)
                    {
                        command.Parameters.AddWithValue("@CILINDRADA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CILINDRADA", vehiculo.CILINDRADA);
                    }

                    if (vehiculo.ANO == 0)
                    {
                        command.Parameters.AddWithValue("@ANO", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ANO", vehiculo.ANO);
                    }

                    if (vehiculo.PRECIO_VENTA == 0)
                    {
                        command.Parameters.AddWithValue("@PRECIO_VENTA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PRECIO_VENTA", vehiculo.PRECIO_VENTA);
                    }

                    if (vehiculo.PRECIO_COMPRA == 0)
                    {
                        command.Parameters.AddWithValue("@PRECIO_COMPRA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PRECIO_COMPRA", vehiculo.PRECIO_COMPRA);
                    }

                    if (vehiculo.PRECIO_CONSIGNACION == 0)
                    {
                        command.Parameters.AddWithValue("@PRECIO_CONSIGNACION", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PRECIO_CONSIGNACION", vehiculo.PRECIO_CONSIGNACION);
                    }

                    if (vehiculo.PRECIO_MINIMO_VENTA == 0)
                    {
                        command.Parameters.AddWithValue("@PRECIO_MINIMO_VENTA", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PRECIO_MINIMO_VENTA", vehiculo.PRECIO_MINIMO_VENTA);


                    }

                    if (vehiculo.ID_CLIENTE == 0)
                    {
                        command.Parameters.AddWithValue("@ID_CLIENTE", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID_CLIENTE", vehiculo.ID_CLIENTE);
                    }



                    

                    command.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.KILOMETRAJE);


                    command.Parameters.AddWithValue("@CANTIDAD_DUENIOS", vehiculo.CANTIDAD_DUENIOS);

                    //--------------------------------------------------------------------------

                    if (vehiculo.IMAGEN_PRINCIPAL != null)
                    {
                        string imagenCompleta = vehiculo.IMAGEN_PRINCIPAL;
                        string[] arrImagen = imagenCompleta.Split(',');
                        string imagenSinExtension = arrImagen[1];

                        byte[] imgBaseDatos = Base64Decoder(imagenSinExtension);

                        command.Parameters.AddWithValue("@IMAGEN_PRINCIPAL", imgBaseDatos);
                        command.Parameters.AddWithValue("@TIPO_IMAGEN", vehiculo.TIPO_IMAGEN);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@IMAGEN_PRINCIPAL", DBNull.Value);
                        command.Parameters.AddWithValue("@TIPO_IMAGEN", DBNull.Value);
                    }

                    command.ExecuteNonQuery();

                    //======================================================================================================================
                    //===============================================INGRESO A BITACORA=====================================================

                    string cambiosDetallados = "";


                    //Verifico si se hicieron cambios en el kilometraje o en el precio de venta
                    if (OBJ_VehiculoDatosAntiguos.ID_DISPONIBILIDAD != vehiculo.ID_DISPONIBILIDAD)
                    {
                        cambiosDetallados += "\n  \n  \n  Cambios a Disponibilidad:\n Valor Antiguo: " + OBJ_VehiculoDatosAntiguos.DISPONIBILIDAD +
                                                                  "\n Valor nuevo: " + vehiculo.DISPONIBILIDAD ;
                    }
                    if (OBJ_VehiculoDatosAntiguos.PRECIO_COMPRA != vehiculo.PRECIO_COMPRA)
                    {
                        cambiosDetallados += "\n  \n  \nCambios a Precio de toma:\n Valor Antiguo: " + OBJ_VehiculoDatosAntiguos.DISPONIBILIDAD +
                                                                  "\n Valor nuevo: " + vehiculo.DISPONIBILIDAD ;
                    }

                    if (OBJ_VehiculoDatosAntiguos.PRECIO_VENTA != vehiculo.PRECIO_VENTA)
                    {
                        cambiosDetallados += "\n  \n  \n  Cambios a Precio de venta:\n Valor Antiguo: " + OBJ_VehiculoDatosAntiguos.DISPONIBILIDAD +
                                                                  "\n Valor nuevo: " + vehiculo.DISPONIBILIDAD ;
                    }

                    if (OBJ_VehiculoDatosAntiguos.KILOMETRAJE != vehiculo.KILOMETRAJE)
                    {
                        cambiosDetallados += "\n  \n  \n  Cambios a kilometraje:\n Valor Antiguo: " + OBJ_VehiculoDatosAntiguos.KILOMETRAJE +
                                                                  "\n Valor nuevo: " + vehiculo.KILOMETRAJE;
                    }


                    //======================================================================================================================

                    OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", ha editado el vehículo." +
                                         "\n Razón de los cambios: " + razonCambios +
                                         cambiosDetallados;

                    OBJbitacora.ID_VEHICULO = vehiculo.ID_VEHICULO;
                    OBJbitacora.ESTADO = vehiculo.DISPONIBILIDAD;
                   
                    OBJbitacora.TIPO = 1;
                    OBJbitacora.ID_USUARIO = idUsuario;


                    OBJbitacora.AgregarInformacionBitacora(OBJbitacora);



                    //======================================================================================================================
                    //======================================================================================================================



                    //Se encarga de eliminar el equipamiento del vehiculo
                    OBJEquipamientoVehiculo.Eliminar_Equipamiento_Vehiculo(vehiculo.ID_VEHICULO);

                    foreach (var categorias in ListaDeEquipamientosDeVehiculo)
                    {
                        //Encargado de agregar equipamiento del vehiculo
                        OBJEquipamientoVehiculo.Agregar_Equipamiento_Vehiculo(categorias.ID_EQUIPAMIENTO, vehiculo.ID_VEHICULO);
                    }




                    if (imagenes != null)
                    {
                        if (imagenes.Count > 0)
                        {
                            EliminarImagenesVehiculo(vehiculo.ID_VEHICULO);

                            int ordenImagenes = 1;
                            foreach (var imagen in imagenes)
                            {
                                imagen.ID_VEHICULO = vehiculo.ID_VEHICULO;
                                InsertarImagenVehiculo(imagen, vehiculo.ID_VEHICULO, ordenImagenes);
                                ordenImagenes++;
                            }
                        }

                    }

                }


                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Se editaron correctamente los datos del Vehiculo";

            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = ex.Message;

            }
            string respuesta = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            return respuesta;
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

        public Vehiculos ConsultarVehiculoaConsignar(int idVehiculo)
        {
            Vehiculos nuevoVehiculo = new Vehiculos();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULO_CONTRATO_CONSIGNACION", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    nuevoVehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    nuevoVehiculo.FECHA_INGRESO = (DateTime)reader["FECHA_INGRESO"];
                    nuevoVehiculo.PATENTE = reader["PATENTE"].ToString();
                    nuevoVehiculo.ANO = Convert.ToInt32(reader["ANO"]);
                    nuevoVehiculo.MARCA = reader["MARCA"].ToString();
                    nuevoVehiculo.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    nuevoVehiculo.COLOR = reader["COLOR"].ToString();
                    nuevoVehiculo.TIPO_VEHICULO = reader["CARROCERIA"].ToString();
                    nuevoVehiculo.NOMBRE_CLIENTE = reader["NOMBRES"].ToString();
                    nuevoVehiculo.APELLIDO_CLIENTE = reader["APELLIDOS"].ToString();
                    nuevoVehiculo.RUT_CLIENTE = reader["RUT"].ToString();
                    nuevoVehiculo.DIRECCION_CLIENTE = reader["DIRECCION"].ToString();
                    nuevoVehiculo.COMUNA_CLIENTE = reader["COMUNA"].ToString();
                    nuevoVehiculo.CIUDAD_CLIENTE = reader["CIUDAD"].ToString();

                    

                    
                   

                    if (reader["MOTOR"] == DBNull.Value)
                    {
                        nuevoVehiculo.MOTOR = "NO INGRESADO";
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

                   

                    if (reader["PRECIO_VENTA"] == DBNull.Value)
                    {
                        nuevoVehiculo.PRECIO_VENTA = 0;
                    }
                    else
                    {
                        nuevoVehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
                    }

                    

                    if (reader["CHASIS"] == DBNull.Value)
                    {
                        nuevoVehiculo.CHASIS = "NO INGRESADO";
                    }
                    else
                    {
                        nuevoVehiculo.CHASIS = reader["CHASIS"].ToString();
                    }

                   

                    


                }

            }


            return nuevoVehiculo;

        }

        public List<Vehiculos> Consultar_Vehiculos_PartePago_Consignados()
        {
            List<Vehiculos> listaVehiculos = new List<Vehiculos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_CONSIGNADOS_PARTE_PAGO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehiculos vehiculo = new Vehiculos();
                    vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                    vehiculo.PATENTE = reader["PATENTE"].ToString();
                    vehiculo.MARCA = reader["MARCA"].ToString();
                    vehiculo.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    vehiculo.RUT_CLIENTE = reader["RUT"].ToString();

                    if (reader["ANO"] == DBNull.Value)
                    {
                        vehiculo.ANO = 0;
                    }
                    else
                    {
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["ANO"]);
                    }

                    if (reader["PRECIO_COMPRA"] == DBNull.Value)
                    {
                        vehiculo.PRECIO_VENTA = 0;
                    }
                    else
                    {
                        vehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
                    }


                    listaVehiculos.Add(vehiculo);

                }


            }



            return listaVehiculos;
        }

        public void PublicarVehiculo(int idVehiculo,bool publicado)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();
            int valorPublicado;

            if (publicado)
            {
                valorPublicado = 1;
            }
            else {
                valorPublicado = 0;

            }
            conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_VEHICULO_A_ESTADO_PUBLICADO", conn);
                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);
                    command.Parameters.AddWithValue("@OFERTA", valorPublicado);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.ExecuteNonQuery();

                }
                sql.CerrarConnection(conn);

        }

        public void despublicarVehiculo(int idVehiculo)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("EDITAR_VEHICULO_A_ESTADO_NO_PUBLICADO", conn);
                command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.ExecuteNonQuery();


            }
            sql.CerrarConnection(conn);

        }


        public int ConsularContratoConsignacionVehiculo(int idVehiculo)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();
            int IDContratoConsignacion = 0;

            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_ULTIMO_CONTRATO_CONSIGNACION_POR_ID_VEHICULO", conn);
                command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    IDContratoConsignacion = Convert.ToInt32(reader["ID_CONTRATO_CONSIGNACION"]);
                }


            }
            sql.CerrarConnection(conn);

            return IDContratoConsignacion;
        }


    }

}