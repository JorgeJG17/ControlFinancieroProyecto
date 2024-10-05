using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_Financiero
{
    public partial class ConexionDB
    {
        private SQLiteConnection miConexionSql;

        public ConexionDB()
        {
            string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            miConexionSql = new SQLiteConnection(cadena);
        }

        public SQLiteConnection ObtenerConexion()
        {
            return miConexionSql;
        }

    }
}
