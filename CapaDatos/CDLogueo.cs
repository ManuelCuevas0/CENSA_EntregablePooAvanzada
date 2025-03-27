using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class CDLogueo
{
    string CadenaConexion = "Server=localhost;User=root;Password=1234;Port=3306;database=bd_proyecto_poo_avanzada";

    public void PruebaConexion()
    {
        using (MySqlConnection conexion = new MySqlConnection(CadenaConexion))
        {
            conexion.Open();
            // Prueba de conexión exitosa
        }
    }

    public List<string> ObtenerRegistros()
    {
        List<string> registros = new List<string>();

        using (MySqlConnection conexion = new MySqlConnection(CadenaConexion))
        {
            conexion.Open();
            string query = "SELECT id, nombre, apellido, url_foto FROM usuario";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string registro = $"{reader["id"]}, {reader["nombre"]}, {reader["apellido"]}, {reader["url_foto"]}";
                    registros.Add(registro);
                }
            }
        }

        return registros;
    }
}
