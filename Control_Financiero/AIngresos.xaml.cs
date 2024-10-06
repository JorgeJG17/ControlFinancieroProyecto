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
using Control_Financiero.Bases;

namespace Control_Financiero
{

    public partial class AIngresos : Window{
        public AIngresos(int idMes, int idAnno){

            InitializeComponent();

            this.idMes = idMes;
            this.idAnno = idAnno;
        }
        
        //Añadir ingreso
        private void Button_Click(object sender, RoutedEventArgs e){

            //Modificacion en realizar la consulta de insetar, he decidio poner una Array object para pasar varios tipos de datos a nuestra clase
            string consulta = "INSERT INTO Ingresos (Altas, TipoAltas, IdMes, IdAnno) VALUES (@Dinero, @Tipo, @IdMes, @IdAnno)";
            
            string[] arrayA = {"@Dinero", "@Tipo", "@IdMes", "@IdAnno"};
            Object[] arrayO = {cuadroDinero.Text, cuadroTipo.Text, idMes, idAnno};

            ConsultasDB.Insertar(consulta, arrayA, arrayO);

            Close();
        }

        private int idMes;
        private int idAnno;
    }
}
