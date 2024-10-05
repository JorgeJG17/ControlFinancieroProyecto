using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Control_Financiero{

    public partial class AIngresos : Window{

        private SQLiteConnection miConexionSql;
        public AIngresos(int idMes, int idAnno){

            InitializeComponent();

            this.idMes = idMes;
            this.idAnno = idAnno;

            ConexionDB vConexion = new ConexionDB();
            miConexionSql = vConexion.ObtenerConexion();
        }
        
        //Añadir ingreso
        private void Button_Click(object sender, RoutedEventArgs e){

            miConexionSql.Open();
            string consulta = "INSERT INTO Ingresos (Altas, TipoAltas, IdMes, IdAnno) VALUES (@Dinero, @Tipo, @IdMes, @IdAnno)";
            SQLiteCommand miSqlCommand = new SQLiteCommand(consulta, miConexionSql);

            miSqlCommand.Parameters.AddWithValue("@Dinero", cuadroDinero.Text);
            miSqlCommand.Parameters.AddWithValue("@Tipo", cuadroTipo.Text);
            miSqlCommand.Parameters.AddWithValue("@IdMes", idMes);
            miSqlCommand.Parameters.AddWithValue("@IdAnno", idAnno);

            miSqlCommand.ExecuteNonQuery();
            miConexionSql.Close();

            Close();
        }

        private int idMes;
        private int idAnno;
    }
}
