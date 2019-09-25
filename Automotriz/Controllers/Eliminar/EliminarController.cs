using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Eliminar
{
    public class EliminarController : Controller
    {
        // GET: Eliminar
        public ActionResult EliminarVehiculo(int idVehiculo)
        {

            ViewBag.Rol = Session["ROL"];

            return View();
        }
    }
}