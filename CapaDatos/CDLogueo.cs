using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class CDLogueo
{
    string CadenaConexion = "Server=localhost;User=root;Password=1234;Port=3306;database=bd_proyecto_poo_avanzada";

    public bool EliminarRegistro(string id)
    {
        using (MySqlConnection conexion = new MySqlConnection(CadenaConexion))
        {
            string query = "DELETE FROM usuario WHERE id = @Id";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                conexion.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return false;
            }
        }
    }


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

    public bool InsertarRegistro(string id, string nombre, string apellido, string imagePath)
    {
        using (MySqlConnection conexion = new MySqlConnection(CadenaConexion))
        {
            string query = "INSERT INTO usuario (id, nombre, apellido, url_foto) VALUES (@Id, @Nombre, @Apellido, @ImagePath)";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Apellido", apellido);
            cmd.Parameters.AddWithValue("@ImagePath", imagePath);

            try
            {
                conexion.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return false;
            }
        }
    }
}
