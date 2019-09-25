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
    public class HomeController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
                 ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
                 ViewBag.IMAGEN = Session["IMAGEN"];
                 ViewBag.SUCURSAL = Session["SUCURSAL"];
            return View();
        }
        public ActionResult Dashboard()
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

            int stockCerrillos =0;
            int stockMaipu =0;
            int stockTotal = ConsultarStockSucursal();
            int stockConsignados = 0;

            Sucursal consultaIdSucursal = new Sucursal();
            List<Sucursal> lista_sucursales = new List<Sucursal>();
            lista_sucursales = consultaIdSucursal.Consultar_Sucursales();

            foreach (var sucursal in lista_sucursales) {
                int idSucursal = sucursal.ID_SUCURSAL;
                if (stockCerrillos == 0 && sucursal.NOMBRE_SUCURSAL=="CERRILLOS")
                {
                    stockCerrillos = idSucursal;
                }else if (stockMaipu == 0 && sucursal.NOMBRE_SUCURSAL == "MAIPU")
                {
                    stockMaipu = idSucursal;
                }
            }

            TipoConsigna IDtipoConsigna = new TipoConsigna();
            List<TipoConsigna> lista_tipos = new List<TipoConsigna>();
            lista_tipos = IDtipoConsigna.Consultar_tipoConsigna();

            foreach (var tipoConsignacion in lista_tipos)
            {
                int idConsigna = tipoConsignacion.ID_TIPO_CONSIGNACION;
                string descripcion = tipoConsignacion.DESCRIPCION;
                if (stockConsignados == 0 && tipoConsignacion.DESCRIPCION == "CONSIGNADO")
                {
                    stockConsignados = idConsigna;
                }
                
            }

            ViewBag.STOCK_TOTAL = stockTotal;
            ViewBag.STOCK_CERRILLOS = ConsultarStock(8); //id maipu
            ViewBag.STOCK_MAIPU = ConsultarStock(1); //id cerrillos
            ViewBag.ID_MAIPU = 1;
            ViewBag.STOCK_CONSIGNADOS = ConsultarStockVehiculosConsignados(stockConsignados);
            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];

            return View();
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


        

        //Metodo encargado de Verificar si el usuario y contraseña existen
        public string LoginUsuario(Usuarios usuario) {
            Usuarios usuariosAutenticado = new Usuarios();

            RespuestaServicio respuestaServicio = new RespuestaServicio();
               try
                {
                    SQLconn sql = new SQLconn();
                    SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand("LOGIN_USUARIO_USUARIOS", conn);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RUT", usuario.RUT);
                        command.Parameters.AddWithValue("@PASSWORD", usuario.PASSWORD);

                        SqlDataReader reader = command.ExecuteReader();

                        bool user= false;
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
                            usuariosAutenticado.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();



                            if (usuariosAutenticado.TIPO_IMAGEN != "")
                            {


                                string extension = usuariosAutenticado.TIPO_IMAGEN;
                                byte[] img = (byte[])reader["IMAGEN"]; //almacena la imagen de la db en un byte[]

                                

                                string IMG = Base64Encoder(img);

                                usuariosAutenticado.IMAGEN = extension + "," + IMG;

                            }
                            else {
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

        public ActionResult ReportesVehiculo()
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
            DateTime fechaActual = DateTime.Now;
            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();

            EstadoVehiculo estadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstados = estadoVehiculo.Consultar_EstadoVehiculo();

            TipoConsigna tipoConsigna = new TipoConsigna();
            List<TipoConsigna> ListadoTipoconsigna = tipoConsigna.Consultar_tipoConsigna();

            Usuarios usuario = new Usuarios();
            List<Usuarios> ListadoUsuarios = usuario.ConsultarUsuarios();

            Disponibilidad disponibilidad = new Disponibilidad();
            List<Disponibilidad> ListadoDisponibilidad = disponibilidad.Consultar_Disponibilidad();

            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Dashboard(fechaActual);

            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;
            ViewBag.ListaSucursal = ListadoSucursal;
            ViewBag.ListaEstados = ListadoEstados;
            ViewBag.ListaTipoConsigna = ListadoTipoconsigna;
            ViewBag.ListaUsuarios = ListadoUsuarios;
            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }

        public ActionResult ReportesVehiculoSucursal(int idSucursal)
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

            DateTime fechaActual = DateTime.Now;
            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();

            EstadoVehiculo estadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstados = estadoVehiculo.Consultar_EstadoVehiculo();

            TipoConsigna tipoConsigna = new TipoConsigna();
            List<TipoConsigna> ListadoTipoconsigna = tipoConsigna.Consultar_tipoConsigna();

            Usuarios usuario = new Usuarios();
            List<Usuarios> ListadoUsuarios = usuario.ConsultarUsuarios();

            Disponibilidad disponibilidad = new Disponibilidad();
            List<Disponibilidad> ListadoDisponibilidad = disponibilidad.Consultar_Disponibilidad();

            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Sucursal(idSucursal);

            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;
            ViewBag.ListaSucursal = ListadoSucursal;
            ViewBag.ListaEstados = ListadoEstados;
            ViewBag.ListaTipoConsigna = ListadoTipoconsigna;
            ViewBag.ListaUsuarios = ListadoUsuarios;
            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }




        //public ActionResult ReportesVehiculoEstado()
        //{
        //    //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

        //    string nombreUsuario = "";
        //    nombreUsuario = (string)Session["NOMBRE_USUARIO"];


        //    ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
        //    ViewBag.IMAGEN = (string)Session["IMAGEN"];
        //    ViewBag.SUCURSAL = Session["SUCURSAL"];

        //    if (nombreUsuario == null || nombreUsuario == "")
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

        //    EstadoVehiculo estadoVehiculo = new EstadoVehiculo();
        //    List<EstadoVehiculo> ListadoEstados = estadoVehiculo.Consultar_EstadoVehiculo();

        //    ViewBag.ListaEstados = ListadoEstados;


        //    List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
        //    Vehiculos BuscaVehiculo = new Vehiculos();

        //    Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Dashboard();
        //    ViewBag.ListadoVehiculos = Lista_Vehiculos;



        //    return View();

        //}

        //public ActionResult ReportesVehiculoUsuarios()
        //{
        //    //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

        //    string nombreUsuario = "";
        //    nombreUsuario = (string)Session["NOMBRE_USUARIO"];


        //    ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
        //    ViewBag.IMAGEN = (string)Session["IMAGEN"];
        //    ViewBag.SUCURSAL = Session["SUCURSAL"];

        //    if (nombreUsuario == null || nombreUsuario == "")
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

        //    Usuarios usuario = new Usuarios();
        //    List<Usuarios> ListadoUsuarios = usuario.ConsultarUsuarios();

        //    ViewBag.ListaUsuarios = ListadoUsuarios;


        //    List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
        //    Vehiculos BuscaVehiculo = new Vehiculos();

        //    Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Dashboard();
        //    ViewBag.ListadoVehiculos = Lista_Vehiculos;



        //    return View();

        //}



        //public ActionResult ReportesVehiculoIngreso()
        //{
        //    //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

        //    string nombreUsuario = "";
        //    nombreUsuario = (string)Session["NOMBRE_USUARIO"];


        //    ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
        //    ViewBag.IMAGEN = (string)Session["IMAGEN"];
        //    ViewBag.SUCURSAL = Session["SUCURSAL"];

        //    if (nombreUsuario == null || nombreUsuario == "")
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

        //    TipoConsigna tipoConsigna = new TipoConsigna();
        //    List<TipoConsigna> ListadoTipoconsigna = tipoConsigna.Consultar_tipoConsigna();

        //    ViewBag.ListaTipoConsigna = ListadoTipoconsigna;


        //    List<Vehiculos> Lista_Vehiculos = new List<Vehiculos>();
        //    Vehiculos BuscaVehiculo = new Vehiculos();

        //    Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Dashboard();
        //    ViewBag.ListadoVehiculos = Lista_Vehiculos;



        //    return View();

        //}

        public string ObtieneVehiculoPorPatente(string patente)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Vehiculos vehiculoConsultado = new Vehiculos();

            try
            {

                vehiculoConsultado = vehiculoConsultado.Consulta_Vehiculo_Por_Patente(patente);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(vehiculoConsultado, Formatting.Indented);
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




        public string obtieneListaVehiculoUsuario(int idUsuario)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoSucursal = new List<Vehiculos>();
            Vehiculos Obj_vehiculoUsuario = new Vehiculos();

            try
            {

                Lista_VehiculoSucursal = Obj_vehiculoUsuario.Consultar_Vehiculos_Usuario(idUsuario);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoSucursal, Formatting.Indented);
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

        public string obtieneListaVehiculoEstado(int ID_Estado)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoEstado = new List<Vehiculos>();
            Vehiculos Obj_vehiculoEstado = new Vehiculos();

            try
            {

                Lista_VehiculoEstado = Obj_vehiculoEstado.Consultar_Vehiculos_Estado(ID_Estado);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoEstado, Formatting.Indented);
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

        public string obtieneListaVehiculoSucursal(int id_Sucursal)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoSucursal = new List<Vehiculos>();
            Vehiculos Obj_vehiculoSucursal = new Vehiculos();

            try
            {

                Lista_VehiculoSucursal = Obj_vehiculoSucursal.Consultar_Vehiculos_Sucursal(id_Sucursal);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoSucursal, Formatting.Indented);
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

        public string obtieneListaVehiculoTipoConsigna(int ID_TipoConsigna)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoSucursal = new List<Vehiculos>();
            Vehiculos Obj_vehiculoTipoConsigna = new Vehiculos();

            try
            {

                Lista_VehiculoSucursal = Obj_vehiculoTipoConsigna.Consultar_Vehiculos_Tipo_Consignacion(ID_TipoConsigna);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoSucursal, Formatting.Indented);
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

        public string obtieneListaVehiculoRangoFecha(DateTime FechaInicial,DateTime FechaFinal)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoRangoFecha= new List<Vehiculos>();
            Vehiculos Obj_vehiculoRangoFecha = new Vehiculos();

            try
            {

                Lista_VehiculoRangoFecha = Obj_vehiculoRangoFecha.Consultar_Vehiculos_Por_FechaIngreso(FechaInicial, FechaFinal);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoRangoFecha, Formatting.Indented);
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

        public string obtieneListaVehiculoDisponibilidad(int ID_DISPONIBILIDAD)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoDisponibilidad = new List<Vehiculos>();
            Vehiculos Obj_vehiculoDisponibilidad = new Vehiculos();

            try
            {

                Lista_VehiculoDisponibilidad = Obj_vehiculoDisponibilidad.Consultar_Vehiculos_Por_Disponibilidad(ID_DISPONIBILIDAD);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoDisponibilidad, Formatting.Indented);
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




        public int  ConsultarStock(int idSucursal)
        {
            int cantidadVehiculo= 0;

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

                return  -1; // si retorna -1 es por que se cayó la consulta
            }


            return cantidadVehiculo;
        }

        public int ConsultarStockSucursal()
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

        public int ConsultarStockVehiculosConsignados(int idTipoConsigna)
        {
            int cantidadVehiculo = 0;

            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_CANTIDAD_VEHICULOS_CONSIGNACION", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_CONSIGNACION", idTipoConsigna);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cantidadVehiculo = Convert.ToInt32(reader["CONSIGNADOS"]);

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

    }
}