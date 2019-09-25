using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Vender
{
    public class VenderController : Controller
    {
        // GET: Vender
        public ActionResult VentaVehiculo(int idVehiculo)
        {
            ViewBag.Rol = Session["ROL"];

            return View();
        }
    }
}