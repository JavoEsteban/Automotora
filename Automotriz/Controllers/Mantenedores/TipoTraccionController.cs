using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Mantenedores
{
    public class TipoTraccionController : Controller
    {
        // GET: TipoTraccion
        public ActionResult tipoTraccionIndex()
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

        public ActionResult tipoTraccion_RolLectura()
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

        public ActionResult tipoTraccion_RolVendedor()
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

        public string ObtieneListadoTraccion()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<TipoTraccion> Lista_Traccion = new List<TipoTraccion>();
            TipoTraccion Obj_Traccion = new TipoTraccion();

            try
            {

                Lista_Traccion = Obj_Traccion.Consultar_Traccion();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Traccion, Formatting.Indented);
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

        public string AgregarTraccion(TipoTraccion traccion)
        {
            RespuestaServicio respuesta;

            respuesta = traccion.agregarTraccion(traccion);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ObtieneTraccionPorId(TipoTraccion traccion)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();


            try
            {

                traccion = traccion.Consulta_Traccion_Por_Id(traccion);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(traccion, Formatting.Indented);
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

        public string Editar_Traccion(TipoTraccion traccion)
        {
            RespuestaServicio respuesta ;

            respuesta = traccion.Editar_Traccion(traccion);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarTraccion(TipoTraccion traccion)
        {
            RespuestaServicio respuesta;

            respuesta = traccion.EliminarTraccion(traccion);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }
    }
}