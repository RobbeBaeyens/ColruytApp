using Colruyt_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for GebruikerToevoegen.xaml
    /// </summary>
    public partial class GebruikerToevoegen : Window
    {
        List<Login> GebruikerLijst = new List<Login>();
        PasswordHasher secure = new PasswordHasher();

        SolidColorBrush error = new SolidColorBrush(Colors.Red);
        SolidColorBrush correct = new SolidColorBrush(Colors.Green);

        Helper helperClass = new Helper();

        public GebruikerToevoegen()
        {
            InitializeComponent();
            txtGebruikersnaam.Focus();
            lblWachtwoordAlert.Content = "❌ Wachtwoord mag niet leeg zijn!";
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            Gebruikers gebruikers = new Gebruikers();
            this.Close();
            gebruikers.Show();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForm(true))
            {
                Login login = new Login();
                login.gebruikersnaam = txtGebruikersnaam.Text.ToLower();
                PasswordHasher secure = new PasswordHasher();
                login.email = secure.HashPassword(txtEmail.Text.ToLower());
                login.wachtwoord = secure.HashPassword(pswWachtwoord.Password);

                if (DatabaseOperations.ToevoegenGebruiker(login) == 1)
                {
                    MessageBox.Show("Succesvol geregistreerd", "Registreer", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
                    Gebruikers gebruikers = new Gebruikers();
                    this.Close();
                    gebruikers.Show();
                }
                else
                {
                    MessageBox.Show("Fout bij registreren, probeer opnieuw!", "Registreer", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
                }
            }
        }

        private void pswWachtwoord2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckForm(false);
        }

        private void pswWachtwoord_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckForm(false);
        }

        private bool CheckForm(bool register)
        {
            string gebruikersNaam = txtGebruikersnaam.Text;
            string email = txtEmail.Text;
            bool wwVereisten = false;

            if (!string.IsNullOrWhiteSpace(pswWachtwoord.Password))
            {
                if (pswWachtwoord.Password.Length >= 4)
                {
                    if (pswWachtwoord.Password == pswWachtwoord2.Password)
                    {
                        lblWachtwoordAlert.Content = "✔ Wachtwoorden komen overeen!";
                        lblWachtwoordAlert.Foreground = correct;
                        wwVereisten = true;
                    }
                    else
                    {
                        lblWachtwoordAlert.Content = "❌ Wachtwoorden komen niet overeen!";
                        lblWachtwoordAlert.Foreground = error;
                    }
                }
                else
                {
                    lblWachtwoordAlert.Content = "❌ Wachtwoord moet minstens 4 tekens bevatten!";
                    lblWachtwoordAlert.Foreground = error;
                }
            }
            else
            {
                lblWachtwoordAlert.Content = "❌ Wachtwoord mag niet leeg zijn!";
                lblWachtwoordAlert.Foreground = error;
            }

            if (register)
            {
                GebruikerLijst = DatabaseOperations.OphalenGebruikers();

                //Als email van gebruiker bestaan in databse voeg Succes toe aan lijst
                foreach (Login gebruikerCheck in GebruikerLijst)
                {
                    if(secure.VerifyHashedPassword(gebruikerCheck.email, email) == PasswordHasher.PasswordVerificationResult.Success){
                        lblFormAlert.Content = "❌ Er bestaat al een account met dit emailadres!";
                        lblFormAlert.Foreground = error;
                        return false;
                    }                    
                }

                if (!string.IsNullOrWhiteSpace(gebruikersNaam))
                {
                    if (gebruikersNaam.Length >= 3)
                    {
                        if (helperClass.IsValidEmailAddress(email))
                        {
                            if (wwVereisten)
                            {
                                lblFormAlert.Content = "✔ Succesvol geregistreerd!";
                                lblFormAlert.Foreground = correct;
                                return true;
                            }
                            else
                            {
                                lblFormAlert.Content = "❌ Wachtwoord voldoet niet aan vereisten!";
                                lblFormAlert.Foreground = error;
                            }
                        }
                        else
                        {
                            lblFormAlert.Content = "❌ Geef een geldig emailadres in!";
                            lblFormAlert.Foreground = error;
                        }
                    }
                    else
                    {
                        lblFormAlert.Content = "❌ Gebruikersnaam moet minstens 3 tekens bevatten!";
                        lblFormAlert.Foreground = error;
                    }
                }
                else
                {
                    lblFormAlert.Content = "❌ Gebruikersnaam mag niet leeg zijn!";
                    lblFormAlert.Foreground = error;
                }

            }
            return false;
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
                btnRegister_Click(sender, e);
        }
    }
}
