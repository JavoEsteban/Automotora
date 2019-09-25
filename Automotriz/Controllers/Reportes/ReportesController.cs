using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Automotriz.connection;
using Automotriz.Models;
using Newtonsoft.Json;

namespace Automotriz.Controllers.Reportes
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult ReportesVehiculo()
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
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            string nombreSucursal = "";
            DateTime fechaActual = DateTime.Now;
            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();
            Sucursal = Sucursal.Consulta_Sucursal_Por_Id(idSucursal);
            nombreSucursal = Sucursal.NOMBRE_SUCURSAL;

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

            ViewBag.NombreSucursal = nombreSucursal;
            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;
            ViewBag.ListaSucursal = ListadoSucursal;
            ViewBag.ListaEstados = ListadoEstados;
            ViewBag.ListaTipoConsigna = ListadoTipoconsigna;
            ViewBag.ListaUsuarios = ListadoUsuarios;
            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }

        public ActionResult ReportesVehiculoCliente()
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
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Reportes_Vehiculos_Cliente(0,0,0,0);

            Cliente cliente = new Cliente();
            List<Cliente> listaClientes = cliente.Consultar_Clientes();

            ViewBag.ListaClientes = listaClientes;
            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;
            ViewBag.ListaSucursal = ListadoSucursal;
            ViewBag.ListaEstados = ListadoEstados;
            ViewBag.ListaTipoConsigna = ListadoTipoconsigna;
            ViewBag.ListaUsuarios = ListadoUsuarios;
            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }


        public ActionResult ReportesVehiculoPorSucursal(int idSucursal)
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



            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Sucursal(idSucursal);

         
            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }

        public ActionResult ReportesVehiculosPorTipoConsignacion(int idConsignacion)
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
            //*******************************************************************************

            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Tipo_Consignacion(idConsignacion);


            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }

        public ActionResult ReportesVehiculosPropiosYPorParteDePago()
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
            //*******************************************************************************

            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Propios_PartePago();


            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }

        public ActionResult ReportesVehiculosConsignadosPorVendedor()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            DateTime fechaActual = DateTime.Now;
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

            Usuarios OBJ_Usuarios = new Usuarios();
            List<Usuarios> listaDeUsuarios = OBJ_Usuarios.ConsultarUsuarios();



            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Consignados(fechaActual);

            ViewBag.ListaUsuarios = listaDeUsuarios;
            ViewBag.ListadoVehiculos = Lista_Vehiculos;

            return View();

        }
        public ActionResult ReportesVehiculoPorCliente()
        {
            DateTime fechaActual = DateTime.Now;
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

            Cliente cliente = new Cliente();
            List<Cliente> listaClientes = cliente.Consultar_Clientes();

            

            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Consignados_Clientes(fechaActual);


            ViewBag.ListadoVehiculos = Lista_Vehiculos;
            ViewBag.ListaClientes = listaClientes;

            return View();

        }

        public ActionResult ReportesVehiculosRetirados()
        {
            DateTime fechaActual = DateTime.Now;
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

           



            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Retirados(fechaActual);


            ViewBag.ListadoVehiculos = Lista_Vehiculos;


            return View();

        }

        public ActionResult ReportesVehiculoPorVendedor()
        {
            DateTime fechaActual = DateTime.Now;
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

            Cliente cliente = new Cliente();
            List<Cliente> listaClientes = cliente.Consultar_Clientes();



            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Consignados_Clientes(fechaActual);


            ViewBag.ListadoVehiculos = Lista_Vehiculos;
            ViewBag.ListaClientes = listaClientes;

            return View();

        }
        public ActionResult ReportesVehiculosVendidosPorUsuario()
        {
            DateTime fechaActual = DateTime.Now;
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

            Usuarios usuarios = new Usuarios();
            List<Usuarios> listaUsuarios = usuarios.ConsultarUsuarios();



            Vehiculos BuscaVehiculo = new Vehiculos();
            List<Vehiculos> Lista_Vehiculos = BuscaVehiculo.Consultar_Vehiculos_Vendidos();


            ViewBag.ListadoVehiculos = Lista_Vehiculos;
            ViewBag.ListaUsuarios = listaUsuarios;

            return View();

        }


        public string consultaVehiculosVendidosPorUsuario(int idUsuario)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoCliente = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();
            DateTime fechaActual = DateTime.Now;
            try
            {
                if (idUsuario != 0)
                {
                    Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Vendidos_Por_Usuario(idUsuario);
                }
                else
                {
                    Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Vendidos();
                }
                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoCliente, Formatting.Indented);
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

        public string consultaVehiculoPorCliente(int idCliente)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoCliente = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();
            DateTime fechaActual = DateTime.Now;
            try
            {
                if (idCliente != 0)
                {
                    Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Por_IDCliente(idCliente);
                }
                else
                {
                    Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Consignados_Clientes(fechaActual);
                }
                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoCliente, Formatting.Indented);
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

        public string consultaVehiculoPorUsuario(int idUsuario)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoCliente = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();
            DateTime fechaActual = DateTime.Now;
            try
            {
                if (idUsuario != 0)
                {
                    Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Usuario(idUsuario);
                }
                else
                {
                    Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Consignados(fechaActual);
                }
                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoCliente, Formatting.Indented);
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

        public string consultaVehiculosConsignadosPorID_Usuario(int idUusuario)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoCliente = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();

            try
            {

                Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Vehiculos_Por_IDCliente(idUusuario);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoCliente, Formatting.Indented);
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

        public string obtieneFiltroVehiculoSucursal(int idSucursal, int idUsuario, int idIngreso, int idDisponibilidad, int idEstado, string fechaInicial, string fechaFinal)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoSucursal = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();

            try
            {

                Lista_VehiculoSucursal = Obj_vehiculoFiltro.Consultar_Vehiculos_Filtro_Sucursal(idSucursal, idUsuario, idIngreso, idDisponibilidad, idEstado, fechaInicial, fechaFinal);

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
        
        public string obtieneFiltroVehiculoCliente(int idCliente, int idIngreso, int idDisponibilidad, int idEstado)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoCliente = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();

            try
            {

                Lista_VehiculoCliente = Obj_vehiculoFiltro.Consultar_Reportes_Vehiculos_Cliente(idCliente, idIngreso, idDisponibilidad, idEstado);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_VehiculoCliente, Formatting.Indented);
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

        public string obtieneFiltroVehiculo(int idSucursal, int idUsuario, int idIngreso, int idDisponibilidad, int idEstado, string fechaInicial, string fechaFinal)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoSucursal = new List<Vehiculos>();
            Vehiculos Obj_vehiculoFiltro = new Vehiculos();

            try
            {

                Lista_VehiculoSucursal = Obj_vehiculoFiltro.Consultar_Reportes_Vehiculos(idSucursal, idUsuario, idIngreso, idDisponibilidad, idEstado, fechaInicial, fechaFinal);

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

        public string obtieneListaVehiculoRangoFecha(DateTime FechaInicial, DateTime FechaFinal)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Vehiculos> Lista_VehiculoRangoFecha = new List<Vehiculos>();
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




        public int ConsultarStock(int idSucursal)
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