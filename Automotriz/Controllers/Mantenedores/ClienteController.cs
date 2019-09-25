using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Mantenedores
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult ClienteIndex()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            Cliente Obj_Cliente = new Cliente();
            List<Cliente> Lista_Clientes = new List<Cliente>();
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

            Lista_Clientes = Obj_Cliente.Consultar_Clientes();

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            return View();
        }


        public ActionResult Cliente_RolLectura()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            Cliente Obj_Cliente = new Cliente();
            List<Cliente> Lista_Clientes = new List<Cliente>();
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

            Lista_Clientes = Obj_Cliente.Consultar_Clientes();

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            return View();
        }

        public ActionResult Cliente_RolVendedor()
        {
            
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            Cliente Obj_Cliente = new Cliente();
            List<Cliente> Lista_Clientes = new List<Cliente>();
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
            Lista_Clientes = Obj_Cliente.Consultar_Clientes();

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            return View();
        }

        public string ObtieneListadoClientes()
        {
            RespuestaServicio Respuesta = new RespuestaServicio();
            List<Cliente> Lista_Clientes = new List<Cliente>();
            Cliente Obj_Cliente = new Cliente();

            try
            {

                Lista_Clientes = Obj_Cliente.Consultar_Clientes();

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(Lista_Clientes, Formatting.Indented);
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

        public string AgregarCliente(Cliente cliente)
        {
            RespuestaServicio respuesta;

            respuesta = cliente.AgregarClientes(cliente);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string ValidaRut(string Rut)
        {
            //Se instancian objetos
            RespuestaServicio Respuesta;
            Cliente cliente = new Cliente();

            //Se ejecuta la validacion de rut
            Respuesta = cliente.ValidarClienteRut(Rut);

            //se retorna la respuesta
            return JsonConvert.SerializeObject(Respuesta, Formatting.Indented); 
        }

        public string ObtieneClientePorId(Cliente cliente)
        {
            RespuestaServicio Respuesta = new RespuestaServicio();


            try
            {

                cliente = cliente.Consulta_Cliente_Por_Id(cliente);

                Respuesta.Respuesta = "OK";
                Respuesta.Descripcion = JsonConvert.SerializeObject(cliente, Formatting.Indented);
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

        public string Editar_Cliente(Cliente cliente)
        {
            RespuestaServicio respuesta;

            respuesta = cliente.Editar_Cliente(cliente);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }

        public string EliminarCliente(Cliente cliente)
        {
            RespuestaServicio respuesta;

            respuesta = cliente.EliminarCliente(cliente);

            string resultado = JsonConvert.SerializeObject(respuesta, Formatting.Indented);
            return resultado;
        }


    }
}