using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_Financiero
{
    public partial class MainWindow : Window
    {
        private SQLiteConnection miConexionSql;
        
        public MainWindow()
        {
            InitializeComponent();

            ConexionDB vConexion = new ConexionDB();
            miConexionSql = vConexion.ObtenerConexion();

            VerCombos();
        }

        private void VerCombos() //Ver los meses y los años en el ComboBox
        {
            string consulta = "SELECT * FROM Meses";
            string consultaAnno = "SELECT * FROM Annos";

            SQLiteCommand miSqlCommand = new SQLiteCommand(consulta, miConexionSql);
            SQLiteCommand miSqlCommandAnno = new SQLiteCommand(consultaAnno, miConexionSql);

            miConexionSql.Open();

            DataTable resultados = new DataTable();
            SQLiteDataAdapter miAdaptadorSql = new SQLiteDataAdapter(miSqlCommand);
            miAdaptadorSql.Fill(resultados);

            comboMes.DisplayMemberPath = "Mes";
            comboMes.SelectedValuePath = "Id";
            comboMes.ItemsSource = resultados.DefaultView;


            DataTable resultadosAnno = new DataTable();
            SQLiteDataAdapter miAdaptadorSqlAnno = new SQLiteDataAdapter(miSqlCommandAnno);
            miAdaptadorSqlAnno.Fill(resultadosAnno);

            comboAnno.DisplayMemberPath = "Anno";
            comboAnno.SelectedValuePath = "Id";
            comboAnno.ItemsSource = resultadosAnno.DefaultView;

            miConexionSql.Close();
        }

        private void ActualizarDinero()
        {
            string consultaAltas = "SELECT SUM(Altas) AS TotalAltas FROM Ingresos WHERE IdAnno = @idAnno AND IdMes = @idMes";
            string consultaBajas = "SELECT SUM(Bajas) AS TotalBajas FROM Gastos WHERE IdAnno = @idAnno AND IdMes = @idMes";

            SQLiteCommand miSqlCommandAltas = new SQLiteCommand(consultaAltas, miConexionSql);
            SQLiteCommand miSqlCommandBajas = new SQLiteCommand(consultaBajas, miConexionSql);

            miConexionSql.Open();

            miSqlCommandAltas.Parameters.AddWithValue("@idAnno", comboAnno.SelectedValue);
            miSqlCommandAltas.Parameters.AddWithValue("@idMes", comboMes.SelectedValue);

            miSqlCommandBajas.Parameters.AddWithValue("@idAnno", comboAnno.SelectedValue);
            miSqlCommandBajas.Parameters.AddWithValue("@idMes", comboMes.SelectedValue);

            //Arreglar que el elemento id no lo encuentra
            try 
            { 
                DataTable resultadosAltas = new DataTable();
                SQLiteDataAdapter miAdaptadorSqlAltas = new SQLiteDataAdapter(miSqlCommandAltas);
                miAdaptadorSqlAltas.Fill(resultadosAltas);

                DataTable resultadosBajas = new DataTable();
                SQLiteDataAdapter miAdaptadorSqlBajas = new SQLiteDataAdapter(miSqlCommandBajas);
                miAdaptadorSqlBajas.Fill(resultadosBajas);

                double AltasT = Convert.ToDouble(resultadosAltas.Rows[0]["TotalAltas"]);
                double BajasT = Convert.ToDouble(resultadosBajas.Rows[0]["TotalBajas"]);

                dineroActual.Text = (AltasT - BajasT).ToString();

            }
            catch (Exception e)
            {
                dineroActual.Text = "0€";
            }

            miConexionSql.Close();
        }

        
        private void comboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboAnno.SelectedItem != null) 
            {
                ActualizarDinero();
            }
        }

        private void comboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboMes.SelectedItem != null)
            {
                ActualizarDinero();
            }
        }

        //Botón ingresos
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboAnno.SelectedItem != null && comboMes.SelectedItem != null)
            {
                int idMes = int.Parse(comboMes.SelectedValue.ToString());
                int idAnno = int.Parse(comboAnno.SelectedValue.ToString());

                AIngresos VentanaIngresos = new AIngresos(idMes, idAnno);
                VentanaIngresos.ShowDialog();
                ActualizarDinero();
                Console.Beep();
            }
            else MessageBox.Show("No puedes ingresar nada si no marcas correctamente el mes y el año");
        }
        
        //Botón añadir gasto
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (comboAnno.SelectedItem != null && comboMes.SelectedItem != null)
            {
                int idMes = int.Parse(comboMes.SelectedValue.ToString());
                int idAnno = int.Parse(comboAnno.SelectedValue.ToString());

                AGastos VentanaGastos = new AGastos(idMes, idAnno);
                VentanaGastos.ShowDialog();
                ActualizarDinero();
                Console.Beep();
            }
            else MessageBox.Show("No puedes gastar nada si no marcas correctamente el mes y el año");
        }
    }
}