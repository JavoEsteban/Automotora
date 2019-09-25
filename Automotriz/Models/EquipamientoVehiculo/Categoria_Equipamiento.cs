using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automotriz.Models
{
    public class Categoria_Equipamiento
    {
       public int ID_CATEGORIA_EQUIPAMIENTO { get; set; }
       public string DESCRIPCION { get; set; }
       public int VIGENCIA { get; set; }


        public List<Categoria_Equipamiento> Consultar_Categoria_Equipamientos_Disponibles()
        { //traer todas las marcas de la base de datos presentes en la tabla Marca de forma serializada
            List<Categoria_Equipamiento> listaCategoriaEquipamientos = new List<Categoria_Equipamiento>();


            SQLconn sql = new SQLconn();
            SqlConnection conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_CATEGORIAS_EQUIPAMIENTOS_DISPONIBLES", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Categoria_Equipamiento equipamientoCategoria = new Categoria_Equipamiento();
                    equipamientoCategoria.ID_CATEGORIA_EQUIPAMIENTO = Convert.ToInt32(reader["ID_CATEGORIA_EQUIPAMIENTO"].ToString());
                    equipamientoCategoria.DESCRIPCION = reader["DESCRIPCION"].ToString();
                    equipamientoCategoria.VIGENCIA = Convert.ToInt32(reader["VIGENCIA"].ToString());
                   

                    listaCategoriaEquipamientos.Add(equipamientoCategoria);

                }


            }



            return listaCategoriaEquipamientos;
        }
    }
}