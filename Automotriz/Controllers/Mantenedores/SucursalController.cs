using Automotriz.connection;
using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult IndexSucursal()
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

        public ActionResult Sucursal_RolLectura()
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

        public ActionResult Sucursal_RolVendedor()
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



        public string ObtienePreviewSucursal()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Sucursal Obj_Sucursal = new Sucursal();

            List<Sucursal> Lista_Sucursales = new List<Sucursal>();

            try
            {

                Lista_Sucursales = Obj_Sucursal.Consultar_sucursales_preview();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Sucursales, Formatting.Indented);
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

        public string ObtieneListaSucursales()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Sucursal> Lista_Sucursales = new List<Sucursal>();
            Sucursal Obj_Sucursal = new Sucursal();

            try
            {

                Lista_Sucursales = Obj_Sucursal.Consultar_Sucursales();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Sucursales, Formatting.Indented);
                Respuesta.Detalle_Error = "";

            }
            catch (Exception Err)
            {

                Respuesta.Respuesta = "NOK";
                Respuesta.Descripcion = "";
                Respuesta.Detalle_Error = "No fue posible cargar el listado de Sucursales" + Err.Message;
            }

            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented); ;
        }



        public string AgregarSucursal(Sucursal sucursal)
        {
            RespuestaServicio respuesta;

            respuesta = sucursal.AgregarSucursales(sucursal);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ObtieneSucursalPorID(int idSucursal)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Sucursal sucursalConsultada = new Sucursal();

            try
            {

                sucursalConsultada = sucursalConsultada.Consulta_Sucursal_Por_Id(idSucursal);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(sucursalConsultada, Formatting.Indented);
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



        public string Editar_Sucursal(Sucursal sucursal)
        {
            RespuestaServicio respuesta;

            respuesta = sucursal.Editar_Sucursal(sucursal);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarSucursal(Sucursal sucursal)
        {
            RespuestaServicio respuesta;

            respuesta = sucursal.EliminarSucursal(sucursal);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string guardarEnCarpeta(byte[] imagen, string nombreSuc, string extension)
        {
            string url = Server.MapPath("~/imagenesGuardar/imagenesSucursal");

            bool saberExistenciaCarpeta;

            saberExistenciaCarpeta = Directory.Exists(url + "/" + nombreSuc);
            if (!saberExistenciaCarpeta)
            {
                Directory.CreateDirectory(url + "/" + nombreSuc);
            }
            else
            {
                Directory.Delete(url + "/" + nombreSuc, true);
                Directory.CreateDirectory(url + "/" + nombreSuc);
            }

            using (Image image = Image.FromStream(new MemoryStream(imagen)))
            {
                image.Save(url + "/" + nombreSuc + "/" + nombreSuc + ".jpg", ImageFormat.Jpeg);
            }
            

            return "";
        }
    }

    
}
        

