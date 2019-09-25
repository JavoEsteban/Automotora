using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Marca
    {
        public int ID_MARCA { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }
        public string VIGENTE { get; set; }
        public int CANTIDAD_VENDIDOS { get; set; }

        public RespuestaServicio AgregarMarca(Marca marca) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_MARCAR", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", marca.DESCRIPCION.ToUpper());
                    command.Parameters.AddWithValue("@VIGENCIA", marca.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente la Marca";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar la Marca";
                return respuesta;
            }
        }


        public List<Marca> Consultar_Marcas()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<Marca> listaMarcas = new List<Marca>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_MARCAS", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Marca marcas = new Marca();

                    marcas.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"].ToString());
                    marcas.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    marcas.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                    marcas.VIGENTE = reader["VIGENCIA"].ToString();

                    if (marcas.VIGENTE == "1")
                    {
                        marcas.VIGENTE = "VIGENTE";
                    }
                    else {
                        marcas.VIGENTE = "NO VIGENTE";
                    }


                    listaMarcas.Add(marcas);

                }


            }



            return listaMarcas;
        } //Obtiene todas las marcas existentes en la BD

        public Marca Consulta_Marca_Por_Id(Marca marca)
        { //traer los datos de la marca consultada
            Marca marcaConsultada = new Marca();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_MARCA_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_MARCA", marca.ID_MARCA);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    marcaConsultada.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"].ToString());
                    marcaConsultada.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    marcaConsultada.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return marcaConsultada;
        }//Obtiene una marca en base a su id

        public RespuestaServicio Editar_Marcas(Marca marca)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_MARCA", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_MARCA", marca.ID_MARCA);
                    command.Parameters.AddWithValue("@DESCRIPCION", marca.DESCRIPCION.ToUpper());
                    command.Parameters.AddWithValue("@VIGENCIA", marca.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo marca satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar marca con id :" + ID_MARCA + ", nombre: " + marca.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }//edita una marca existente en la bd

        public RespuestaServicio EliminarMarca(Marca marca)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_MARCA", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_MARCA", marca.ID_MARCA);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó marca satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar la marca seleccionada";
            }

            return respuestaServicio;
        }
        
        public List<Marca> Consultar_TOP5_MARCAS_MAS_VENDIDAS()
        //BUSCA LAS 5 MARCAS MAS VENDIDAS HISTORICAMENTE(SIN DESCRIMINAR EN FECHA)
        {
            List<Marca> listaMarcas = new List<Marca>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TOP5_MARCAS_MAS_VENDIDAS", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Marca marcas = new Marca();

                    marcas.ID_MARCA = Convert.ToInt32(reader["ID_MARCA"].ToString());
                    marcas.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    marcas.CANTIDAD_VENDIDOS = Convert.ToInt32(reader["CANTIDAD_VENDIDOS"].ToString());

                    listaMarcas.Add(marcas);
                }


            }



            return listaMarcas;
        }






    }

}


