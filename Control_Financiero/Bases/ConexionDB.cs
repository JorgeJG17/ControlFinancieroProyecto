using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_Financiero.Bases
{
    public class ConexionDB
    {

        private static String cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static SQLiteConnection miConexionSql = new SQLiteConnection(cadena);

        protected static SQLiteConnection ObtenerConexion()
        {
            return miConexionSql;
        }
    }
}
