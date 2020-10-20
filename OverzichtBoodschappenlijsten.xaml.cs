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
    /// Interaction logic for OverzichtBoodschappenlijsten.xaml
    /// </summary>
    public partial class OverzichtBoodschappenlijsten : Window
    {
        
        public OverzichtBoodschappenlijsten()
        {
            InitializeComponent();
        }

        private void btnNieuwBoodschaplijst_Click(object sender, RoutedEventArgs e)
        {
           
            LijstBewerkenToevoegen lijstBewerkenToevoegen = new LijstBewerkenToevoegen();
            this.Hide();
            lijstBewerkenToevoegen.Show();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
            
        }
    }
}
