using Automotriz.connection;
using Automotriz.Models.Parametro_Nota_Venta;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class NotaVenta
    {
        public int ID_NOTA_VENTA { get; set; }
        public DateTime FECHA_DOCUMENTO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public string SUCURSAL_DOCUMENTO { get; set; }
        public string SUCURSAL_VEHICULO { get; set; }

        public string DIRECCION_SUCURSAL { get; set; }
        public string RUT_SUCURSAL { get; set; }
        public int ID_CLIENTE { get; set; }
        public string RUT { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string DIRECCION { get; set; }
        public string COMUNA_CLIENTE { get; set; }
        public string CIUDAD_CLIENTE { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string MES { get; set; }
        public string DIA { get; set; }
        public int ID_VEHICULOS { get; set; }
        public string PATENTE { get; set; }
        public string MARCA { get; set; }
        public string NOMBRE_MODELO { get; set; }
        public string ANO { get; set; }
        public string COLOR { get; set; }
        public string TIPO_INGRESO { get; set; }
        public int ID_USUARIOS { get; set; }
        public string NOMBRE_USUARIO { get; set; }

        public int PRECIO_LISTA { get; set; }
        public int TRANSFERENCIA { get; set; }
        public int TOTAL { get; set; }
        public int TOTAL_SIN_DESCUENTOS { get; set; }
        public int TOTAL_DESCUENTOS { get; set; }
        public int TOTAL_PAGOS { get; set; }
        public int TOTAL_VEHICULOS_PP { get; set; }
        public string OBSERVACIONES { get; set; }

        //Metodo encargado de devolver el mes al cual pertenece el numero ingresado por parametro
        public string ConsultarMesComoString(int Mes)
        {
            string  mes = "";
            switch (Mes)
            {
                case 1:
                    mes =
                    "Enero";
                    break;
                case 2:
                    mes =
                    "Febrero";
                    break;
                case 3:
                    mes =
                    "Marzo";
                    break;
                case 4:
                    mes =
                    "Abril";
                    break;
                case 5:
                    mes = "Mayo";
                    break;
                case 6:
                    mes =
                    "Junio";
                    break;
                case 7:
                    mes =
                    "Julio";
                    break;
                case 8:
                    mes =
                    "Agosto";
                    break;
                case 9:
                    mes =
                    "Septiembre";
                    break;
                case 10:
                    mes =
                    "Octubre";
                    break;
                case 11:
                    mes =
                    "Noviembre";
                    break;
                case 12:
                    mes =
                    "Diciembre";
                    break;
            };



            return mes;

        }

        public RespuestaServicio agregarNotaVenta(NotaVenta notaVenta, List<Vehiculos> listaVehiculos, List<Pagos> listaPagos,List<Descuentos> listaDescuentos,string nombreUsuario,int idUsuario,string patente) 
        {
            Bitacora_Vehiculo OBJbitacora = new Bitacora_Vehiculo();
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();
            Vehiculos OBJ_Vehiculos = new Vehiculos();
         

            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_NOTA_VENTA", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FECHA_DOCUMENTO", notaVenta.FECHA_DOCUMENTO);
                    command.Parameters.AddWithValue("@ID_SUCURSAL", notaVenta.ID_SUCURSAL);
                    command.Parameters.AddWithValue("@ID_CLIENTE", notaVenta.ID_CLIENTE);
                    command.Parameters.AddWithValue("@ID_VEHICULO", notaVenta.ID_VEHICULOS);
                    command.Parameters.AddWithValue("@ID_USUARIOS", notaVenta.ID_USUARIOS);
                    command.Parameters.AddWithValue("@PRECIO_LISTA", notaVenta.PRECIO_LISTA);
                    command.Parameters.AddWithValue("@TRANSFERENCIA", notaVenta.TRANSFERENCIA);
                    command.Parameters.AddWithValue("@TOTAL", notaVenta.TOTAL);
                    command.Parameters.AddWithValue("@TOTAL_SIN_DESCUENTOS", notaVenta.TOTAL_SIN_DESCUENTOS);
                    command.Parameters.AddWithValue("@TOTAL_DESCUENTOS", notaVenta.TOTAL_DESCUENTOS);
                    command.Parameters.AddWithValue("@TOTAL_PAGOS", notaVenta.TOTAL_PAGOS);
                    command.Parameters.AddWithValue("@TOTAL_VEHICULOS_PP", notaVenta.TOTAL_VEHICULOS_PP);
                    command.Parameters.AddWithValue("@OBSERVACIONES", notaVenta.OBSERVACIONES);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        //tomo el valor devuelto desde la base de datos
                        notaVenta.ID_NOTA_VENTA = Convert.ToInt32(reader["ID_NOTA_VENTA"]);


                        //revisa si la lista de descuentos viene vacia, de lo contrario añade los descuentos
                        if (listaVehiculos != null)
                        {
                            if (listaVehiculos.Count > 0)
                            {

                                foreach (var vehiculo in listaVehiculos)
                                {
                                    agregarVehiculoPartePago(notaVenta.ID_NOTA_VENTA, vehiculo);

                                }
                            }

                        }

                        if (listaPagos != null)
                        {
                            if (listaPagos.Count > 0)
                            {

                                foreach (var pago in listaPagos)
                                {
                                    agregarPago(notaVenta.ID_NOTA_VENTA, pago);

                                }
                            }

                        }
                        if (listaDescuentos != null)
                        {
                            if (listaDescuentos.Count > 0)
                            {

                                foreach (var descuento in listaDescuentos)
                                {
                                    agregarDescuento(notaVenta.ID_NOTA_VENTA, descuento);

                                }
                            }

                        }

                    }

                   

                }
                sql.CerrarConnection(conn);

                //========================================VENTA DEL VEHICULO SIN REGISTRAR A LA BITACORA=====================
                //===========================================================================================================


                respuesta = OBJ_Vehiculos.VenderVehiculo_Sin_Bitacora(notaVenta.ID_VEHICULOS);


                //======================================================================================================================
                //===============================================INGRESO A BITACORA=====================================================

                if (respuesta.Respuesta == "OK")
                {

                    string detalleVenta = "";

                    OBJbitacora.DETALLE = "El usuario: " + nombreUsuario + ", A Vendido el vehiculo con patente " + patente + "\n\n";

                    if (listaVehiculos != null )
                    {
                        detalleVenta += "Vehiculos en parte de pago: \n\n";
                        foreach (var vehiculos in listaVehiculos)
                        {
                            detalleVenta += "Vehiculo Parte de Pago con patente " + vehiculos.PATENTE + " Marca " + vehiculos.PATENTE + " del año " + vehiculos.ANO + "\n";
                        }
                    }

                    if (listaPagos != null)
                    {
                        detalleVenta += " Pagos: \n\n";
                        foreach (var pago in listaPagos)
                        {
                            detalleVenta += "Forma de pago" + pago.FORMA_PAGO + " del banco " + pago.BANCO + " con un monto de " + pago.MONTO + "\n";
                        }
                    }

                    if (listaDescuentos != null)
                    {
                        detalleVenta += "Descuentos: \n\n";
                        foreach (var descuento in listaDescuentos)
                        {
                            detalleVenta += "Descuento de $" + descuento.MONTO + ", Razon del descuento: " + descuento.DESCRIPCION + "\n";
                        }
                    }

                    //agregamos el detalle
                    OBJbitacora.DETALLE += detalleVenta;

                    OBJbitacora.ID_VEHICULO = notaVenta.ID_VEHICULOS;
                    OBJbitacora.ESTADO = "VENDIDO";

                    OBJbitacora.TIPO = 1;
                    OBJbitacora.ID_USUARIO = idUsuario;


                    OBJbitacora.AgregarInformacionBitacora(OBJbitacora);
                }
                else
                {
                    respuesta.Respuesta = "NOK";
                    respuesta.Descripcion = "";
                    respuesta.Detalle_Error = "ups, se agrego nota de venta pero no se logro cambiar el estado del vehiculo a VENDIDO,Detalle error: " + respuesta.Detalle_Error;
                    return respuesta;
                }


                //======================================================================================================================
                //======================================================================================================================



                respuesta.Respuesta = "OK";
                respuesta.Descripcion = notaVenta.ID_NOTA_VENTA.ToString();
                respuesta.Detalle_Error = "Se agrego correctamente La nota de Venta";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar nota de venta,Error: " + ex.Message;
                return respuesta;
            }
        }

        public RespuestaServicio agregarDescuento(int ID_NOTA_VENTA, Descuentos descuento) 
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_DESCUENTO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", descuento.DESCRIPCION);
                    command.Parameters.AddWithValue("@MONTO", descuento.MONTO);
                    command.Parameters.AddWithValue("@ID_NOTA_VENTA", ID_NOTA_VENTA);



                    SqlDataReader reader = command.ExecuteReader();

                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Descuento";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar Descuento,Error: " + ex.Message;
                return respuesta;
            }
        }

        public RespuestaServicio agregarVehiculoPartePago(int ID_NOTA_VENTA, Vehiculos vehiculo)
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_VEHICULO_PARTE_PAGO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_NOTA_VENTA", ID_NOTA_VENTA);
                    command.Parameters.AddWithValue("@PATENTE", vehiculo.PATENTE);
                    command.Parameters.AddWithValue("@MARCA", vehiculo.MARCA);
                    command.Parameters.AddWithValue("@ANO", vehiculo.ANO);
                    command.Parameters.AddWithValue("@PRECIO_TOMA", vehiculo.PRECIO_COMPRA);


                    SqlDataReader reader = command.ExecuteReader();

                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el VEHICULO";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar VEHICULO,Error: " + ex.Message;
                return respuesta;
            }
        }

        public RespuestaServicio agregarPago(int ID_NOTA_VENTA, Pagos pago)
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_PAGO", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_NOTA_VENTA", ID_NOTA_VENTA);
                    command.Parameters.AddWithValue("@ID_FORMA_PAGO", pago.ID_FORMA_PAGO);
                    command.Parameters.AddWithValue("@TIPO_DOCUMENTO", pago.TIPO_DOCUMENTO);
                    command.Parameters.AddWithValue("@MONTO", pago.MONTO);
                    command.Parameters.AddWithValue("@CUOTAS", pago.CUOTAS);
                    command.Parameters.AddWithValue("@DIAS_PAGO", pago.DIAS_PAGO);


                    SqlDataReader reader = command.ExecuteReader();

                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el pago";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar pago,Error: " + ex.Message;
                return respuesta;
            }
        }

        public NotaVenta Consulta_Nota_Venta_Por_Id(int id_notaVenta)
        {
            NotaVenta notaConsultada = new NotaVenta();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_NOTA_VENTA_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_NOTA_VENTA", id_notaVenta);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    notaConsultada.ID_NOTA_VENTA = Convert.ToInt32(reader["ID_NOTA_VENTA"].ToString());
                    notaConsultada.FECHA_DOCUMENTO = (DateTime)(reader["FECHA_DOCUMENTO"]);
                    notaConsultada.PRECIO_LISTA = Convert.ToInt32(reader["PRECIO_LISTA"].ToString());
                    notaConsultada.TRANSFERENCIA = Convert.ToInt32(reader["TRANSFERENCIA"].ToString());
                    notaConsultada.TOTAL = Convert.ToInt32(reader["TOTAL"].ToString());
                    notaConsultada.TOTAL_SIN_DESCUENTOS = Convert.ToInt32(reader["TOTAL_SIN_DESCUENTOS"].ToString());
                    notaConsultada.TOTAL_DESCUENTOS = Convert.ToInt32(reader["TOTAL_DESCUENTOS"].ToString());
                    notaConsultada.TOTAL_PAGOS = Convert.ToInt32(reader["TOTAL_PAGOS"].ToString());
                    notaConsultada.TOTAL_VEHICULOS_PP = Convert.ToInt32(reader["TOTAL_VEHICULOS_PP"].ToString());
                    notaConsultada.OBSERVACIONES = reader["OBSERVACIONES"].ToString();
                    notaConsultada.SUCURSAL_DOCUMENTO = (reader["SUCURSAL_DOCUMENTO"]).ToString();
                    notaConsultada.DIRECCION_SUCURSAL = (reader["DIRECCION_SUCURSAL"]).ToString();
                    notaConsultada.RUT_SUCURSAL = (reader["RUT_SUCURSAL"]).ToString();
                    notaConsultada.RUT = reader["RUT"].ToString();
                    notaConsultada.NOMBRES = reader["NOMBRES"].ToString();
                    notaConsultada.APELLIDOS = reader["APELLIDOS"].ToString();
                    notaConsultada.DIRECCION = reader["DIRECCION"].ToString();
                    notaConsultada.COMUNA_CLIENTE = reader["COMUNA"].ToString();
                    notaConsultada.CIUDAD_CLIENTE = reader["CIUDAD"].ToString();
                    notaConsultada.TELEFONO = reader["TELEFONO"].ToString();
                    notaConsultada.EMAIL = reader["EMAIL"].ToString();
                    notaConsultada.PATENTE = reader["PATENTE"].ToString();
                    notaConsultada.MARCA = reader["MARCA"].ToString();
                    notaConsultada.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    notaConsultada.ANO = reader["ANO"].ToString();
                    notaConsultada.COLOR = reader["COLOR"].ToString();
                    notaConsultada.TIPO_INGRESO = reader["TIPO_INGRESO"].ToString();
                    notaConsultada.SUCURSAL_VEHICULO = reader["SUCURSAL_VEHICULO"].ToString();
                    notaConsultada.NOMBRE_USUARIO = reader["NOMBRE_USUARIO"].ToString();


                }


            }

            return notaConsultada;
        }

        public List<Pagos> Consultar_Pagos(int idNotaVenta)
        {
            List<Pagos> listaPagos = new List<Pagos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_PAGOS_POR_ID_NOTA_VENTA", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_NOTA_VENTA", idNotaVenta);



                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Pagos pago = new Pagos();
                    pago.ID_PAGOS = Convert.ToInt32(reader["ID_PAGOS"].ToString());
                    pago.ID_NOTA_VENTA = Convert.ToInt32(reader["ID_NOTA_VENTA"].ToString());
                    pago.TIPO_DOCUMENTO = reader["TIPO_DOCUMENTO"].ToString();
                    pago.CUOTAS = Convert.ToInt32(reader["CUOTAS"].ToString());
                    pago.DIAS_PAGO = reader["DIAS_PAGO"].ToString();
                    pago.MONTO = Convert.ToInt32(reader["MONTO"].ToString());
                    pago.ID_FORMA_PAGO = Convert.ToInt32(reader["ID_FORMA_PAGO"].ToString());
                    pago.FORMA_PAGO = reader["FORMA_PAGO"].ToString();



                    listaPagos.Add(pago);

                }


            }

            return listaPagos;
        }

        public List<VehiculoPartePago> Consultar_VehiculosPartePago(int idNotaVenta)
        {
            List<VehiculoPartePago> listaVehiculosPartePago = new List<VehiculoPartePago>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_VEHICULOS_PARTE_PAGO_POR_ID_NOTA_VENTA", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_NOTA_VENTA", idNotaVenta);



                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    VehiculoPartePago vehiculoPartePago = new VehiculoPartePago();
                    vehiculoPartePago.ID_VEHICULO_PARTE_PAGO = Convert.ToInt32(reader["ID_VEHICULO_PARTE_PAGO"].ToString());
                    vehiculoPartePago.ID_NOTA_VENTA = Convert.ToInt32(reader["ID_NOTA_VENTA"].ToString());
                    vehiculoPartePago.PATENTE = reader["PATENTE"].ToString();
                    vehiculoPartePago.MARCA = reader["MARCA"].ToString();
                    vehiculoPartePago.ANO = Convert.ToInt32(reader["ANO"].ToString());
                    vehiculoPartePago.PRECIO_COMPRA = Convert.ToInt32(reader["PRECIO_TOMA"].ToString());

                    listaVehiculosPartePago.Add(vehiculoPartePago);

                }


            }

            return listaVehiculosPartePago;
        }

        public List<Descuentos> Consultar_Descuentos(int idNotaVenta)
        {
            List<Descuentos> listaDescuentos = new List<Descuentos>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_DESCUENTOS_POR_ID_NOTA_VENTA", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_NOTA_VENTA", idNotaVenta);



                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Descuentos descuento = new Descuentos();
                    descuento.ID_DESCUENTOS = Convert.ToInt32(reader["ID_DESCUENTOS"].ToString());
                    descuento.ID_NOTA_VENTA = Convert.ToInt32(reader["ID_NOTA_VENTA"].ToString());
                    descuento.DESCRIPCION = reader["MOTIVO"].ToString();
                    descuento.MONTO = Convert.ToInt32(reader["MONTO"].ToString());

                    listaDescuentos.Add(descuento);

                }


            }

            return listaDescuentos;
        }

        public int ConsultarIdNotaVentaPorIdVehiculo(int idVehiculo)
        {
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
                int idNotaDeVenta = 0;

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_NOTA_VENTA_POR_ID_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_VEHICULO", idVehiculo);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        idNotaDeVenta = Int32.Parse(reader["ID"].ToString());

                    }


                }

                return idNotaDeVenta;
            }catch(Exception ex){
                return 0;
            }
            
        }

        public int ConsultarTotalVentaDelMes(int Mes,int Anio)
        {
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
                int totalVenta = 0;

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VENTAS_DEL_MES_Y_ANIO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MES", Mes);
                    command.Parameters.AddWithValue("@ANIO", Anio);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        totalVenta = Int32.Parse(reader["TOTAL"].ToString());

                    }


                }

                return totalVenta;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ConsultarTotalVentaDelMesPorUsuario(int Mes, int Anio,int idUsuario)
        {
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
                int totalVenta = 0;

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VENTAS_DEL_MES_Y_ANIO_POR_USUAROP", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MES", Mes);
                    command.Parameters.AddWithValue("@ANIO", Anio);
                    command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        totalVenta = Int32.Parse(reader["TOTAL"].ToString());

                    }


                }

                return totalVenta;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ConsultarTotalVentaDelMesPorSucursal(int Mes, int Anio,int Id_Sucursal)
        {
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
                int totalVenta = 0;

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VENTAS_DEL_MES_Y_ANIO_POR_SUCURSAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MES", Mes);
                    command.Parameters.AddWithValue("@ANIO", Anio);
                    command.Parameters.AddWithValue("@ID_SUCURSAL", Id_Sucursal);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        totalVenta = Int32.Parse(reader["TOTAL"].ToString());

                    }


                }

                return totalVenta;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ConsultarTotalVentaDelMesPorVendedor(int Mes, int Anio, int idUsuario)
        {
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
                int totalVenta = 0;

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VENTAS_DEL_MES_Y_ANIO_POR_VENDEDOR", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MES", Mes);
                    command.Parameters.AddWithValue("@ANIO", Anio);
                    command.Parameters.AddWithValue("@ID_USUARIOS", idUsuario);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        totalVenta = Int32.Parse(reader["TOTAL"].ToString());

                    }


                }

                return totalVenta;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string RemplazarNombreInglesEspaniol(string dia)
        {
            switch (dia)
            {
                case "MONDAY":
                    return "LUNES";

                case "TUESDAY":
                    return "MARTES";

                case "WEDNESDAY":
                    return "MIERCOLES";

                case "THURSDAY":
                    return "JUEVES";
        
                case "FRIDAY":
                    return "VIERNES";
               
                case "SATURDAY":
                    return "SABADO";
             
                case "SUNDAY":
                    return "DOMINGO";
                    
                default:
                    return dia;

            }
        }

        public List<NotaVenta> ConsultarVentasPordiaDelMesActual()
        {
            List<NotaVenta> listaDeVentasDeLaSemana = new List<NotaVenta>();
            try
            {

                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());
                int totalVenta = 0;

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTAR_VENTAS_DIAS_DEL_MES_ACTUAL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                   

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NotaVenta diaDeVenta = new NotaVenta();

                        diaDeVenta.DIA = reader["DIA"].ToString();
                        diaDeVenta.DIA = RemplazarNombreInglesEspaniol(diaDeVenta.DIA);
                        diaDeVenta.TOTAL = Int32.Parse(reader["TOTAL_DIARIO"].ToString());

                       listaDeVentasDeLaSemana.Add(diaDeVenta);
                    }


                }

                return listaDeVentasDeLaSemana;
            }
            catch (Exception ex)
            {
                return listaDeVentasDeLaSemana;
            }
        }
    }
}