using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class RespuestaServicio
    {
      
            public String Respuesta; //AQUI PUEDES ASIGNAR DOS STRINGS DIFERENTES SEGUN LA RESPUESTA:  "OK" y "NOK"
            public String Descripcion; //AQUI ES DONDE DEBES ASIGNAR LOS DATOS QUE ESTES ENVIANDO, PUEDES ENVIAR UN OBJETO SERIALIZADO A JSON

            public String Detalle_Error; //AQUI DEBES ASIGNAR EL ERROR EN CASO DE QUE OCURRA PARA DESPLEGAR EN EL FRONT EL PROBLEMA DE LA FUNCION

        
    }
}