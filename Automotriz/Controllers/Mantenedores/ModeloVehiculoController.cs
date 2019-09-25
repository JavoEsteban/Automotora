using Automotriz.connection;
using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Mantenedores
{
    public class ModeloVehiculoController : Controller
    {
        // GET: ModeloVehiculo
        public ActionResult Modelo_Vehiculo()
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

        public ActionResult Modelo_Vehiculo_RolLectura()
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

        public ActionResult Modelo_Vehiculo_RolVendedor()
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

        public string AgregarModelo(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio respuesta;

            respuesta = modeloVehiculo.AgregarModelo(modeloVehiculo);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ObtieneListaModeloPorMarca(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<ModeloVehiculo> Lista_Modelo = new List<ModeloVehiculo>();
            ModeloVehiculo Obj_ModeloVehiculo = new ModeloVehiculo();

            try
            {

                Lista_Modelo = Obj_ModeloVehiculo.consultarModelos(modeloVehiculo);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Modelo, Formatting.Indented);
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

        public string ObtieneModelosTodos(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<ModeloVehiculo> Lista_Modelo = new List<ModeloVehiculo>();
            ModeloVehiculo Obj_ModeloVehiculo = new ModeloVehiculo();

            try
            {

                Lista_Modelo = Obj_ModeloVehiculo.consultarModelosTodos(modeloVehiculo);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Modelo, Formatting.Indented);
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

        public string ObtieneModeloPorId(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();


            try
            {

                modeloVehiculo = modeloVehiculo.Consulta_Modelo_Por_Id(modeloVehiculo);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(modeloVehiculo, Formatting.Indented);
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


        public string Editar_Modelo(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio respuesta;

            respuesta = modeloVehiculo.Editar_Modelo(modeloVehiculo);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarModelo(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio respuesta;

            respuesta = modeloVehiculo.EliminarModelo(modeloVehiculo);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

    }
}