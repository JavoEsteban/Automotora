using Automotriz.connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AutomotrizAPI.Models.EquipamientoVehiculo
{
    public class EquipamientosVehiculo
    {
         public int ID_EQUIPAMIENTO_VEHICULO { get; set; }
         public int ID_EQUIPAMIENTO { get; set; }
         public int ID_VEHICULOS { get; set; }
         public string NOMBRE_EQUIPAMIENTO { get; set; }




        public void Agregar_Equipamiento_Vehiculo(int ID_EQUIPAMIENTO,int ID_VEHICULO) //Agrega una marca la base datos en la tabla MARCAS
        {
           
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("INSERTAR_EQUIPAMIENTO_VEHICULO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_EQUIPAMIENTO", ID_EQUIPAMIENTO);
                command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);

                SqlDataReader reader = command.ExecuteReader();



            }
            sql.CerrarConnection(conn);
            
        }
        public List<EquipamientosVehiculo> Consultar_Equipamiento_Vehiculo_Por_Id_Vehiculo(int ID_VEHICULO) //Agrega una marca la base datos en la tabla MARCAS
        {
            List<EquipamientosVehiculo> ListaDeEquipamientoVehiculos = new List<EquipamientosVehiculo>();
            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("CONSULTAR_EQUIPAMIENTO_VEHICULO_POR_ID_VEHICULO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EquipamientosVehiculo OBJEquipamientoVehiculo = new EquipamientosVehiculo();
                    OBJEquipamientoVehiculo.ID_EQUIPAMIENTO_VEHICULO = Convert.ToInt32(reader["ID_EQUIPAMIENTO_VEHICULO"].ToString());
                    OBJEquipamientoVehiculo.NOMBRE_EQUIPAMIENTO = reader["DESCRIPCION"].ToString();
                    OBJEquipamientoVehiculo.ID_EQUIPAMIENTO = Convert.ToInt32(reader["ID_EQUIPAMIENTO"].ToString());
                    ListaDeEquipamientoVehiculos.Add(OBJEquipamientoVehiculo);
                }


            }
            sql.CerrarConnection(conn);


            return ListaDeEquipamientoVehiculos;
        }

        public void Eliminar_Equipamiento_Vehiculo(int ID_VEHICULO) 
        {

            SqlConnection conn = null;
            SQLconn sql = new SQLconn();

            conn = sql.AbrirConnection(sql.ObtenerConnection());

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("ELIMINAR_EQUIPAMIENTO_VEHICULO", conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;

              
                command.Parameters.AddWithValue("@ID_VEHICULO", ID_VEHICULO);

                SqlDataReader reader = command.ExecuteReader();



            }
            sql.CerrarConnection(conn);

        }

    }
}