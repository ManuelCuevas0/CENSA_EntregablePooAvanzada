using System.Collections.Generic;

public class CNLogueo
{
    CDLogueo cDLogueo = new CDLogueo();

    public void PruebaMysql()
    {
        cDLogueo.PruebaConexion();
    }

    public List<string> ObtenerRegistros()
    {
        return cDLogueo.ObtenerRegistros();
    }
}
