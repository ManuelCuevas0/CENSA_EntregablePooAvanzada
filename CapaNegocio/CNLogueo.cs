using System.Collections.Generic;

public class CNLogueo
{

    CDLogueo cDLogueo = new CDLogueo();

    public Registro? BuscarRegistro(string id)
    {
        return datos.BuscarRegistro(id);
    }

    public bool EliminarRegistro(string id)
    {
        return datos.EliminarRegistro(id);
    }

    public void PruebaMysql()
    {
        cDLogueo.PruebaConexion();
    }

    public List<string> ObtenerRegistros()
    {
        return cDLogueo.ObtenerRegistros();
    }

    private CDLogueo datos = new CDLogueo();

    public bool GuardarRegistro(string id, string nombre, string apellido, string imagePath)
    {
        return datos.InsertarRegistro(id, nombre, apellido, imagePath);
    }
}
