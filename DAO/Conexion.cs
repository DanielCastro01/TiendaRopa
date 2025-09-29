using System.Configuration;
namespace DAO
{
    public class Conexion
    {
        public static string ruta = ConfigurationManager.ConnectionStrings["cadena"].ToString();


    }
}
