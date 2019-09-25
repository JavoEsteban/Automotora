using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automotriz.Models.Parametro_Nota_Venta
{
    public class Pagos
    {
        public int ID_PAGOS { get; set; }
        public int ID_NOTA_VENTA { get; set; }

        public int ID_FORMA_PAGO { get; set; }
        public string FORMA_PAGO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string BANCO { get; set; }
        public string NUMERO_CUENTA { get; set; }
        public int MONTO { get; set; }
        public int CUOTAS { get; set; }
        public string DIAS_PAGO { get; set; }


    }
}