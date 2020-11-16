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
        List<string> gebruikersMailLijst = new List<string>();

        PasswordHasher secure = new PasswordHasher();

        SolidColorBrush rood = new SolidColorBrush(Colors.Red);

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
            string email = txtEmailadresLogin.Text.ToLower();
            string wachtwoord = pswWachtwoordLogin.Password;
            bool checkEmail = string.IsNullOrWhiteSpace(email);
            bool checkWachtwoord = string.IsNullOrWhiteSpace(wachtwoord);

            //Warnings en gebruikersMAilLijst leegmaken
            lblLoginWarnings.Content = "";
            gebruikersMailLijst.Clear();

            //Als email van gebruiker bestaan in databse voeg Succes toe aan lijst
            foreach (Login gebruikerCheck in GebruikerLijst)
            {
                gebruikersMailLijst.Add($"{secure.VerifyHashedPassword(gebruikerCheck.email, email)}");
            }
            
            //Als gebruikersnaam en wachtwoord niet leeg zijn!
            if (!checkEmail)
            {
                if (!checkWachtwoord)
                {
                    if (gebruikersMailLijst.Contains("Success"))
                    {
                        foreach (Login gebruiker in GebruikerLijst)
                        {
                            if (secure.VerifyHashedPassword(gebruiker.wachtwoord, wachtwoord) == PasswordHasher.PasswordVerificationResult.Success)
                            {
                                OverzichtBoodschappenlijsten overzichtBoodschappenlijsten = new OverzichtBoodschappenlijsten(gebruiker);
                                this.Close();
                                overzichtBoodschappenlijsten.Show();
                            }
                            else
                            {
                                PrintScherm("❌ Wachtwoord klopt niet!", rood);
                            }
                        }
                    }
                    else
                    {
                        PrintScherm("❌ Gebruiker bestaat niet. Registreer u!", rood);
                    }
                }
                else
                {
                    PrintScherm("❌ Wachtwoord mag niet leeg zijn!", rood);
                }
            }
            else
            {
                PrintScherm("❌ Email mag niet leeg zijn!", rood);
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

        private void PrintScherm(string zin, SolidColorBrush kleur)
        {
            lblLoginWarnings.Foreground = kleur;
            lblLoginWarnings.Content = zin;
        }
    }
}
