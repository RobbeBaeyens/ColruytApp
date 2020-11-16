using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Colruyt_WPF
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
