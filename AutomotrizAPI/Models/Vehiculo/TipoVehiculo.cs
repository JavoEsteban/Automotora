using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AutomotrizAPI.Models.Vehiculo
{
    public class TipoVehiculo
    {
        public int ID_TIPO_VEHICULO { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }

        public List<Vehiculos> LISTA_DE_VEHICULOS { get; set; }

        public List<TipoVehiculo> Consultar_TipoVehiculo()
        { //trae los tipos de vehiculos en la tabla tipo vehiculo
            List<TipoVehiculo> listaTipos = new List<TipoVehiculo>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TIPO_VEHICULO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TipoVehiculo tipoVehiculos = new TipoVehiculo();
                    tipoVehiculos.ID_TIPO_VEHICULO = Convert.ToInt32(reader["ID_TIPO_VEHICULO"].ToString());
                    tipoVehiculos.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    tipoVehiculos.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaTipos.Add(tipoVehiculos);

                }


            }



            return listaTipos;
        }


        public List<TipoVehiculo> Consultar_TipoVehiculo_Vigentes()
        { //trae los tipos de vehiculos en la tabla tipo vehiculo
            List<TipoVehiculo> listaTipos = new List<TipoVehiculo>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TIPO_VEHICULO_VIGENTES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TipoVehiculo tipoVehiculos = new TipoVehiculo();
                    tipoVehiculos.ID_TIPO_VEHICULO = Convert.ToInt32(reader["ID_TIPO_VEHICULO"].ToString());
                    tipoVehiculos.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    tipoVehiculos.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaTipos.Add(tipoVehiculos);

                }


            }



            return listaTipos;
        }

        public RespuestaServicio AgregarTipoVehiculo(TipoVehiculo tipoVehiculo) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_TIPO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Tipo de Vehiculo";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar el Tipo de Vehiculo";
                return respuesta;
            }
        }

        public TipoVehiculo Consulta_TipoVehiculo_Por_Id(TipoVehiculo tipoVehiculo)
        { //traer los datos de un tipo de vehiculo
            TipoVehiculo tipoVehiculoConsultado = new TipoVehiculo();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_TIPO_VEHICULO_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", tipoVehiculo.ID_TIPO_VEHICULO);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tipoVehiculoConsultado.ID_TIPO_VEHICULO = Convert.ToInt32(reader["ID_TIPO_VEHICULO"].ToString());
                    tipoVehiculoConsultado.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    tipoVehiculoConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return tipoVehiculoConsultado;
        }

        public RespuestaServicio Editar_TipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_TIPO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", tipoVehiculo.ID_TIPO_VEHICULO);
                    command.Parameters.AddWithValue("@DESCRIPCION", tipoVehiculo.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", tipoVehiculo.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el Tipo de Vehiculo correctamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar el Tipo de Vehiculo nombre: " + tipoVehiculo.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarTipoVehiculo(TipoVehiculo tipoVehiculo)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_TIPO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_VEHICULO", tipoVehiculo.ID_TIPO_VEHICULO);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó el tipo de Vehiculo satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el tipo de Vehiculo seleccionado";
            }

            return respuestaServicio;
        }

    }
}