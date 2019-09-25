using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Mantenedores
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Usuarios()
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
            if (ViewBag.Rol != "ADMINISTRADOR")
            {
                return RedirectToAction("Login", "Login");
            }


            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            return View();
        }

        [HttpPost]
        public string AgregarUsuario(Usuarios usuario)
        {
            RespuestaServicio respuesta;

            respuesta = usuario.AgregarUsuarios(usuario);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }


        public string ObtieneListaUsuarios()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Usuarios> Lista_Usuarios = new List<Usuarios>();
            Usuarios Obj_Usuario = new Usuarios();

            try
            {

                Lista_Usuarios = Obj_Usuario.ConsultarUsuarios();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Usuarios, Formatting.Indented);
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

        public string ObtieneUsuarioPorId(string idUsuario)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            Usuarios usuarioConsultado = new Usuarios();

            try
            {

                usuarioConsultado = usuarioConsultado.Consulta_Usuario_Por_Id(idUsuario);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(usuarioConsultado, Formatting.Indented);
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

        public string Editar_Usuario(Usuarios usuario)
        {
            RespuestaServicio respuesta;

            respuesta = usuario.Editar_Usuario(usuario);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarUsuario(Usuarios usuario)
        {
            RespuestaServicio respuesta;

            respuesta = usuario.EliminarUsuario(usuario);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ValidaRutUsuario(string Rut)
        {
            //Se instancian objetos
            RespuestaServicio Respuesta;
            Usuarios usuario = new Usuarios();

            //Se ejecuta la validacion de rut
            Respuesta = usuario.ValidarRutUsuario(Rut);

            //se retorna la respuesta
            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented);
        }
    }
}