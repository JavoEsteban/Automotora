using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Mantenedores
{
    public class TipoVehiculoController : Controller
    {
        // GET: TipoVehiculo
        public ActionResult TipoVehiculoIndex()
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

            return View();
        }

        public ActionResult TipoVehiculo_RolLectura()
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

            return View();
        }

        public ActionResult TipoVehiculo_RolVendedor()
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

            return View();
        }

        public string ObtieneListadoTipoVehiculo()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<TipoVehiculo> Lista_TipoVehiculo = new List<TipoVehiculo>();
            TipoVehiculo Obj_tipoVehiculo = new TipoVehiculo();

            try
            {

                Lista_TipoVehiculo = Obj_tipoVehiculo.Consultar_TipoVehiculo();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_TipoVehiculo, Formatting.Indented);
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

        public string AgregarTipo_Vehiculo(TipoVehiculo tipoVehiculo)
        {
            RespuestaServicio respuesta;

            respuesta = tipoVehiculo.AgregarTipoVehiculo(tipoVehiculo);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ObtieneTipoVehiculoPorId(TipoVehiculo tipoVehiculo)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();


            try
            {

                tipoVehiculo = tipoVehiculo.Consulta_TipoVehiculo_Por_Id(tipoVehiculo);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(tipoVehiculo, Formatting.Indented);
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

        public string Editar_tipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            RespuestaServicio respuesta;

            respuesta = tipoVehiculo.Editar_TipoVehiculo(tipoVehiculo);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarTipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            RespuestaServicio respuesta;

            respuesta = tipoVehiculo.EliminarTipoVehiculo(tipoVehiculo);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

    }
}