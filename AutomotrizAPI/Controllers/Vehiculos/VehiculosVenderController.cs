using AutomotrizAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomotrizAPI.Controllers.Vehiculos
{
    public class VehiculosVenderController : ApiController
    {
        // POST api/<controller>
        public RespuestaServicio VehiculosPorVender()
        {


            RespuestaServicio OBJRespuestaServicio = new RespuestaServicio();
            Models.Vehiculos OBJVehiculos = new Models.Vehiculos();
            List<Models.Vehiculos> ListaVehiculos = new List<Models.Vehiculos>();

            try
            {
                ListaVehiculos =  OBJVehiculos.Consultar_Vehiculos();

                OBJRespuestaServicio.Respuesta = "OK";
                OBJRespuestaServicio.Descripcion = JsonConvert.SerializeObject(ListaVehiculos, Formatting.Indented);
            }
            catch (Exception ex)
            {
                OBJRespuestaServicio.Respuesta = "NOK";
                OBJRespuestaServicio.Detalle_Error = "No se logro Obtener los vehiculos,Error: " + ex.Message;

            }
            return OBJRespuestaServicio;


        }

        public RespuestaServicio InformacionVehiculo(int ID_VEHICULO)
        {


            RespuestaServicio OBJRespuestaServicio = new RespuestaServicio();
            Models.Vehiculos OBJVehiculos = new Models.Vehiculos();
            

            try
            {
                OBJVehiculos = OBJVehiculos.Consultar_Informacion_vehiculo(ID_VEHICULO);

                OBJRespuestaServicio.Respuesta = "OK";
                OBJRespuestaServicio.Descripcion = JsonConvert.SerializeObject(OBJVehiculos, Formatting.Indented);
            }
            catch (Exception ex)
            {
                OBJRespuestaServicio.Respuesta = "NOK";
                OBJRespuestaServicio.Detalle_Error = "No se logro Obtener los vehiculos,Error: " + ex.Message;

            }
            return OBJRespuestaServicio;


        }
    }
}