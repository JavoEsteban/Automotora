using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotrizAPI.Models
{
    public class ImagenVehiculo
    {
        public int ID_IMAGEN { get; set; }
        public int ID_VEHICULO { get; set; }
        public string IMAGEN { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public string EXTENSION { get; set; }
        public int VIGENCIA { get; set; }
        public int POSICION { get; set; }

    }
}