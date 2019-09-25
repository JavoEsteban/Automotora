using Automotriz.connection;
using Automotriz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automotriz.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Administrador()
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            Vehiculos OBJvehiculo = new Vehiculos();
            NotaVenta OBJNotaDeVenta = new NotaVenta();
            Usuarios OBJUsuarios = new Usuarios();
            Marca OBJMArca = new Marca();
            List<Marca> ListaMarcasMasVendidas = new List<Marca>();
            List<NotaVenta> ListaVentasSemana = new List<NotaVenta>();
            List<Usuarios> ListaMejoresVendedoresDelMes = new List<Usuarios>();
            string nombreUsuario = "";
            int stockTotal= 0;
            int cantidadVehiculosPropios = 0;
            int cantidadVehiculosConsignados = 0;
           
           
            //**Comienza PROCESO DE VALIDACION AUTENTIFICACION *******************

            nombreUsuario = (string)Session["NOMBRE_USUARIO"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
            }


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];
            ViewBag.Rol = Session["ROL"];

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************


            stockTotal = OBJvehiculo.Consultar_Stock_Total_De_Vehiculos();
            cantidadVehiculosPropios = OBJvehiculo.Consultar_Cantidad_Vehiculos_Propios_Y_ParteDePago();
            cantidadVehiculosConsignados = OBJvehiculo.Consultar_Cantidad_Vehiculos_Por_Tipo_Consignacion(5);
            ListaVentasSemana = OBJNotaDeVenta.ConsultarVentasPordiaDelMesActual();
            ListaMejoresVendedoresDelMes = OBJUsuarios.ConsultaTop3Vendedores();
            ListaMarcasMasVendidas = OBJMArca.Consultar_TOP5_MARCAS_MAS_VENDIDAS();

            DateTime fechaActual = DateTime.Today;

            string mesActual = string.Format("{0}", fechaActual.ToString("MMMM"));

            String mesActualCapitalizado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(mesActual);

            ViewBag.mesActual = mesActualCapitalizado;

            ViewBag.MarcasMasVendidasTop5 = ListaMarcasMasVendidas;
            ViewBag.MejoresVendedoresDelMes = ListaMejoresVendedoresDelMes;
            ViewBag.VentasSemana = ListaVentasSemana;
            ViewBag.Propios = cantidadVehiculosPropios;
            ViewBag.Consignados = cantidadVehiculosConsignados;
            ViewBag.STOCK_TOTAL = stockTotal;
            ViewBag.ID_MAIPU = 1;
            ViewBag.ID_CERRILLOS = 8;
            ViewBag.IDConsignados = 5;
            ViewBag.IDPropios = 4;
            ViewBag.STOCK_CERRILLOS = OBJvehiculo.Consultar_Stock_Total_De_Vehiculos_Por_Sucursal(8); //id maipu
            ViewBag.STOCK_MAIPU = OBJvehiculo.Consultar_Stock_Total_De_Vehiculos_Por_Sucursal(1); //id cerrillos

            return View();
        }

        public ActionResult DashboardVendedor(int idUsuario)
        {
            //***PROCESO DE VALIDACION AUTENTIFICACION + Session de USUARIO  *******************
            Vehiculos OBJvehiculo = new Vehiculos();
            NotaVenta OBJNotaDeVenta = new NotaVenta();
            Usuarios OBJUsuarios = new Usuarios();
            Marca OBJMArca = new Marca();
            List<Marca> ListaMarcasMasVendidas = new List<Marca>();
            List<NotaVenta> ListaVentasSemana = new List<NotaVenta>();
            List<Usuarios> ListaMejoresVendedoresDelMes = new List<Usuarios>();
            string nombreUsuario = "";
            int stockTotal = 0;
            int cantidadVehiculosPropios = 0;
            int cantidadVehiculosConsignados = 0;


            //**Comienza PROCESO DE VALIDACION AUTENTIFICACION *******************

            nombreUsuario = (string)Session["NOMBRE_USUARIO"];

            if (nombreUsuario == null || nombreUsuario == "")
            {
                return RedirectToAction("Login", "Login");
            }


            ViewBag.NOMBRE_USUARIO = Session["NOMBRE_USUARIO"];
            ViewBag.IMAGEN = (string)Session["IMAGEN"];
            ViewBag.SUCURSAL = Session["SUCURSAL"];
            ViewBag.Rol = Session["ROL"];
            ViewBag.ID_Usuario= Session["ID_USUARIO"];

            //**FIN PROCESO DE VALIDACION AUTENTIFICACION *******************
            DateTime fechaActual = DateTime.Today;

            string mesActual = string.Format("{0}",fechaActual.ToString("MMMM"));

            String mesActualCapitalizado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(mesActual);

            ViewBag.mesActual = mesActualCapitalizado;
            stockTotal = OBJvehiculo.Consultar_Stock_Total_De_Vehiculos();
            cantidadVehiculosPropios = OBJvehiculo.Consultar_Cantidad_Vehiculos_Propios_Y_ParteDePago();
            cantidadVehiculosConsignados = OBJvehiculo.Consultar_Cantidad_Vehiculos_Por_Tipo_Consignacion(5);
            ListaVentasSemana = OBJNotaDeVenta.ConsultarVentasPordiaDelMesActual();
            ListaMejoresVendedoresDelMes = OBJUsuarios.ConsultaTop3Vendedores();
            ListaMarcasMasVendidas = OBJMArca.Consultar_TOP5_MARCAS_MAS_VENDIDAS();

            ViewBag.MarcasMasVendidasTop5 = ListaMarcasMasVendidas;
            ViewBag.MejoresVendedoresDelMes = ListaMejoresVendedoresDelMes;
            ViewBag.VentasSemana = ListaVentasSemana;
            ViewBag.Propios = cantidadVehiculosPropios;
            ViewBag.Consignados = cantidadVehiculosConsignados;
            ViewBag.STOCK_TOTAL = stockTotal;
            ViewBag.ID_MAIPU = 1;
            ViewBag.ID_CERRILLOS = 8;
            ViewBag.IDConsignados = 5;
            ViewBag.IDPropios = 4;
            ViewBag.STOCK_CERRILLOS = OBJvehiculo.Consultar_Stock_Total_De_Vehiculos_Por_Sucursal(8); //id maipu
            ViewBag.STOCK_MAIPU = OBJvehiculo.Consultar_Stock_Total_De_Vehiculos_Por_Sucursal(1); //id cerrillos

            return View();
        }


        public ActionResult PruebasDeRendimiento()
        {
            Models.Parametro_Generico.Parametro_Genericos OBJParametroGenerico = new Models.Parametro_Generico.Parametro_Genericos();

            OBJParametroGenerico.cambiarTamanioDeImagenes();

            return View();
        }

        //=========================pruebas desarrollo
        public string imagenPrincipal()
        {
            string imagen = "";
            ImagenVehiculo OBJImagenVehiculos = new ImagenVehiculo();
            imagen = OBJImagenVehiculos.Consulta_IMAGEN_PRINCIPAL();
            return imagen;
        }

        public string imagenReducida()
        {
            string imagen = "";
            ImagenVehiculo OBJImagenVehiculos = new ImagenVehiculo();
            imagen = OBJImagenVehiculos.Consulta_IMAGEN_COMPRIMIDA();
            return imagen;
        }
        //========================================

        public string UltimosTresMesesVentas()
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {

                List<NotaVenta> ListNotaDeVenta = new List<NotaVenta>();
                String FechaActualString = DateTime.Now.ToString();
                DateTime FechaActual = (Convert.ToDateTime(FechaActualString.ToString()));
                int MesActual = 0;
                int AnioActual = 0;


                MesActual = Int32.Parse(FechaActual.Month.ToString());
                AnioActual = Int32.Parse(FechaActual.Year.ToString());



                for (int contador = 1; contador <= 3; contador++)
                {
                    NotaVenta OBJnotaDeVenta = new NotaVenta();
                    if (MesActual <= 0)
                    {
                        MesActual = 12;
                        AnioActual = AnioActual - 1;
                    }

                   
                    OBJnotaDeVenta.TOTAL = OBJnotaDeVenta.ConsultarTotalVentaDelMes(MesActual, AnioActual);
                    OBJnotaDeVenta.MES = OBJnotaDeVenta.ConsultarMesComoString(MesActual);

                    ListNotaDeVenta.Add(OBJnotaDeVenta);

                    MesActual = MesActual - 1;
                }

                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = JsonConvert.SerializeObject(ListNotaDeVenta, Formatting.Indented); 
                respuestaServicio.Detalle_Error = "Se encontro las totales satisfactoriamente";

            }
            catch(Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "" ;
                respuestaServicio.Detalle_Error = "No calcular las ventas totales de los ultimos 3 meses,Error: "+ex.Message;

            }
            return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
        }

        public string UltimosTresMesesVentasPorSucursal()
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                
                Sucursal OBJSucursal = new Sucursal();
                List<Sucursal> listaSucursales = new List<Sucursal>();
                String FechaActualString = DateTime.Now.ToString();
                DateTime FechaActual = (Convert.ToDateTime(FechaActualString.ToString()));
                int MesActual = 0;
                int AnioActual = 0;


                MesActual = Int32.Parse(FechaActual.Month.ToString());
                AnioActual = Int32.Parse(FechaActual.Year.ToString());
                int auxMesActual = MesActual;
                int auxAnioActual = AnioActual;
                listaSucursales = OBJSucursal.Consultar_Sucursales();

                foreach(var sucursal in listaSucursales)
                {
                    List<NotaVenta> ListNotaDeVenta = new List<NotaVenta>();

                    MesActual = auxMesActual;
                    AnioActual = auxAnioActual;

                    for (int contador = 1; contador <= 3; contador++)
                    {
                        NotaVenta OBJnotaDeVenta = new NotaVenta();
                        if (MesActual <= 0)
                        {
                            MesActual = 12;
                            AnioActual = AnioActual - 1;
                        }


                        OBJnotaDeVenta.TOTAL = OBJnotaDeVenta.ConsultarTotalVentaDelMesPorSucursal(MesActual, AnioActual, sucursal.ID_SUCURSAL);
                        OBJnotaDeVenta.MES = OBJnotaDeVenta.ConsultarMesComoString(MesActual);

                        ListNotaDeVenta.Add(OBJnotaDeVenta);

                        MesActual = MesActual - 1;
                    }
                    sucursal.MesesDeVenta = ListNotaDeVenta;
                }
                

                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = JsonConvert.SerializeObject(listaSucursales, Formatting.Indented);
                respuestaServicio.Detalle_Error = "Se encontro las totales satisfactoriamente";

            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No calcular las ventas totales de los ultimos 3 meses,Error: " + ex.Message;

            }
            return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
        }

        public string UltimosTresMesesVentasPorVendedor(int idUsuario)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {

                Sucursal OBJSucursal = new Sucursal();
                List<Sucursal> listaSucursales = new List<Sucursal>();
                String FechaActualString = DateTime.Now.ToString();
                DateTime FechaActual = (Convert.ToDateTime(FechaActualString.ToString()));
                int MesActual = 0;
                int AnioActual = 0;

                MesActual = Int32.Parse(FechaActual.Month.ToString());
                AnioActual = Int32.Parse(FechaActual.Year.ToString());

                    List<NotaVenta> ListNotaDeVenta = new List<NotaVenta>();
                    for (int contador = 1; contador <= 3; contador++)
                    {
                        NotaVenta OBJnotaDeVenta = new NotaVenta();
                        if (MesActual <= 0)
                        {
                            MesActual = 12;
                            AnioActual = AnioActual - 1;
                        }


                        OBJnotaDeVenta.TOTAL = OBJnotaDeVenta.ConsultarTotalVentaDelMesPorVendedor(MesActual, AnioActual, idUsuario);
                        OBJnotaDeVenta.MES = OBJnotaDeVenta.ConsultarMesComoString(MesActual);

                        ListNotaDeVenta.Add(OBJnotaDeVenta);

                        MesActual = MesActual - 1;
                    }
                  
                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = JsonConvert.SerializeObject(ListNotaDeVenta, Formatting.Indented);
                respuestaServicio.Detalle_Error = "Se encontro las totales satisfactoriamente";

            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No calcular las ventas totales de los ultimos 3 meses,Error: " + ex.Message;

            }
            return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
        }


        public string VentasDelMesPorEmpleado()
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {

             
                Usuarios OBJUsuarios = new Usuarios();
                List<Usuarios> listaUsuarios = new List<Usuarios>();
                NotaVenta OBJNotaDeVenta = new NotaVenta();
                String FechaActualString = DateTime.Now.ToString();
                DateTime FechaActual = (Convert.ToDateTime(FechaActualString.ToString()));
                int MesActual = 0;
                int AnioActual = 0;


                MesActual = Int32.Parse(FechaActual.Month.ToString());
                AnioActual = Int32.Parse(FechaActual.Year.ToString());


                listaUsuarios = OBJUsuarios.ConsultarUsuariosVendedores();

                foreach(var usuario in listaUsuarios)
                {
                    int TotalVentas = OBJNotaDeVenta.ConsultarTotalVentaDelMesPorUsuario(MesActual, AnioActual, usuario.ID_USUARIOS);
                    string mesDeVenta = OBJNotaDeVenta.ConsultarMesComoString(MesActual);
                    usuario.TOTAL_VENDIDO = TotalVentas;
                    usuario.MES_VENTA = mesDeVenta;
                }



                respuestaServicio.Respuesta = "OK";
                respuestaServicio.Descripcion = JsonConvert.SerializeObject(listaUsuarios, Formatting.Indented);
                respuestaServicio.Detalle_Error = "Se encontro las totales satisfactoriamente";

            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Descripcion = "";
                respuestaServicio.Detalle_Error = "No calcular las ventas totales de los ultimos 3 meses por usuario, Error: " + ex.Message;

            }
            return JsonConvert.SerializeObject(respuestaServicio, Formatting.Indented);
        }


    }
}