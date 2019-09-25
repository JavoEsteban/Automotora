using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers
{
    public class FormaPagoController : Controller
    {
        // GET: FormaPago
        public ActionResult FormaPagoIndex()
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

        public string agregarPago(FormasPago formaPago)
        {
            RespuestaServicio respuesta;

            respuesta = formaPago.AgregarFormaPago(formaPago);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }
        public string ObtieneListadoFormas()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<FormasPago> Lista_FormasPago = new List<FormasPago>();
            FormasPago Obj_FormaPago = new FormasPago();

            try
            {

                Lista_FormasPago = Obj_FormaPago.Consultar_Forma_Pago();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_FormasPago, Formatting.Indented);
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

        public string ObtieneFormaPagoPorId(int ID_FORMA_PAGO)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            FormasPago forma = new FormasPago();
            try
            {

                forma = forma.Consulta_FormaPago_Por_Id(ID_FORMA_PAGO);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(forma, Formatting.Indented);
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

        public string Editar_FormaPago(FormasPago forma)
        {
            RespuestaServicio respuesta;

            respuesta = forma.Editar_FormaPago(forma);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarFormaPago(int ID_FORMA_PAGO)
        {
            RespuestaServicio respuesta;
            FormasPago forma = new FormasPago();

            respuesta = forma.EliminarFormaPago(ID_FORMA_PAGO);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }
    }
}