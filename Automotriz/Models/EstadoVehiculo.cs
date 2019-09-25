using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class EstadoVehiculo
    {
        public int ID_ESTADO{ get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }


        public List<EstadoVehiculo> Consultar_EstadoVehiculo()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<EstadoVehiculo> listaEstados = new List<EstadoVehiculo>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_ESTADO_VEHICULO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EstadoVehiculo estados = new EstadoVehiculo();
                    estados.ID_ESTADO = Convert.ToInt32(reader["ID_ESTADO"].ToString());
                    estados.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    estados.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaEstados.Add(estados);

                }


            }



            return listaEstados;
        }

        public RespuestaServicio AgregarEstado(EstadoVehiculo estadoVehiculo) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_ESTADO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", estadoVehiculo.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", estadoVehiculo.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Estado ";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar el Estado";
                return respuesta;
            }
        }

        public EstadoVehiculo Consulta_Marca_Por_Id(EstadoVehiculo estado)
        { //traer los datos de la marca consultada
            EstadoVehiculo estadoConsultado = new EstadoVehiculo();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_ESTADO_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_ESTADO", estado.ID_ESTADO);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estadoConsultado.ID_ESTADO = Convert.ToInt32(reader["ID_ESTADO"].ToString());
                    estadoConsultado.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    estadoConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return estadoConsultado;
        }

        public RespuestaServicio Editar_Estado(EstadoVehiculo estado)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_ESTADO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_ESTADO", estado.ID_ESTADO);
                    command.Parameters.AddWithValue("@DESCRIPCION", estado.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", estado.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el Estado satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar el Estado: " + estado.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarEstado(EstadoVehiculo estado)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_ESTADO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_ESTADO", estado.ID_ESTADO);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó el Estado satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el Estado seleccionado";
            }

            return respuestaServicio;
        }
    }

    
}