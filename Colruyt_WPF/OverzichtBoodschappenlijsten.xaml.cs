using Colruyt_DAL;
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

namespace Colruyt_WPF
{
    /// <summary>
    /// Interaction logic for OverzichtBoodschappenlijsten.xaml
    /// </summary>
    public partial class OverzichtBoodschappenlijsten : Window
    {
        List<Lijst> boodschappenlijsten = new List<Lijst>();
        Lijst lijst = new Lijst();
        List<string> lijstnamen = new List<string>() {"test1", "test2", "test3"};

        public OverzichtBoodschappenlijsten()
        {
            InitializeComponent();
        }

        public OverzichtBoodschappenlijsten(Login gebruiker)
        {
            InitializeComponent();

            Title = "Colruyt boodschappenlijsten | Welkom " + gebruiker.gebruikersnaam + "!";

            lstBoodschappenlijsten.Items.Clear();
            lstBoodschappenlijsten.Items.Refresh();

            foreach (string naam in lijstnamen)
            {
                lijst.naam = naam;
                lijst.datumAangemaakt = DateTime.Now;
                lijst.loginId = gebruiker.id;
                boodschappenlijsten.Add(lijst);
            }
            lstBoodschappenlijsten.ItemsSource = lijstnamen;
            lstBoodschappenlijsten.Items.Refresh();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            Gebruikers gebruikers = new Gebruikers();
            this.Close();
            gebruikers.Show();
        }

        private void btnNieuweBoodschappenlijst_Click(object sender, RoutedEventArgs e)
        {
            LijstBewerkenToevoegen lijstBewerkenToevoegen = new LijstBewerkenToevoegen();
            this.Close();
            lijstBewerkenToevoegen.Show();
        }
    }
}
