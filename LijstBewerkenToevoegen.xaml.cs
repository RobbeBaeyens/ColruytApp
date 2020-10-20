using System;
using System.Collections.Generic;
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

namespace ColruytApp
{
    /// <summary>
    /// Interaction logic for LijstBewerkenToevoegen.xaml
    /// </summary>
    public partial class LijstBewerkenToevoegen : Window
    {
        public LijstBewerkenToevoegen()
        {
            InitializeComponent();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            OverzichtBoodschappenlijsten boodschappenlijsten = new OverzichtBoodschappenlijsten();
            this.Hide();
            boodschappenlijsten.Show();
        }
    }
}
