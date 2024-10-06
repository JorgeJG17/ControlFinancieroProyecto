using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Financiero.Bases
{
    public class ConsultasDB : ConexionDB
    {
        private static SQLiteConnection miConexionSql = ObtenerConexion();

        public static DataTable Seleccionar(string consulta){ 

            SQLiteCommand miSqlCommand = new SQLiteCommand(consulta, miConexionSql);
            miConexionSql.Open();

            DataTable resultados = new DataTable();
            SQLiteDataAdapter miAdaptadorSql = new SQLiteDataAdapter(miSqlCommand);
            miAdaptadorSql.Fill(resultados);
            miConexionSql.Close();

            return resultados;
        }

        public static DataTable Seleccionar(string consulta, string[] array, Object[] arrayO)
        {

            SQLiteCommand miSqlCommand = new SQLiteCommand(consulta, miConexionSql);
            miConexionSql.Open();

            for (int i = 0; i < array.Length; i++)
            {

                miSqlCommand.Parameters.AddWithValue(array[i], arrayO[i]);
            }

            DataTable resultados = new DataTable();
            SQLiteDataAdapter miAdaptadorSql = new SQLiteDataAdapter(miSqlCommand);
            miAdaptadorSql.Fill(resultados);

            miConexionSql.Close();
            return resultados;
        }


        public static void Insertar(string consulta, string[] array, Object[] arrayO)
        {
            miConexionSql.Open();
            SQLiteCommand miSqlCommand = new SQLiteCommand(consulta, miConexionSql);

            for (int i = 0; i < array.Length; i++)
            {

                miSqlCommand.Parameters.AddWithValue(array[i], arrayO[i]);
            }

            miSqlCommand.ExecuteNonQuery();
            miConexionSql.Close();
        }

        /*public static void Borrar()
        {
            //Sin realizar aún
        }*/
    }
}
