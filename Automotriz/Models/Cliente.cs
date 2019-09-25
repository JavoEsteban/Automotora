using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Cliente
    {
        public int ID_CLIENTE { get; set; }
        public string RUT { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string DIRECCION { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONO { get; set; }
        public string COMUNA { get; set; }
        public string CIUDAD { get; set; }

        public RespuestaServicio AgregarClientes(Cliente cliente) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_CLIENTE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RUT", cliente.RUT);
                    command.Parameters.AddWithValue("@NOMBRES", cliente.NOMBRES.ToUpper());
                    command.Parameters.AddWithValue("@APELLIDOS", cliente.APELLIDOS.ToUpper());
                    command.Parameters.AddWithValue("@DIRECCION", cliente.DIRECCION.ToUpper());
                    command.Parameters.AddWithValue("@EMAIL", cliente.EMAIL.ToLower());
                    command.Parameters.AddWithValue("@TELEFONO", cliente.TELEFONO);
                    command.Parameters.AddWithValue("@COMUNA", cliente.COMUNA);
                    command.Parameters.AddWithValue("@CIUDAD", cliente.CIUDAD);

                    SqlDataReader reader = command.ExecuteReader();

                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el cliente";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar Cliente,Error: "+ex.Message;
                return respuesta;
            }
        }

        public List<Cliente> Consultar_Clientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_CLIENTES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Cliente clientes = new Cliente();
                    clientes.ID_CLIENTE = Convert.ToInt32(reader["ID_CLIENTE"].ToString());
                    clientes.RUT = reader["RUT"].ToString();
                    clientes.NOMBRES = reader["NOMBRES"].ToString();
                    clientes.APELLIDOS = reader["APELLIDOS"].ToString();
                    clientes.DIRECCION = reader["DIRECCION"].ToString();
                    clientes.EMAIL = reader["EMAIL"].ToString();
                    clientes.TELEFONO = reader["TELEFONO"].ToString();
                    clientes.COMUNA = reader["COMUNA"].ToString();
                    clientes.CIUDAD = reader["CIUDAD"].ToString();



                    listaClientes.Add(clientes);

                }


            }

            return listaClientes;
        }
        //
        public RespuestaServicio ValidarClienteRut(string rut)
        {


            RespuestaServicio respuesta = new RespuestaServicio();
            SQLconn sql = new SQLconn();
            int rutsEncontrados = 0;

            try { 

                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("CONSULTA_EXISTENCIA_RUT_CLIENTE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RUT", rut);

                    SqlDataReader reader = command.ExecuteReader();
                   
                    while (reader.Read())
                    {
                        rutsEncontrados = Convert.ToInt32(reader["ENCONTRADOS"].ToString());
                    }

                    if (rutsEncontrados == 0)
                    {
                        respuesta.Respuesta = "OK";
                        respuesta.Detalle_Error = "No se encontro ningun rut que se paresca";
                        return respuesta;
                    }
                    else
                    {
                        respuesta.Respuesta = "NOK";
                        respuesta.Detalle_Error = "El rut ya existe";
                        return respuesta;
                    }

                }
                else
                {
                    respuesta.Respuesta = "NOK";
                    respuesta.Detalle_Error = "No se logro establecer comunicacion con el servidor al validar el rut";
                    return respuesta;
                }
            }catch(Exception ex)
            {
                respuesta.Respuesta = "NOK";
                respuesta.Detalle_Error = "No se logro validar el rut del cliente,Error: "+ex.Message;
                return respuesta;
            }


        }

        public Cliente Consulta_Cliente_Por_Id(Cliente cliente)
        {
            Cliente clienteConsultado = new Cliente();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_CLIENTE_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_CLIENTE", cliente.ID_CLIENTE);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    clienteConsultado.ID_CLIENTE = Convert.ToInt32(reader["ID_CLIENTE"].ToString());
                    clienteConsultado.RUT = reader["RUT"].ToString();
                    clienteConsultado.NOMBRES = reader["NOMBRES"].ToString();
                    clienteConsultado.APELLIDOS = reader["APELLIDOS"].ToString();
                    clienteConsultado.DIRECCION = reader["DIRECCION"].ToString();
                    clienteConsultado.EMAIL = reader["EMAIL"].ToString();
                    clienteConsultado.TELEFONO = reader["TELEFONO"].ToString();
                    clienteConsultado.COMUNA = reader["COMUNA"].ToString();
                    clienteConsultado.CIUDAD = reader["CIUDAD"].ToString();

                }


            }

            return clienteConsultado;
        }

        public RespuestaServicio Editar_Cliente(Cliente cliente)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_CLIENTES", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", cliente.ID_CLIENTE);
                    command.Parameters.AddWithValue("@RUT", cliente.RUT);
                    command.Parameters.AddWithValue("@NOMBRES", cliente.NOMBRES.ToUpper());
                    command.Parameters.AddWithValue("@APELLIDOS", cliente.APELLIDOS.ToUpper());
                    command.Parameters.AddWithValue("@DIRECCION", cliente.DIRECCION.ToUpper());
                    command.Parameters.AddWithValue("@EMAIL", cliente.EMAIL.ToLower());
                    command.Parameters.AddWithValue("@TELEFONO", cliente.TELEFONO);
                    command.Parameters.AddWithValue("@COMUNA", cliente.COMUNA);
                    command.Parameters.AddWithValue("@CIUDAD", cliente.CIUDAD);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el Cliente satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar cliente  nombre: " + cliente.NOMBRES + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarCliente(Cliente cliente)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_CLIENTE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", cliente.ID_CLIENTE);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó el Cliente satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el Cliente seleccionado";
            }

            return respuestaServicio;
        }


       


    }
}
