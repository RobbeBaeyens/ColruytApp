using System;
using System.Collections.Generic;
using System.IO;
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
using Colruyt_WPF.Dialog;
using Newtonsoft.Json;

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

        Helper helperClass = new Helper();


        public Gebruikers()
        {
            InitializeComponent();
            txtEmailadresLogin.Focus();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GebruikerLijst = DatabaseOperations.OphalenGebruikers();
            RememberMeData rememberMeData = helperClass.RemeberedMe();
            if (rememberMeData.RememberMe)
            {
                foreach (Login gebruiker in GebruikerLijst)
                {
                    if ((gebruiker.wachtwoord == rememberMeData.Password) && (gebruiker.email == rememberMeData.Email))
                    {
                        OverzichtBoodschappenlijsten overzichtBoodschappenlijsten = new OverzichtBoodschappenlijsten(gebruiker);
                        this.Close();
                        overzichtBoodschappenlijsten.Show();
                    }
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Attributen initialiseren
            string email = txtEmailadresLogin.Text.ToLower();
            string wachtwoord = pswWachtwoordLogin.Password;
            bool checkEmail = string.IsNullOrWhiteSpace(email);
            bool checkWachtwoord = string.IsNullOrWhiteSpace(wachtwoord);


            GebruikerLijst = DatabaseOperations.OphalenGebruikers();

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
                            if ((secure.VerifyHashedPassword(gebruiker.wachtwoord, wachtwoord) == PasswordHasher.PasswordVerificationResult.Success) && (secure.VerifyHashedPassword(gebruiker.email, email) == PasswordHasher.PasswordVerificationResult.Success))
                            {
                                if (cbxHerinner.IsChecked ?? false)
                                {
                                    helperClass.RememberMe(true, gebruiker);
                                }
                                else
                                {
                                    helperClass.RememberMe(false, gebruiker);
                                }

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

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WachtwoordVergeten();
            if (dialog.ShowDialog() == true)
            {
                MessageBox.Show("You said: " + dialog.ResponseText);
            }
        }

        private bool hitEnter(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                return true;
            return false;
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (hitEnter(e))
                btnLogin_Click(sender, e);
        }        

        
    }
}
