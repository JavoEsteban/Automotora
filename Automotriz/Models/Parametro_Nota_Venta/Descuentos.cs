using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automotriz.Models.Parametro_Nota_Venta
{
    public class Descuentos
    {
        public int ID_DESCUENTOS { get; set; }
        public int ID_NOTA_VENTA { get; set; }
        public string DESCRIPCION { get; set; }
        public int MONTO { get; set; }

    }
}