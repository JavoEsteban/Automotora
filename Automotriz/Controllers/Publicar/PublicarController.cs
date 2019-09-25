using Automotriz.connection;
using Automotriz.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Publicar
{
    public class PublicarController : Controller
    {
        // metodo encargado de buscar toda la informacion del vehiculo
        public ActionResult PublicarVehiculo(int idVehiculo)
        { //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************

            string nombreUsuario = "";
            nombreUsuario = (string)Session["NOMBRE_USUARIO"];


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];
            ViewBag.Rol = Session["ROL"];


            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Home");
            }

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************

            //Busca el vehiculo para cargar en la vista
            Vehiculos BuscaVehiculo = new Vehiculos();

           
            BuscaVehiculo = BuscaVehiculo.BuscarVehiculoID(idVehiculo);

            //Obtiene imagenes del vehiculo
            List<ImagenVehiculo> ImagenesVehiculo = obetenerTodasLasImagenes(BuscaVehiculo.ID_VEHICULO);


            ViewBag.Vehiculo = BuscaVehiculo;
            ViewBag.ImagenesVehiculo = ImagenesVehiculo;

            //CARGAR COMBOS PARA LA VISTA

            Marca Marca = new Marca();
            List<Marca> ListaMarcas = Marca.Consultar_Marcas();

            ViewBag.ListaMarcas = ListaMarcas;

            TipoVehiculo TiposVehiculo = new TipoVehiculo();
            List<TipoVehiculo> ListaTipoVehiculos = TiposVehiculo.Consultar_TipoVehiculo();

            ViewBag.ListaTipoVehiculo = ListaTipoVehiculos;


            TipoTransmision TipoTransmision = new TipoTransmision();
            List<TipoTransmision> ListaTransmision = TipoTransmision.Consultar_Transmision();

            ViewBag.ListaTipoTransmision = ListaTransmision;


            TipoTraccion TipoTraccion = new TipoTraccion();
            List<TipoTraccion> ListaTraccion = TipoTraccion.Consultar_Traccion();

            ViewBag.ListaTipoTraccion = ListaTraccion;

            TipoConsigna TipoConsignacion = new TipoConsigna();
            List<TipoConsigna> ListaConsigna = TipoConsignacion.Consultar_tipoConsigna();

            ViewBag.ListaConsignacion = ListaConsigna;

            TipoCombustible TipoCombustible = new TipoCombustible();
            List<TipoCombustible> ListadoCombustible = TipoCombustible.Consultar_TipoCombustible();

            ViewBag.ListaCombustible = ListadoCombustible;


            Color Color = new Color();
            List<Color> ListadoColor = Color.Consultar_Color();

            ViewBag.ListaColor = ListadoColor;


            Sucursal Sucursal = new Sucursal();
            List<Sucursal> ListadoSucursal = Sucursal.Consultar_Sucursales();

            ViewBag.ListaSucursal = ListadoSucursal;

            Disponibilidad Disponibilidad = new Disponibilidad();
            List<Disponibilidad> ListadoDisponibilidad = Disponibilidad.Consultar_Disponibilidad();

            ViewBag.ListaDisponibilidad = ListadoDisponibilidad;

            EstadoVehiculo EstadoVehiculo = new EstadoVehiculo();
            List<EstadoVehiculo> ListadoEstado = EstadoVehiculo.Consultar_EstadoVehiculo();

            ViewBag.ListaEstado = ListadoEstado;


            Usuarios Usuarios = new Usuarios();
            List<Usuarios> ListadoUsuarios = Usuarios.ConsultarUsuarios();

            ViewBag.ListaUsuarios = ListadoUsuarios;

            //Se instancia Objeto bitacora
            Bitacora_Vehiculo OBJ_bitacora = new Bitacora_Vehiculo();

            //Se retornara lista con datos de la bitacora
            ViewBag.Bitacora = OBJ_bitacora.Consultar_Bitacora(idVehiculo);

            //FIN CARGA COMBOS PARA LA VISTA


            return View();
        }

        //Metodo encargado de buscar las imagenes 
        public List<ImagenVehiculo> obetenerTodasLasImagenes(int ID_VEHICULO)
        {
            List<ImagenVehiculo> imagenesVehiculos = new List<ImagenVehiculo>();

            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("TRAER_IMAGENES_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ImagenVehiculo imagenVehiculo = new ImagenVehiculo();

                        byte[] imagenArray;
                        string tipoArchivo = "";
                        string base64 = "";

                        imagenVehiculo.ID_IMAGEN = Convert.ToInt32(reader["ID_IMAGEN"]);
                        imagenVehiculo.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO"]);
                        imagenVehiculo.TIPO_IMAGEN = reader["TIPO_IMAGEN"].ToString();
                        imagenVehiculo.EXTENSION = reader["EXTENSION"].ToString();
                        imagenVehiculo.POSICION = Convert.ToInt32(reader["POSICION"]);

                        imagenArray = (byte[])reader["IMAGEN"];


                        tipoArchivo = imagenVehiculo.TIPO_IMAGEN;
                        base64 = Convert.ToBase64String(imagenArray);
                        imagenVehiculo.IMAGEN = tipoArchivo + "," + base64;

                        imagenesVehiculos.Add(imagenVehiculo);
                    }

                }

            }
            catch (Exception ex)
            {
                imagenesVehiculos = new List<ImagenVehiculo>();

            }
            return imagenesVehiculos;
        }
    }
}