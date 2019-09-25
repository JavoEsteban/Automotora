using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class TipoCombustible
    {
        public int ID_TIPO_COMBUSTIBLE { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }

        public List<TipoCombustible> Consultar_TipoCombustible()
        { //traer todas los combustibles de la tabla tipo combustibles
            List<TipoCombustible> listaComsbutibles = new List<TipoCombustible>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_TIPO_COMBUSTIBLE", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TipoCombustible combustibles = new TipoCombustible();
                    combustibles.ID_TIPO_COMBUSTIBLE = Convert.ToInt32(reader["ID_TIPO_COMBUSTIBLE"].ToString());
                    combustibles.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    combustibles.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaComsbutibles.Add(combustibles);

                }


            }



            return listaComsbutibles;
        }

        public RespuestaServicio AgregarCombustible(TipoCombustible combustible) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_TIPO_COMBUSTIBLE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", combustible.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", combustible.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Combustible";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "ups, no se logro agregar el Combustible";
                return respuesta;
            }
        }

        public TipoCombustible Consulta_Combustible_Por_Id(TipoCombustible combustible)
        { //traer los datos del combustible consultado
            TipoCombustible combustibleConsultado = new TipoCombustible();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_TIPO_COMBUSTIBLE_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", combustible.ID_TIPO_COMBUSTIBLE);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    combustibleConsultado.ID_TIPO_COMBUSTIBLE = Convert.ToInt32(reader["ID_TIPO_COMBUSTIBLE"].ToString());
                    combustibleConsultado.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    combustibleConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return combustibleConsultado;
        }

        public RespuestaServicio Editar_TipoCombustible(TipoCombustible combustible)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_TIPO_COMBUSTIBLE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", combustible.ID_TIPO_COMBUSTIBLE);
                    command.Parameters.AddWithValue("@DESCRIPCION", combustible.DESCRIPCION);
                    command.Parameters.AddWithValue("@VIGENCIA", combustible.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo el Tipo de Combustible satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro editar Tipo de Combustible nombre: " + combustible.DESCRIPCION + ". Error: " + ex.Message;
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarCombustible(TipoCombustible combustible)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_TIPO_COMBUSTIBLE", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_TIPO_COMBUSTIBLE", combustible.ID_TIPO_COMBUSTIBLE);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se elimino el Combustible satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se logro eliminar el Combustible seleccionado";
            }

            return respuestaServicio;
        }


    }
}
