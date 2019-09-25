using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Documentos
{
    public class ReImprimirDocumentosController : Controller
    {
        // GET: ReImprimirDocumentos
        public ActionResult ReImpresion()
        {
            ViewBag.Rol = Session["ROL"];

            return View();
        }
    }
}