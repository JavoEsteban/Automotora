using Automotriz.connection;
using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers
{
    public class MantenedorVehiculoController : Controller
    {
        // GET: MantenedorVehiculo
        public ActionResult Index()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            
            return View();
        }

        public ActionResult FormularioEditarVehiculo()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            //Busca el vehiculo para cargar en la vista
            Vehiculos BuscaVehiculo = new Vehiculos();

            int idVehiculo = Int32.Parse(Request.QueryString["ID"]);
            BuscaVehiculo = BuscaVehiculo.BuscarVehiculoID(idVehiculo);

            //Obtiene imagenes del vehiculo
            List<ImagenVehiculo> ImagenesVehiculo = obetenerTodasLasImagenes(BuscaVehiculo.ID_VEHICULO);


            ViewBag.Vehiculo = BuscaVehiculo;
            ViewBag.ImagenesVehiculo = ImagenesVehiculo;

            //CARGAR COMBOS PARA LA VISTA

            Marca Marca = new Marca();
            List<Marca> ListaMarcas = Marca.Consultar_Marcas();

            ViewBag.ListaMarcas = ListaMarcas;

            TipoVehiculo TiposVehiculo = new TipoVehiculo();
            List<TipoVehiculo> ListaTipoVehiculos = TiposVehiculo.Consultar_TipoVehiculo();

            ViewBag.ListaTipoVehiculo = ListaTipoVehiculos;


            TipoTransmision TipoTransmision = new TipoTransmision();
            List<TipoTransmision> ListaTransmision = TipoTransmision.Consultar_Transmision();

            ViewBag.ListaTipoTransmision = ListaTransmision;


            TipoTraccion TipoTraccion = new TipoTraccion();
            List<TipoTraccion> ListaTraccion = TipoTraccion.Consultar_Traccion();

            ViewBag.ListaTipoTraccion = ListaTraccion;

            TipoConsigna TipoConsignacion = new TipoConsigna();
            List<TipoConsigna> ListaConsigna = TipoConsignacion.Consultar_tipoConsigna();

            ViewBag.ListaConsignacion = ListaConsigna;

            TipoCombustible TipoCombustible = new TipoCombustible();
            List<TipoCombustible> ListadoCombustible = TipoCombustible.Consultar_TipoCombustible();

            ViewBag.ListaCombustible = ListadoCombustible;


            Color Color = new Color();
            List<Color> ListadoColor = Color.Consultar_Color();

            ViewBag.ListaColor = ListadoColor;


            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();

            ViewBag.ListaSucursal = ListadoSucursal;

            Disponibilidad Disponibilidad = new Disponibilidad();
            List<Disponibilidad> ListadoDisponibilidad = Disponibilidad.Consultar_Disponibilidad();

            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;

            EstadoVehiculo EstadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstado = EstadoVehiculo.Consultar_EstadoVehiculo();

            ViewBag.ListaEstado = ListadoEstado;


            Usuarios Usuarios = new Usuarios();
            List<Usuarios> ListadoUsuarios = Usuarios.ConsultarUsuarios();

            ViewBag.ListaUsuarios = ListadoUsuarios;

            Cliente Cliente = new Cliente();
            List<Cliente> ListadoClientes = Cliente.Consultar_Clientes();

            ViewBag.ListaClientes = ListadoClientes;

            //FIN CARGA COMBOS PARA LA VISTA




            return View();
        }

        public ActionResult IngresoVehiculo()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            //CARGAR COMBOS PARA LA VISTA

            Marca Marca = new Marca();
            List<Marca> ListaMarcas = Marca.Consultar_Marcas();

            ViewBag.ListaMarcas = ListaMarcas;

            TipoVehiculo TiposVehiculo = new TipoVehiculo();
            List<TipoVehiculo> ListaTipoVehiculos = TiposVehiculo.Consultar_TipoVehiculo();

            ViewBag.ListaTipoVehiculo = ListaTipoVehiculos;


            TipoTransmision TipoTransmision = new TipoTransmision();
            List<TipoTransmision> ListaTransmision = TipoTransmision.Consultar_Transmision();

            ViewBag.ListaTipoTransmision = ListaTransmision;


            TipoTraccion TipoTraccion = new TipoTraccion();
            List<TipoTraccion> ListaTraccion = TipoTraccion.Consultar_Traccion();

            ViewBag.ListaTipoTraccion = ListaTraccion;

            TipoConsigna TipoConsignacion = new TipoConsigna();
            List<TipoConsigna> ListaConsigna = TipoConsignacion.Consultar_tipoConsigna();

            ViewBag.ListaConsignacion = ListaConsigna;

            TipoCombustible TipoCombustible = new TipoCombustible();
            List<TipoCombustible> ListadoCombustible = TipoCombustible.Consultar_TipoCombustible();

            ViewBag.ListaCombustible = ListadoCombustible;


            Color Color = new Color();
            List<Color> ListadoColor = Color.Consultar_Color();

            ViewBag.ListaColor = ListadoColor;


            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();

            ViewBag.ListaSucursal = ListadoSucursal;

            Disponibilidad Disponibilidad = new Disponibilidad();
            List<Disponibilidad> ListadoDisponibilidad = Disponibilidad.Consultar_Disponibilidad();

            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;

            EstadoVehiculo EstadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstado = EstadoVehiculo.Consultar_EstadoVehiculo();

            ViewBag.ListaEstado = ListadoEstado;


            Usuarios Usuarios = new Usuarios();
            List<Usuarios> ListadoUsuarios = Usuarios.ConsultarUsuarios();

            ViewBag.ListaUsuarios = ListadoUsuarios;

            Cliente Cliente = new Cliente();
            List<Cliente> ListadoClientes = Cliente.Consultar_Clientes();

            ViewBag.ListaClientes = ListadoClientes;

            //FIN CARGA COMBOS PARA LA VISTA

            return View();
        }

        public ActionResult ListadoVehiculos()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
            Vehiculos BuscaVehiculo = new Vehiculos();

            Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos();

            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();
        }

        public ActionResult FormularioDetalleVehiculo()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            //Busca el vehiculo para cargar en la vista
            Vehiculos BuscaVehiculo = new Vehiculos();

            int idVehiculo = Int32.Parse(Request.QueryString["ID"]);
            BuscaVehiculo = BuscaVehiculo.BuscarVehiculoID(idVehiculo);

            //Obtiene imagenes del vehiculo
            List<ImagenVehiculo> ImagenesVehiculo = obetenerTodasLasImagenes(BuscaVehiculo.ID_VEHICULO);


            ViewBag.Vehiculo = BuscaVehiculo;
            ViewBag.ImagenesVehiculo = ImagenesVehiculo;

            //CARGAR COMBOS PARA LA VISTA

            Marca Marca = new Marca();
            List<Marca> ListaMarcas = Marca.Consultar_Marcas();

            ViewBag.ListaMarcas = ListaMarcas;

            TipoVehiculo TiposVehiculo = new TipoVehiculo();
            List<TipoVehiculo> ListaTipoVehiculos = TiposVehiculo.Consultar_TipoVehiculo();

            ViewBag.ListaTipoVehiculo = ListaTipoVehiculos;


            TipoTransmision TipoTransmision = new TipoTransmision();
            List<TipoTransmision> ListaTransmision = TipoTransmision.Consultar_Transmision();

            ViewBag.ListaTipoTransmision = ListaTransmision;


            TipoTraccion TipoTraccion = new TipoTraccion();
            List<TipoTraccion> ListaTraccion = TipoTraccion.Consultar_Traccion();

            ViewBag.ListaTipoTraccion = ListaTraccion;

            TipoConsigna TipoConsignacion = new TipoConsigna();
            List<TipoConsigna> ListaConsigna = TipoConsignacion.Consultar_tipoConsigna();

            ViewBag.ListaConsignacion = ListaConsigna;

            TipoCombustible TipoCombustible = new TipoCombustible();
            List<TipoCombustible> ListadoCombustible = TipoCombustible.Consultar_TipoCombustible();

            ViewBag.ListaCombustible = ListadoCombustible;


            Color Color = new Color();
            List<Color> ListadoColor = Color.Consultar_Color();

            ViewBag.ListaColor = ListadoColor;


            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();

            ViewBag.ListaSucursal = ListadoSucursal;

            Disponibilidad Disponibilidad = new Disponibilidad();
            List<Disponibilidad> ListadoDisponibilidad = Disponibilidad.Consultar_Disponibilidad();

            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;

            EstadoVehiculo EstadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstado = EstadoVehiculo.Consultar_EstadoVehiculo();

            ViewBag.ListaEstado = ListadoEstado;


            Usuarios Usuarios = new Usuarios();
            List<Usuarios> ListadoUsuarios = Usuarios.ConsultarUsuarios();

            ViewBag.ListaUsuarios = ListadoUsuarios;

            //Se instancia Objeto bitacora
            Bitacora_Vehiculo OBJ_bitacora = new Bitacora_Vehiculo();
            
            //Se retornara lista con datos de la bitacora
            ViewBag.Bitacora = OBJ_bitacora.Consultar_Bitacora(idVehiculo);

            //FIN CARGA COMBOS PARA LA VISTA


            return View();
        }










        public RespuestaServicio Editar()
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();


            return respuestaServicio;
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

        [HttpPost]
        public string ValidaPatenteExistente(string Patente)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            Vehiculos buscaPatente = new Vehiculos();

            try
            {
                int ExistePatente = buscaPatente.ValidaVehiculoPatente(Patente);

                if (ExistePatente == 0)
                {
                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "";
                    respuestaServicio.Detalle_Error = "";
                }
                else if (ExistePatente == 1)
                {
                    respuestaServicio.Respuesta = "NOK";
                    respuestaServicio.Descripcion = "";
                    respuestaServicio.Detalle_Error = "Ya existe un vehiculo con patente: " + Patente;
                }
                else if (ExistePatente == -1)
                {
                    respuestaServicio.Respuesta = "NOK";
                    respuestaServicio.Descripcion = "";
                    respuestaServicio.Detalle_Error = "Error al validar la patente";
                }
            }
            catch (Exception)
            {

                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Error de conexión";
            }
            
            return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);

        }

        [HttpPost]
        public string GuardarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();
            int ID_AUTO_INGRESADO = 0;
            string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
            int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());



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

                    //Es necesario un acuerdo para determinar si se ingresa como 0 o como null

                    //if (vehiculo.KILOMETRAJE == 0)
                    //{
                    //    command.Parameters.AddWithValue("@KILOMETRAJE", DBNull.Value);
                    //}
                    //else
                    //{
                    //    command.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.KILOMETRAJE);
                    //}

                    command.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.KILOMETRAJE);


                    //if (vehiculo.CANTIDAD_DUENIOS == 0)
                    //{
                    //    command.Parameters.AddWithValue("@CANTIDAD_DUENIOS", DBNull.Value);
                    //}
                    //else
                    //{
                    //    command.Parameters.AddWithValue("@CANTIDAD_DUENIOS", vehiculo.CANTIDAD_DUENIOS);
                    //}

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



                    //se ejecuta el metodo y toma la respuesta 
                    SqlDataReader reader = command.ExecuteReader();

                    //lee la respuesta que envia 
                    while (reader.Read())
                    {

                        //tomo el valor de de la bd (ve el procedimiento almacenado INSERTAR_VEHICULO )
                        ID_AUTO_INGRESADO = Int32.Parse(reader["ID_AUTO"].ToString());

                        OBJbitacora.ID_VEHICULO = ID_AUTO_INGRESADO;
                        OBJbitacora.ESTADO = vehiculo.DISPONIBILIDAD;
                        OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", A agregado un nuevo Vehiculo Como " + vehiculo.TIPO_INGRESO;
                        OBJbitacora.TIPO = 1;
                        OBJbitacora.ID_USUARIO = idUsuario;


                        OBJbitacora.AgregarInformacionBitacora(OBJbitacora);
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

                    sql.CerrarConnection(conn);


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "";
                    respuestaServicio.Detalle_Error = "";

                }
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








        //public string traerVehiculoPorID(int idVehiculoParam)
        //{

        //    Vehiculos nuevoVehiculo = new Vehiculos();
        //    RespuestaServicio respuestaServicio = new RespuestaServicio();
        //    try
        //    {
        //        SQLconn sql = new SQLconn();
        //        SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            SqlCommand command = new SqlCommand("TRAER_VEHICULO_POR_ID", conn);

        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculoParam);

        //            SqlDataReader reader = command.ExecuteReader();
        //            int contadorVehiculo = 0;
        //            while (reader.Read())
        //            {

        //                nuevoVehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
        //                nuevoVehiculo.ID_SUCURSAL = Convert.ToInt32(reader["ID_SUCURSAL"]);
        //                nuevoVehiculo.ID_DISPONIBILIDAD = Convert.ToInt32(reader["ID_DISPONIBILIDAD"]);
        //                nuevoVehiculo.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"]);
        //                nuevoVehiculo.ID_TIPOTRACCION = Convert.ToInt32(reader["ID_TIPOTRACCION"]);
        //                nuevoVehiculo.ID_TIPO_CONSIGNACION = Convert.ToInt32(reader["ID_TIPO_CONSIGNACION"]);
        //                nuevoVehiculo.ID_COLOR = Convert.ToInt32(reader["ID_COLOR"]);
        //                nuevoVehiculo.ID_TIPO_TRANSMICION = Convert.ToInt32(reader["ID_TIPO_TRANSMICION"]);
        //                nuevoVehiculo.ID_TIPO_COMBUSTIBLE = Convert.ToInt32(reader["ID_TIPO_COMBUSTIBLE"]);
        //                nuevoVehiculo.ID_ESTADO = Convert.ToInt32(reader["ID_ESTADO"]);
        //                nuevoVehiculo.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
        //                nuevoVehiculo.ID_USUARIOS =  Convert.ToInt32(reader["ID_USUARIOS"]);
        //                nuevoVehiculo.ID_TIPO_VEHICULO = Convert.ToInt32(reader["ID_TIPO_VEHICULO"]);
        //                nuevoVehiculo.PATENTE = reader["PATENTE"].ToString();
        //                nuevoVehiculo.FECHA_INGRESO =(DateTime)reader["FECHA_INGRESO"];
        //                nuevoVehiculo.VERSION = reader["VERSION"].ToString();
        //                nuevoVehiculo.MOTOR = reader["MOTOR"].ToString();
        //                nuevoVehiculo.ANO = Convert.ToInt32(reader["ANO"]);
        //                nuevoVehiculo.CILINDRADA = reader["CILINDRADA"].ToString();
        //                nuevoVehiculo.PRECIO_VENTA = Convert.ToInt32(reader["PRECIO_VENTA"]);
        //                nuevoVehiculo.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_COMPRA"]);
        //                nuevoVehiculo.PRECIO_CONSIGNACION = Convert.ToInt32(reader["PRECIO_CONSIGNACION"]);
        //                nuevoVehiculo.PRECIO_MINIMO_VENTA = Convert.ToInt32(reader["PRECIO_MINIMO_VENTA"]);
        //                nuevoVehiculo.KILOMETRAJE = Convert.ToInt32(reader["KILOMETRAJE"]);
        //                nuevoVehiculo.CHASIS = reader["CHASIS"].ToString();
        //                nuevoVehiculo.CANTIDAD_DUENIOS = Convert.ToInt32(reader["KILOMETRAJE"]);

        //                contadorVehiculo++;
        //            }

        //            if (contadorVehiculo > 0)
        //            {
        //                respuestaServicio.Respuesta = "OK";
        //                respuestaServicio.Descripcion = JsonConvert.SerializeObject(nuevoVehiculo, Formatting.Indented);
        //                respuestaServicio.Detalle_Error = "";
        //            }
        //            else
        //            {
        //                respuestaServicio.Respuesta = "NOK";
        //                respuestaServicio.Descripcion = "";
        //                respuestaServicio.Detalle_Error = "Error al obtener información del Vehiculo";
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respuestaServicio.Respuesta = "NOK";
        //        respuestaServicio.Descripcion = "";
        //        respuestaServicio.Detalle_Error = ex.Message;
        //    }

        //    string respuesta = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
        //    return respuesta;

        //}

        [HttpPost]
        public string EliminarVehiculo(int idVehiculo)
        {
            Vehiculos EliminarVehiculo = new Vehiculos();
            RespuestaServicio respuesta = new RespuestaServicio();

            try
            {
                respuesta = EliminarVehiculo.EliminarVehiculo(idVehiculo);
            }
            catch (Exception ex)
            {

                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = ex.Message;
            }

            

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string traerVehiculoPorPatente(string PATENTE)
        {

            List<Vehiculos> listaVehiculo = new List<Vehiculos>();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            string jsonRespuestaServicio = "";
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("TRAER_VEHICULO_POR_PATENTE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PATENTE", PATENTE);

                    SqlDataReader reader = command.ExecuteReader();
                    Vehiculos vehiculo = null;
                    while (reader.Read())
                    {
                        vehiculo = new Vehiculos();
                        vehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);

                    }
                    if (vehiculo != null)
                    {
                        string JsonVehiculo = JsonConvert.SerializeObject(vehiculo, Formatting.Indented);
                        respuestaServicio.Respuesta = "OK";
                        respuestaServicio.Descripcion = JsonVehiculo;
                        respuestaServicio.Detalle_Error = "Se encontro vehiculo Correctamente";
                        jsonRespuestaServicio = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
                    }
                    else
                    {
                        respuestaServicio.Respuesta = "NOK";
                        respuestaServicio.Descripcion = "";
                        respuestaServicio.Detalle_Error = "No se encontro ningun Vehiculo con la patente: " + PATENTE;
                        jsonRespuestaServicio = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
                    }


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Uuups, no se logro buscar vehiculo con patente, Error: " + ex.Message;
                jsonRespuestaServicio = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            }


            return jsonRespuestaServicio;

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

        public void obtenerImagenPrincipalVehiculo(Vehiculos vehiculos)
        {
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("TRAER_IMAGEN_PRINCIPAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", vehiculos.ID_VEHICULO);

                    SqlDataReader reader = command.ExecuteReader();
                    ImagenVehiculo imagenVehiculo = null;
                    while (reader.Read())
                    {
                        byte[] imagenArray;
                        string tipoArchivo = "";
                        string base64 = "";
                        imagenVehiculo = new ImagenVehiculo();



                        imagenVehiculo.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();
                        imagenArray = (byte[])reader["IMAGEN"];

                        tipoArchivo = imagenVehiculo.TIPO_IMAGEN;
                        base64 = Convert.ToBase64String(imagenArray);
                        imagenVehiculo.IMAGEN = tipoArchivo + "," + base64;

                    }
                    if (imagenVehiculo != null)
                    {
                        vehiculos.IMAGEN_PRINCIPAL = imagenVehiculo.IMAGEN;
                    }
                    else
                    {
                        vehiculos.IMAGEN_PRINCIPAL = "~/img/autito.jpg";
                    }
                }
            }
            catch (Exception ex)
            {
                vehiculos.IMAGEN_PRINCIPAL = "~/img/autito.jpg";
            }
        }

        public List<ImagenVehiculo> obetenerTodasLasImagenes(int ID_VEHICULO)
        {
            List<ImagenVehiculo> imagenesVehiculos = new List<ImagenVehiculo>();

            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("TRAER_IMAGENES_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);

                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        ImagenVehiculo imagenVehiculo = new ImagenVehiculo();

                        byte[] imagenArray;
                        string tipoArchivo = "";
                        string base64 = "";
                        
                        imagenVehiculo.ID_IMAGEN = Convert.ToInt32(reader["ID_IMAGEN"]);
                        imagenVehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        imagenVehiculo.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();
                        imagenVehiculo.EXTENSION = reader["EXTENSION"].ToString();
                        imagenVehiculo.POSICION = Convert.ToInt32(reader["POSICION"]);

                        imagenArray = (byte[])reader["IMAGEN"];


                        tipoArchivo = imagenVehiculo.TIPO_IMAGEN;
                        base64 = Convert.ToBase64String(imagenArray);
                        imagenVehiculo.IMAGEN = tipoArchivo + "," + base64;

                        imagenesVehiculos.Add(imagenVehiculo);
                    }
                    
                }

                            }
            catch (Exception ex)
            {
                imagenesVehiculos = new List<ImagenVehiculo>();
               
            }
            return imagenesVehiculos;
        }


        public string ObtieneListadoVehiculos()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
            Vehiculos Obj_Vehiculos = new Vehiculos();

            try
            {

                Lista_Vehiculos = Obj_Vehiculos.Consultar_Vehiculos();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Vehiculos, Formatting.Indented);
                Respuesta.Detalle_Error = "";

            }
            catch (Exception Err)
            {

                Respuesta.Respuesta = "NOK";
                Respuesta.Descripcion = "";
                Respuesta.Detalle_Error = Err.Message.ToString();
            }

            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented); ;
        }


        public string AgregarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes)
        {
            RespuestaServicio respuesta;

            respuesta = vehiculo.AgregarVehiculo(vehiculo, imagenes);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }
        public string FiltrarVehiculo(Vehiculos vehiculo)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();
            int ID_AUTO_INGRESADO = 0;
            string jsonRespuestaServicio = "";

            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("TRAER_VEHICULOS_FILTRADOS", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_MARCA", vehiculo.ID_MARCA);
                    command.Parameters.AddWithValue("@ID_SUCURSAL", vehiculo.ID_SUCURSAL);
                    command.Parameters.AddWithValue("@ID_TRANSMISION", vehiculo.ID_TIPO_TRANSMICION);
                    command.Parameters.AddWithValue("@ID_TRACCION", vehiculo.ID_TIPOTRACCION);
                    command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", vehiculo.ID_TIPO_COMBUSTIBLE);
                    command.Parameters.AddWithValue("@PRECIO_MINIMO", vehiculo.PRECIO_MINIMO_VENTA);

                    SqlDataReader reader = command.ExecuteReader();

                    List<Vehiculos> ListaDeVehiculos = new List<Vehiculos>();
                    //lee la respuesta que envia 
                    while (reader.Read())
                    {
                        Vehiculos VueltaVehiculo = new Vehiculos();

                        VueltaVehiculo.ID_VEHICULO = Int32.Parse(reader["ID_VEHICULO"].ToString());
                        VueltaVehiculo.PATENTE = reader["PATENTE"].ToString();
                        VueltaVehiculo.ANO = Int32.Parse(reader["ANO"].ToString());
                        VueltaVehiculo.PRECIO_COMPRA = Int32.Parse(reader["PRECIO_COMPRA"].ToString());
                        VueltaVehiculo.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                        VueltaVehiculo.NOMBRE_TRANSMISION = reader["NOMBRE_TRANSMISION"].ToString();

                        ListaDeVehiculos.Add(VueltaVehiculo);
                    }

                    sql.CerrarConnection(conn);


                    if (ListaDeVehiculos.Count > 0)
                    {
                        string jsonListaVehiculos = JsonConvert.SerializeObject(ListaDeVehiculos, Formatting.Indented);

                        respuestaServicio.Respuesta = "OK";
                        respuestaServicio.Descripcion = jsonListaVehiculos;
                        respuestaServicio.Detalle_Error = "Se encontraton vehiculos Perfectamente";
                        jsonRespuestaServicio = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
                    }
                    else
                    {
                        respuestaServicio.Respuesta = "NOK";
                        respuestaServicio.Descripcion = "";
                        respuestaServicio.Detalle_Error = "No se encontraron Vehiculos con los filtros seleccionados";

                        jsonRespuestaServicio = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
                    }

                }
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "ups, no se lograron buscar vehiculos" + ex.Message;
                jsonRespuestaServicio = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            }

            return jsonRespuestaServicio;
        }

        public string BusquedaVehiculoMantenedor(Vehiculos vehiculo)
        {
            string JsonRespuesta = "";


            if (vehiculo.PATENTE != "" || vehiculo.PATENTE != null)
            {
                JsonRespuesta = traerVehiculoPorPatente(vehiculo.PATENTE);
            }
            else
            {

                JsonRespuesta = FiltrarVehiculo(vehiculo);
            }

            return JsonRespuesta;
        }


        //public RespuestaServicio EliminarVehiculo(int ID_VEHICULO)
        //{

        //    RespuestaServicio respuestaServicio = new RespuestaServicio();
        //    try
        //    {
        //        SQLconn sql = new SQLconn();
        //        SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            SqlCommand command = new SqlCommand("ELIMINAR_VEHICULO_VEHICULOS", conn);

        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);
        //            SqlDataReader reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //            }


        //        }
        //        sql.CerrarConnection(conn);
        //        respuestaServicio.Respuesta = "OK";
        //        respuestaServicio.Descripcion = "Se elimino correctamente el Vehículo";
        //        respuestaServicio.Detalle_Error = "";
        //        return respuestaServicio;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuestaServicio.Respuesta = "NOK";
        //        respuestaServicio.Descripcion = "";
        //        respuestaServicio.Detalle_Error = ex.Message;
        //        return respuestaServicio;
        //    }


        //}

        public string EditarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes)
        {
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {
                string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
                int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());

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



                    //Es necesario un acuerdo para determinar si se ingresa como 0 o como null

                    //if (vehiculo.KILOMETRAJE == 0)
                    //{
                    //    command.Parameters.AddWithValue("@KILOMETRAJE", DBNull.Value);
                    //}
                    //else
                    //{
                    //    command.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.KILOMETRAJE);
                    //}

                    command.Parameters.AddWithValue("@KILOMETRAJE", vehiculo.KILOMETRAJE);


                    //if (vehiculo.CANTIDAD_DUENIOS == 0)
                    //{
                    //    command.Parameters.AddWithValue("@CANTIDAD_DUENIOS", DBNull.Value);
                    //}
                    //else
                    //{
                    //    command.Parameters.AddWithValue("@CANTIDAD_DUENIOS", vehiculo.CANTIDAD_DUENIOS);
                    //}

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

                    OBJbitacora.ID_VEHICULO = vehiculo.ID_VEHICULO;
                    OBJbitacora.ESTADO = vehiculo.DISPONIBILIDAD;
                    OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", A Editado el vehiculo";
                    OBJbitacora.TIPO = 1;
                    OBJbitacora.ID_USUARIO = idUsuario;


                    OBJbitacora.AgregarInformacionBitacora(OBJbitacora);

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


       

    }

    
}

