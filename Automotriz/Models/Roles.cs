using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Roles
    {

        public int ID_ROL { get; set; }
        public string DESCRIPCION { get; set; }
        public int VIGENCIA { get; set; }


        public List<Roles> Consultar_Rol()
        { //consulta los roles de la tabla roles 
            List<Roles> listaRoles = new List<Roles>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_ROLES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Roles roles = new Roles();
                    roles.ID_ROL = Convert.ToInt32(reader["ID_ROL"].ToString());
                    roles.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    roles.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());


                    listaRoles.Add(roles);

                }


            }

            return listaRoles;
        }

        public RespuestaServicio AgregarRol(Roles rol) //Agrega una marca la base datos en la tabla MARCAS
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();


            try
            {

                conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERTAR_ROL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DESCRIPCION", rol.DESCRIPCION.ToUpper());
                    command.Parameters.AddWithValue("@VIGENCIA", rol.VIGENCIA);

                    SqlDataReader reader = command.ExecuteReader();



                }
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "OK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "Se agrego correctamente el Rol";
                return respuesta;
            }
            catch (Exception ex)
            {
                sql.CerrarConnection(conn);
                respuesta.Respuesta = "NOK";
                respuesta.Descripcion = "";
                respuesta.Detalle_Error = "No fué posible agregar el Rol";
                return respuesta;
            }
        }

        public Roles Consulta_Rol_Por_Id(Roles rol)
        { //traer los datos de la marca consultada
            Roles rolConsultado = new Roles();

            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTA_ROL_POR_ID", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_ROL", rol.ID_ROL);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rolConsultado.ID_ROL = Convert.ToInt32(reader["ID_ROL"].ToString());
                    rolConsultado.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    rolConsultado.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                }


            }

            return rolConsultado;
        }


        public RespuestaServicio Editar_Rol(Roles rol)
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("EDITAR_ROL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_ROL", rol.ID_ROL);
                    command.Parameters.AddWithValue("@DESCRIPCION", rol.DESCRIPCION.ToUpper());
                    command.Parameters.AddWithValue("@VIGENCIA", rol.VIGENCIA);


                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se actualizo marca satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                string rolmayus = rol.DESCRIPCION.ToLower();
                string rolCapitalizado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(rolmayus);
                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "No se modificó el Rol " + rolCapitalizado + "";
            }

            return respuestaServicio;
        }

        public RespuestaServicio EliminarRol(Roles rol)// elimina una marca existente en la bd
        {
            RespuestaServicio respuestaServicio = new RespuestaServicio();
            try
            {
                SQLconn sql = new SQLconn();
                SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("ELIMINAR_ROL", conn);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_ROL", rol.ID_ROL);



                    command.ExecuteNonQuery();


                    respuestaServicio.Respuesta = "OK";
                    respuestaServicio.Descripcion = "Se eliminó el Rol satisfactoriamente";


                }
            }
            catch (Exception ex)
            {
                string rolmayus = rol.DESCRIPCION.ToLower();
                string rolCapitalizado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(rolmayus);

                respuestaServicio.Respuesta = "NOK";
                respuestaServicio.Detalle_Error = "El rol "+ rolCapitalizado + " no ha podido ser eliminado, uno o más usuarios tienen este rol";
            }

            return respuestaServicio;
        }




    }

   
}