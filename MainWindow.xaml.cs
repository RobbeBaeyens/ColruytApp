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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColruytApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> Gebruiker = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            Gebruiker.Add("test1");
            Gebruiker.Add("test2");
            Gebruiker.Add("test3");
            Gebruiker.Add("test4");
            GebruikerLijst.ItemsSource = Gebruiker;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            GebruikerToevoegen gebruikerToevoegen = new GebruikerToevoegen();
            this.Hide();
            gebruikerToevoegen.Show();
            
        }

        private void btnGebruiker_Click(object sender, RoutedEventArgs e)
        {
            OverzichtBoodschappenlijsten overzichtBoodschappenlijsten = new OverzichtBoodschappenlijsten();
            this.Hide();
            overzichtBoodschappenlijsten.Show();
        }
    }
}
