using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;


namespace ClassLibraryPOSG
{
    class BaseDatos
    {

        protected SqlConnection ObtenerConexion()
        {
            // Leer la conexión desde el Web.config del proyecto principal
            string cadenaConexion = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
    }
}
