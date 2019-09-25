using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class ModeloVehiculo
    {
        public int ID_MODELO { set; get; }
        public string NOMBRE_MODELO { set; get; }
        public string MARCA { set; get; }
        public int ID_MARCA { set; get; }
        public int VIGENCIA { set; get; }
        public string VIGENTE { set; get; }

        public List<ModeloVehiculo> consultarModelos(ModeloVehiculo modeloVehiculo)//traer todos los modelos de vehiculos en la bd
        { 
            List<ModeloVehiculo> listaModelos = new List<ModeloVehiculo>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_MODELO_POR_MARCA", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_MARCA", modeloVehiculo.ID_MARCA);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ModeloVehiculo modeloVehiculos = new ModeloVehiculo();
                    modeloVehiculos.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"].ToString());
                    modeloVehiculos.MARCA = reader["MARCA"].ToString();
                    modeloVehiculos.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    modeloVehiculos.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                    modeloVehiculos.VIGENTE = reader["VIGENCIA"].ToString();

                    if (modeloVehiculos.VIGENTE == "1")
                    {

                        modeloVehiculos.VIGENTE = "VIGENTE";
                    }
                    else {
                        modeloVehiculos.VIGENTE = "NO VIGENTE";
                    }


                    listaModelos.Add(modeloVehiculos);

                }


            }

            return listaModelos;
        }

        public List<ModeloVehiculo> consultarModelosTodos(ModeloVehiculo modeloVehiculo)//traer todos los modelos de vehiculos en la bd
        {
            List<ModeloVehiculo> listaModelos = new List<ModeloVehiculo>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_MODELO_POR_MARCA_TODOS", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_MARCA", modeloVehiculo.ID_MARCA);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ModeloVehiculo modeloVehiculos = new ModeloVehiculo();
                    modeloVehiculos.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"].ToString());
                    modeloVehiculos.MARCA = reader["MARCA"].ToString();
                    modeloVehiculos.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    modeloVehiculos.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaModelos.Add(modeloVehiculos);

                }


            }

            return listaModelos;
        }

        public RespuestaServicio AgregarModelo(ModeloVehiculo modeloVehiculo) //Agrega un Modelo de vehiculo
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_MODELO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NOMBRE_MODELO", modeloVehiculo.NOMBRE_MODELO.ToUpper());
                    command.Parameters.AddWithValue("@ID_MARCA", modeloVehiculo.ID_MARCA);
                    command.Parameters.AddWithValue("@VIGENCIA", modeloVehiculo.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Modelo";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar el Modelo";
                return respuesta;
            }
        }



        public ModeloVehiculo Consulta_Modelo_Por_Id(ModeloVehiculo modeloVehiculo)//consulta un modelo de vehiculo en base a su ID
        { 
            ModeloVehiculo modeloConsultado = new ModeloVehiculo();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_MODELO_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_MODELO", modeloVehiculo.ID_MODELO);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    modeloConsultado.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"].ToString());
                    modeloConsultado.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"].ToString());
                    modeloConsultado.NOMBRE_MODELO = reader["NOMBRE_MODELO"].ToString();
                    modeloConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return modeloConsultado;
        }

        public RespuestaServicio Editar_Modelo(ModeloVehiculo modeloVehiculo)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_MODELO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_MODELO", modeloVehiculo.ID_MODELO);
                    command.Parameters.AddWithValue("@ID_MARCA", modeloVehiculo.ID_MARCA);
                    command.Parameters.AddWithValue("@NOMBRE_MODELO", modeloVehiculo.NOMBRE_MODELO.ToUpper());
                    command.Parameters.AddWithValue("@VIGENCIA", modeloVehiculo.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el modelo satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar el modelo" + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarModelo(ModeloVehiculo modeloVehiculo)// elimina un modelo existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_MODELO_VEHICULO", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_MODELO", modeloVehiculo.ID_MODELO);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó el modelo  satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el modelo  seleccionado";
            }

            return respuestaServicio;
        }







    }


}