using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutomotrizAPI.Models;
using Newtonsoft.Json;

namespace AutomotrizAPI.Controllers.Vehiculos
{
    public class VehiculoController : ApiController
    {
     
        

        // POST api/<controller>
        public RespuestaServicio PostTodosLosVehiculos()
        {


            RespuestaServicio OBJRespuestaServicio = new RespuestaServicio();
            Models.Vehiculos OBJVehiculos = new Models.Vehiculos();
            Models.Vehiculo.TipoVehiculo OBJTipoVehiculo = new Models.Vehiculo.TipoVehiculo();
            List<Models.Vehiculo.TipoVehiculo> ListaTipoVehiculos = new List<Models.Vehiculo.TipoVehiculo>();

            try
            {
                ListaTipoVehiculos = OBJTipoVehiculo.Consultar_TipoVehiculo_Vigentes();
                foreach(var tipoVehiculo in ListaTipoVehiculos)
                {

                    int idTipoVehiculo = tipoVehiculo.ID_TIPO_VEHICULO;
                    tipoVehiculo.LISTA_DE_VEHICULOS = OBJVehiculos.Consultar_Vehiculos_Asociados_TipoVehiculo(idTipoVehiculo);
                    
                }
               


                    OBJRespuestaServicio.Respuesta = "OK";
                OBJRespuestaServicio.Descripcion = JsonConvert.SerializeObject(ListaTipoVehiculos, Formatting.Indented);
            }
            catch (Exception ex)
            {
                OBJRespuestaServicio.Respuesta = "NOK";
                OBJRespuestaServicio.Detalle_Error = "No se logro Obtener los vehiculos,Error: " + ex.Message;

            }
            return OBJRespuestaServicio;


        }

        public RespuestaServicio PostImagenPrincipalVehiculo(int ID_VEHICULO)
        {


            RespuestaServicio OBJRespuestaServicio = new RespuestaServicio();
            Models.Vehiculos OBJVehiculos = new Models.Vehiculos();
            string ImgBase64 = "";

            try
            {

                ImgBase64 = OBJVehiculos.BuscarImagenPrincipalDelVehiculo(ID_VEHICULO);
                OBJRespuestaServicio.Respuesta = "OK";
                OBJRespuestaServicio.Descripcion = ImgBase64;
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