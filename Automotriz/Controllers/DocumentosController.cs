using Automotriz.Models;
using Automotriz.Models.Parametro_Nota_Venta;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers
{
    public class DocumentosController : Controller
    {
        // GET: Documentos
        public ActionResult NotaVenta()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
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
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            return View();
        }

        public ActionResult FormularioNotaVenta(int idVehiculo)
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
                string nombreUsuario = "";
                nombreUsuario = (string)Session["NOMBRE_USUARIO"];
                ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
                ViewBag.IMAGEN = (string)Session["IMAGEN"];
                ViewBag.SUCURSAL = Session["SUCURSAL"];
                ViewBag.ID_SUCURSAL = Session["ID_SUCURSAL"];
                ViewBag.ID_USUARIO = Session["ID_USUARIO"];
                ViewBag.Rol = Session["ROL"];


                if (nombreUsuario == null || nombreUsuario == "")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            Sucursal sucursal = new Sucursal();
            sucursal = sucursal.Consulta_Sucursal_Por_Id(ViewBag.ID_SUCURSAL);

            List<Sucursal> listaSucursales = new List<Sucursal>();

            listaSucursales = sucursal.Consultar_Sucursales();

            Cliente cliente = new Cliente();
            List<Cliente> listaCliente = new List<Cliente>();
            listaCliente = cliente.Consultar_Clientes();

            Vehiculos vehiculoVendible = new Vehiculos();
            vehiculoVendible = vehiculoVendible.BuscarVehiculoID(idVehiculo);

            List<Vehiculos> listaVehiculosPartePago = new List<Vehiculos>();
            listaVehiculosPartePago = vehiculoVendible.Consultar_Vehiculos_PartePago_Consignados();

            FormasPago formaPago = new FormasPago();
            List<FormasPago> listaFormasPago = new List<FormasPago>();
            listaFormasPago = formaPago.Consultar_Forma_Pago();

            ViewBag.SucursalDocumento = sucursal;
            ViewBag.ListaFormasPago = listaFormasPago;
            ViewBag.VehiculoVendible = vehiculoVendible;
            ViewBag.ListaClientes = listaCliente;
            ViewBag.ListaSucursales = listaSucursales;
            ViewBag.ListaVehiculosPartePago = listaVehiculosPartePago;
            return View();
        }


        public ActionResult imprimirNotaVenta(int id_nota_venta) {

            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
                string nombreUsuario = "";
                nombreUsuario = (string)Session["NOMBRE_USUARIO"];
                ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
                ViewBag.IMAGEN = (string)Session["IMAGEN"];
                ViewBag.SUCURSAL = Session["SUCURSAL"];
                ViewBag.ID_USUARIO = Session["ID_USUARIO"];
                ViewBag.Rol = Session["ROL"];

                if (nombreUsuario == null || nombreUsuario == "")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            NotaVenta notaVenta = new NotaVenta();
            List<Pagos> listaPagos = new List<Pagos>();
            List<VehiculoPartePago> listadoVehiculosPartePago = new List<VehiculoPartePago>();
            List<Descuentos> listaDescuentos = new List<Descuentos>();

            notaVenta = notaVenta.Consulta_Nota_Venta_Por_Id(id_nota_venta);
            listaPagos = notaVenta.Consultar_Pagos(id_nota_venta);
            listadoVehiculosPartePago = notaVenta.Consultar_VehiculosPartePago(id_nota_venta);
            listaDescuentos = notaVenta.Consultar_Descuentos(id_nota_venta);

            ViewBag.NotaVenta = notaVenta;
            ViewBag.ListaPagos = listaPagos;
            ViewBag.ListadoVehiculosPartePago = listadoVehiculosPartePago;
            ViewBag.ListaDescuentos = listaDescuentos;
            return View();
        }

        public ActionResult imprimirNotaVentaIdVehiculo(int idVehiculo)
        {

            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
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
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            int id_nota_venta = 0;
            NotaVenta notaVenta = new NotaVenta();

            id_nota_venta = notaVenta.ConsultarIdNotaVentaPorIdVehiculo(idVehiculo);


            List<Pagos> listaPagos = new List<Pagos>();
            List<VehiculoPartePago> listadoVehiculosPartePago = new List<VehiculoPartePago>();
            List<Descuentos> listaDescuentos = new List<Descuentos>();

            notaVenta = notaVenta.Consulta_Nota_Venta_Por_Id(id_nota_venta);
            listaPagos = notaVenta.Consultar_Pagos(id_nota_venta);
            listadoVehiculosPartePago = notaVenta.Consultar_VehiculosPartePago(id_nota_venta);
            listaDescuentos = notaVenta.Consultar_Descuentos(id_nota_venta);

            ViewBag.NotaVenta = notaVenta;
            ViewBag.ListaPagos = listaPagos;
            ViewBag.ListadoVehiculosPartePago = listadoVehiculosPartePago;
            ViewBag.ListaDescuentos = listaDescuentos;
            return View();


        }

        public ActionResult ContratoConsignacion(int idContrato)
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
                string nombreUsuario = "";
                nombreUsuario = (string)Session["NOMBRE_USUARIO"];
                ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
                ViewBag.IMAGEN = (string)Session["IMAGEN"];
                ViewBag.SUCURSAL = Session["SUCURSAL"];
                ViewBag.DIRECCION_SUCURSAL = Session["DIRECCION_SUCURSAL"];
                ViewBag.Rol = Session["ROL"];

                if (nombreUsuario == null || nombreUsuario == "")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
            ContratoConsignacion contrato = new ContratoConsignacion();
            contrato = contrato.Consulta_Contrato_por_ID(idContrato);

            ViewBag.ContratoConsignacion = contrato;
            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            return View();
        }

        [HttpPost]
        public string AgregarNotaVenta(NotaVenta NotaVenta, List<Vehiculos> ListaVehiculos, List<Pagos> ListaPagos, List<Descuentos> ListaDescuentos)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            string nombreUsuario = "";
            int idUsuario = 0;

            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
                idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());
                nombreUsuario = (string)Session["NOMBRE_USUARIO"];
                ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
                ViewBag.IMAGEN = (string)Session["IMAGEN"];
                ViewBag.SUCURSAL = Session["SUCURSAL"];

                if (nombreUsuario == null || nombreUsuario == "")
                {


                    respuestaServicio.Respuesta = "NOK";
                    respuestaServicio.Descripcion = "";
                    respuestaServicio.Detalle_Error = "Debe iniciar sesión para poder realizar una venta";

                    return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Error de conexión "+ ex;

                string resultadoCatch = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
                return resultadoCatch;
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            
            Vehiculos OBJVehiculos = new Vehiculos();
           



            OBJVehiculos = OBJVehiculos.BuscarVehiculoID(NotaVenta.ID_VEHICULOS);

            respuestaServicio = NotaVenta.agregarNotaVenta(NotaVenta, ListaVehiculos, ListaPagos, ListaDescuentos,nombreUsuario,idUsuario, OBJVehiculos.PATENTE);

            string resultado = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            return resultado;
        }


       

        public string AgregarContrato(ContratoConsignacion contrato)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            string nombreUsuario = "";
            int idUsuario = 0;

            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            try
            {
                idUsuario = Convert.ToInt32(Session["ID_USUARIO"].ToString());
                nombreUsuario = (string)Session["NOMBRE_USUARIO"];
                ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
                ViewBag.IMAGEN = (string)Session["IMAGEN"];
                ViewBag.SUCURSAL = Session["SUCURSAL"];

                if (nombreUsuario == null || nombreUsuario == "")
                {


                    respuestaServicio.Respuesta = "NOK";
                    respuestaServicio.Descripcion = "";
                    respuestaServicio.Detalle_Error = "Debe iniciar sesión para poder realizar una consignacion";

                    return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "Error de conexión " + ex;
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************


            respuestaServicio = contrato.agregarContrato(contrato);

            string resultado = JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
            return resultado;
        }





    }
}