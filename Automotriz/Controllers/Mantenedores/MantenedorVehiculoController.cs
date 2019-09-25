using Automotriz.connection;
using Automotriz.Models;
using Automotriz.Models.EquipamientoVehiculo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.ingres
{
    public class MantenedorVehiculoController : Controller
    {

        //----------------------------VISTAS------------------------------------------------------------
        // GET: MantenedorVehiculo
        public ActionResult Index()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];
            ViewBag.Rol = Session["ROL"];


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
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
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

            //Se instancia Objeto bitacora
            Bitacora_Vehiculo OBJ_bitacora = new Bitacora_Vehiculo();

            //Se retornara lista con datos de la bitacora
            ViewBag.Bitacora = OBJ_bitacora.Consultar_Bitacora(idVehiculo);

            Equipamiento OBJEquipamiento = new Equipamiento();
            List<Equipamiento> ListaEquipamientos = OBJEquipamiento.Consultar_Equipamientos_Disponibles();

            ViewBag.ListaEquipamientos = ListaEquipamientos;

            Categoria_Equipamiento OBJECategoriaquipamiento = new Categoria_Equipamiento();
            List<Categoria_Equipamiento> ListaCategoriaEquipamientos = OBJECategoriaquipamiento.Consultar_Categoria_Equipamientos_Disponibles();

            ViewBag.ListaCategoriaEquipamientos = ListaCategoriaEquipamientos;

            EquipamientosVehiculo OBJEquipamientoVehiculo = new EquipamientosVehiculo();
            List<EquipamientosVehiculo> ListaDeEquipamientosDeVehiculo = OBJEquipamientoVehiculo.Consultar_Equipamiento_Vehiculo_Por_Id_Vehiculo(idVehiculo);

            ViewBag.ListaDeEquipamientosDeVehiculo = ListaDeEquipamientosDeVehiculo;

            int idContratoConsignacion = BuscaVehiculo.ConsularContratoConsignacionVehiculo(idVehiculo);

            ViewBag.IdContratoConsignacion = idContratoConsignacion;



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
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
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

            EstadoVehiculo EstadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstado = EstadoVehiculo.Consultar_EstadoVehiculo();

            ViewBag.ListaEstado = ListadoEstado;


            Usuarios Usuarios = new Usuarios();
            List<Usuarios> ListadoUsuarios = Usuarios.ConsultarUsuarios();

            ViewBag.ListaUsuarios = ListadoUsuarios;

            Cliente Cliente = new Cliente();
            List<Cliente> ListadoClientes = Cliente.Consultar_Clientes();

            ViewBag.ListaClientes = ListadoClientes;

            Equipamiento OBJEquipamiento = new Equipamiento();
            List<Equipamiento> ListaEquipamientos = OBJEquipamiento.Consultar_Equipamientos_Disponibles();

            ViewBag.ListaEquipamientos = ListaEquipamientos;

            Categoria_Equipamiento OBJECategoriaquipamiento = new Categoria_Equipamiento();
            List<Categoria_Equipamiento> ListaCategoriaEquipamientos = OBJECategoriaquipamiento.Consultar_Categoria_Equipamientos_Disponibles();

            ViewBag.ListaCategoriaEquipamientos = ListaCategoriaEquipamientos;
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
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
            }

            if (ViewBag.Rol != "ADMINISTRADOR" && ViewBag.Rol != "GERENCIA" && ViewBag.Rol != "JEFE DE VENTAS")
            {
                return RedirectToAction("Login", "Login");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            Sucursal OBJSucursal = new Sucursal();
            List<Sucursal> ListaDeSucursales = new List<Sucursal>();
            List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
            Vehiculos BuscaVehiculo = new Vehiculos();

            Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos();

            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            ListaDeSucursales = OBJSucursal.Consultar_Sucursales();

            ViewBag.ListadoDeSucursales = ListaDeSucursales;

            return View();
        }

        public ActionResult ListadoVehiculos_RolLectura()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
            }
            if (ViewBag.Rol != "CONSIGNADOR" && ViewBag.Rol != "GESTION")
            {
                return RedirectToAction("Login", "Login");
            }
            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            Sucursal OBJSucursal = new Sucursal();
            List<Sucursal> ListaDeSucursales = new List<Sucursal>();
            List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
            Vehiculos BuscaVehiculo = new Vehiculos();

            Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos();

            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            ListaDeSucursales = OBJSucursal.Consultar_Sucursales();

            ViewBag.ListadoDeSucursales = ListaDeSucursales;

            return View();
        }

        public ActionResult ListadoVehiculos_RolVendedor()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
            }

            if (ViewBag.Rol != "VENDEDOR")
            {
                return RedirectToAction("Login", "Login");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            Sucursal OBJSucursal = new Sucursal();
            List<Sucursal> ListaDeSucursales = new List<Sucursal>();
            List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
            Vehiculos BuscaVehiculo = new Vehiculos();

            Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos();

            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            ListaDeSucursales = OBJSucursal.Consultar_Sucursales();

            ViewBag.ListadoDeSucursales = ListaDeSucursales;

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
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
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

            Cliente Cliente = new Cliente();
            List<Cliente> ListadoClientes = Cliente.Consultar_Clientes();

            ViewBag.ListaClientes = ListadoClientes;

            //FIN CARGA COMBOS PARA LA VISTA



            //Categorias

            Equipamiento OBJEquipamiento = new Equipamiento();
            List<Equipamiento> ListaEquipamientos = OBJEquipamiento.Consultar_Equipamientos_Disponibles();

            ViewBag.ListaEquipamientos = ListaEquipamientos;

            Categoria_Equipamiento OBJECategoriaquipamiento = new Categoria_Equipamiento();
            List<Categoria_Equipamiento> ListaCategoriaEquipamientos = OBJECategoriaquipamiento.Consultar_Categoria_Equipamientos_Disponibles();

            ViewBag.ListaCategoriaEquipamientos = ListaCategoriaEquipamientos;

            EquipamientosVehiculo OBJEquipamientoVehiculo = new EquipamientosVehiculo();
            List<EquipamientosVehiculo> ListaDeEquipamientosDeVehiculo = OBJEquipamientoVehiculo.Consultar_Equipamiento_Vehiculo_Por_Id_Vehiculo(idVehiculo);

            ViewBag.ListaDeEquipamientosDeVehiculo = ListaDeEquipamientosDeVehiculo;

            //Fin de categorias

            int idContratoConsignacion = BuscaVehiculo.ConsularContratoConsignacionVehiculo(idVehiculo);

            ViewBag.IdContratoConsignacion = idContratoConsignacion;

            return View();
        }
        //------------------------------FIN DE VISTAS---------------------------------------------------

        //------------------------------METODOS---------------------------------------------------------

        public string VehiculosPorSucursal(int IdSucursal)
        {
            RespuestaServicio OBJRespuestaServicio = new RespuestaServicio();
            Vehiculos OBJVehiculo = new Vehiculos();
            List<Vehiculos> ListaDeVehiculos = new List<Vehiculos>();
            string JSONRespuesta = "";

            try
            {
                if (IdSucursal == 0)
                {
                    ListaDeVehiculos = OBJVehiculo.Consultar_Vehiculos();
                }
                else
                {
                    ListaDeVehiculos = OBJVehiculo.Consultar_Vehiculos_Por_Sucursal(IdSucursal);

                }
                OBJRespuestaServicio.Respuesta = "OK";
                OBJRespuestaServicio.Descripcion = JsonConvert.SerializeObject(ListaDeVehiculos, Formatting.Indented);

            }
            catch (Exception ex)
            {
                OBJRespuestaServicio.Respuesta = "NOK";
                OBJRespuestaServicio.Detalle_Error = "No se logro consultar los vehiculos, Error: " + ex.Message;
            }

            JSONRespuesta = JsonConvert.SerializeObject(OBJRespuestaServicio, Formatting.Indented);
            return JSONRespuesta;
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
        public string GuardarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes,List<EquipamientosVehiculo> ListaDeEquipamientosDeVehiculo)
        {
            Vehiculos OBJVehiculos = new Vehiculos();

            string respuestaJSON = "";
            string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
            int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());

            if (ListaDeEquipamientosDeVehiculo==null) {

                ListaDeEquipamientosDeVehiculo = new List<EquipamientosVehiculo>();
            }

            respuestaJSON = OBJVehiculos.IngresarVehiculo(vehiculo,imagenes,nombreUsuario,idUsuario, ListaDeEquipamientosDeVehiculo);

            return respuestaJSON;
        }


        [HttpPost]
        public string EliminarVehiculo(int idVehiculo,string razonDeElimninacion,string patente)
        {
            Vehiculos EliminarVehiculo = new Vehiculos();
            RespuestaServicio respuesta = new RespuestaServicio();
            string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
            int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());
            try
            {
                respuesta = EliminarVehiculo.EliminarVehiculo(idVehiculo, nombreUsuario,razonDeElimninacion,idUsuario, patente);
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

        [HttpPost]
        public string RetirarVehiuclo(int IdVehiculo, string RazonDeElimninacion, string Patente)
        {
            Vehiculos OBJ_Vehiculo = new Vehiculos();
            RespuestaServicio respuesta = new RespuestaServicio();
            string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
            int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());
            try
            {
                respuesta = OBJ_Vehiculo.RetirarVehiculo(IdVehiculo, RazonDeElimninacion, Patente, nombreUsuario, idUsuario);
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

        [HttpPost]
        public string VenderVehiuclo(int IdVehiculo,string Patente)
        {
            Vehiculos OBJ_Vehiculo = new Vehiculos();
            RespuestaServicio respuesta = new RespuestaServicio();
            string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
            int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());
            try
            {
                
                respuesta = OBJ_Vehiculo.VenderVehiculo(IdVehiculo, nombreUsuario,idUsuario, Patente);
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

        public string EditarVehiculo(Vehiculos vehiculo, List<ImagenVehiculo> imagenes,string razonCambios, List<EquipamientosVehiculo> ListaDeEquipamientosDeVehiculo)
        {
            Vehiculos OBJ_Vehiculo = new Vehiculos();
            string respuestaJSON;
            string nombreUsuario = Session["NOMBRE_USUARIO"].ToString();
            int idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());

            if (ListaDeEquipamientosDeVehiculo == null)
            {

                ListaDeEquipamientosDeVehiculo = new List<EquipamientosVehiculo>();
            }

            respuestaJSON = OBJ_Vehiculo.EditarVehiculo(vehiculo, imagenes, razonCambios, nombreUsuario, idUsuario, ListaDeEquipamientosDeVehiculo);

            return respuestaJSON;
        }

        public string BuscaVehiculoID(int idVehiculo)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Vehiculos OBJ_Vehiculo = new Vehiculos();


            try
            {

                OBJ_Vehiculo = OBJ_Vehiculo.BuscarVehiculoID(idVehiculo);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(OBJ_Vehiculo, Formatting.Indented);
                Respuesta.Detalle_Error = "";

            }
            catch (Exception Err)
            {

                Respuesta.Respuesta = "NOK";
                Respuesta.Descripcion = "";
                Respuesta.Detalle_Error = Err.Message.ToString();
            }

            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented);
        }

        public string PublicarVehiculo(int ID_VEHICULO, bool OFERTA)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Vehiculos OBJ_Vehiculo = new Vehiculos();
            try
            {
                OBJ_Vehiculo.PublicarVehiculo(ID_VEHICULO, OFERTA);

                Respuesta.Respuesta = "OK";
                Respuesta.Detalle_Error = "";

            }
            catch (Exception Err)
            {

                Respuesta.Respuesta = "NOK";
                Respuesta.Descripcion = "";
                Respuesta.Detalle_Error = Err.Message.ToString();
            }

            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented);
        }

        public string DespublicarVehiculo(int ID_VEHICULO)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Vehiculos OBJ_Vehiculo = new Vehiculos();
            try
            {
                OBJ_Vehiculo.despublicarVehiculo(ID_VEHICULO);

                Respuesta.Respuesta = "OK";
                Respuesta.Detalle_Error = "";

            }
            catch (Exception Err)
            {

                Respuesta.Respuesta = "NOK";
                Respuesta.Descripcion = "";
                Respuesta.Detalle_Error = Err.Message.ToString();
            }

            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented);
        }



    }

    
}

