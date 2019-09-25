using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automotriz.Models.Parametro_Nota_Venta
{
    public class VehiculoPartePago : Vehiculos
    {
        public int ID_VEHICULO_PARTE_PAGO { get; set; }
        public int ID_NOTA_VENTA { get; set; }
    }
}