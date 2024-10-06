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
using Control_Financiero.Bases;

namespace Control_Financiero
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            VerCombos();
        }

        //Ver los meses y los años en el ComboBox
        private void VerCombos(){ //Hemos cambiado la manera de realizar la consultas, creando una clase para ello
            //así poder reutilizar el código en un futuro

            string consulta = "SELECT * FROM Meses";
            string consultaAnno = "SELECT * FROM Annos";

            //Llamamos al método de nuestra clase que hemos creado y nos devuelve directamnet el resultado de la consulta
            DataTable r = ConsultasDB.Seleccionar(consulta);

            comboMes.DisplayMemberPath = "Mes";
            comboMes.SelectedValuePath = "Id";
            comboMes.ItemsSource = r.DefaultView;

            DataTable ra = ConsultasDB.Seleccionar(consultaAnno);

            comboAnno.DisplayMemberPath = "Anno";
            comboAnno.SelectedValuePath = "Id";
            comboAnno.ItemsSource = ra.DefaultView;
        }

        private void ActualizarDinero()
        {
            //Hemos realizado un cambio a la hora de realizar consultas
            string consultaAltas = "SELECT SUM(Altas) AS TotalAltas FROM Ingresos WHERE IdAnno = @idAnno AND IdMes = @idMes";
            string consultaBajas = "SELECT SUM(Bajas) AS TotalBajas FROM Gastos WHERE IdAnno = @idAnno AND IdMes = @idMes";

            string[] arrayA = {"@idAnno", "@idMes"};
            Object[] arrayO = {comboAnno.SelectedValue, comboMes.SelectedValue};

            DataTable r = ConsultasDB.Seleccionar(consultaAltas, arrayA, arrayO);
            DataTable r2 = ConsultasDB.Seleccionar(consultaBajas, arrayA, arrayO);

            try{

                double AltasT = Convert.ToDouble(r.Rows[0]["TotalAltas"]);
                double BajasT = Convert.ToDouble(r2.Rows[0]["TotalBajas"]);

                dineroActual.Text = (AltasT - BajasT).ToString();
            }
            catch (Exception)
            {

                dineroActual.Text = "0€";
            }
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