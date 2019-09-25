using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Mantenedores
{
    public class TipoConsignaController : Controller
    {
        // GET: TipoConsigna
        public ActionResult TipoConsignaIndex()
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

        public ActionResult TipoConsigna_RolLectura()
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

        public ActionResult TipoConsigna_RolVendedor()
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

        public string ObtieneListadoTipoConsigna()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<TipoConsigna> Lista_tipoConsigna = new List<TipoConsigna>();
            TipoConsigna Obj_tipoConsigna = new TipoConsigna();

            try
            {

                Lista_tipoConsigna = Obj_tipoConsigna.Consultar_tipoConsigna();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_tipoConsigna, Formatting.Indented);
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

        public string AgregarTipoConsignacion(TipoConsigna tipoConsigna)
        {
            RespuestaServicio respuesta;

            respuesta = tipoConsigna.AgregarTipoConsignacion(tipoConsigna);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ObtieneTipoConsignaPorId(TipoConsigna tipoConsigna)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();


            try
            {

                tipoConsigna = tipoConsigna.Consulta_TipoConsignacion_Por_Id(tipoConsigna);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(tipoConsigna, Formatting.Indented);
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

        public string Editar_TipoConsigna(TipoConsigna tipoConsigna)
        {
            RespuestaServicio respuesta;

            respuesta = tipoConsigna.Editar_TipoConsigna(tipoConsigna);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarTipoConsignacion(TipoConsigna tipoConsigna)
        {
            RespuestaServicio respuesta;

            respuesta = tipoConsigna.EliminarTipoConsignacion(tipoConsigna);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }
    }
}