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
using Colruyt_DAL;

namespace Colruyt_WPF
{
    /// <summary>
    /// Interaction logic for Gebruikers.xaml
    /// </summary>
    public partial class Gebruikers : Window
    {
        //Atributen declareren
        List<Login> GebruikerLijst = new List<Login>();
        private string gebruikersnaam;
        private string wachtwoord;
        private bool checkGebruikersnaam;
        bool checkWachtwoord;
        SolidColorBrush rood = new SolidColorBrush(Colors.Red);
        SolidColorBrush groen = new SolidColorBrush(Colors.Green);
        SolidColorBrush zwart = new SolidColorBrush(Colors.Black);

        public Gebruikers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GebruikerLijst = DatabaseOperations.OphalenGebruikers();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            //Attributen initialiseren
            gebruikersnaam = txtGebruikersnaamLogin.Text;
            wachtwoord = pswWachtwoordLogin.Password;
            checkGebruikersnaam = string.IsNullOrWhiteSpace(gebruikersnaam);
            checkWachtwoord = string.IsNullOrWhiteSpace(wachtwoord);

            lblLoginWarnings.Content = "";

            //Input gebruikersnaam checken
            if (checkGebruikersnaam) 
            {
                lblLoginWarnings.Content = "Gebruikersnaam mag niet leeg zijn!\n";
            }

            //Input wachtwoord checken!
            if (checkWachtwoord)
            {
                lblLoginWarnings.Content += "Wachtwoord mag niet leeg zijn!";
            }

            //Als gebruikersnaam en wachtwoord niet leeg zijn!
            if (!checkGebruikersnaam && !checkWachtwoord)
            {
                foreach (Login gebruiker in GebruikerLijst)
                {
                    if (gebruiker.gebruikersnaam == gebruikersnaam)
                    {
                        if (gebruiker.wachtwoord == wachtwoord)
                        {
                            OverzichtBoodschappenlijsten overzichtBoodschappenlijsten = new OverzichtBoodschappenlijsten();
                            this.Close();
                            overzichtBoodschappenlijsten.Show();
                        }
                    }
                }
            }

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            GebruikerToevoegen gebruikerToevoegen = new GebruikerToevoegen();
            this.Close();
            gebruikerToevoegen.Show();
        }

        private void txtGebruikersnaamLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblLoginWarnings.Content = "";
        }
    }
}
