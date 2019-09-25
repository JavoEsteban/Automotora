using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models.EquipamientoVehiculo
{
    public class Equipamiento
    {
         public int ID_EQUIPAMIENTO { get; set; }
         public string DESCRIPCION { get; set; }
         public int ID_CATEGORIA_EQUIPAMIENTO { get; set; }
         public int VIGENCIA { get; set; }

        public List<Equipamiento> Consultar_Equipamientos_Disponibles()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<Equipamiento> listaEquipamientos = new List<Equipamiento>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_EQUIPAMIENTOS_VIGENTES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Equipamiento equipamiento = new Equipamiento();
                    equipamiento.ID_EQUIPAMIENTO = Convert.ToInt32(reader["ID_EQUIPAMIENTO"].ToString());
                    equipamiento.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    equipamiento.ID_CATEGORIA_EQUIPAMIENTO = Convert.ToInt32(reader["ID_CATEGORIA_EQUIPAMIENTO"].ToString());
                    equipamiento.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());

                    listaEquipamientos.Add(equipamiento);

                }


            }



            return listaEquipamientos;
        }
    }
}