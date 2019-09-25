using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automotriz.Models.Imagenes
{
    public class Imagenes_Sucursal
    {
        public int ID_IMAGEN_SUCURSAL {get;set;}
        public int ID_SUCURSAL { get; set; }
        public byte[] IMAGEN { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public string EXTENSION { get; set; }
        public string DESCRIPCION { get; set; }
     }
}