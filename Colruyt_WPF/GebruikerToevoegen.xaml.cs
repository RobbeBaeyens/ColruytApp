using Colruyt_DAL;
using System.Windows;
using System.Windows.Media;

namespace Colruyt_WPF
{
    /// <summary>
    /// Interaction logic for GebruikerToevoegen.xaml
    /// </summary>
    public partial class GebruikerToevoegen : Window
    {
        SolidColorBrush error = new SolidColorBrush(Colors.Red);
        SolidColorBrush correct = new SolidColorBrush(Colors.Green);

        public GebruikerToevoegen()
        {
            InitializeComponent();
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
            if (CheckForm(true)){
                Login login = new Login();
                login.gebruikersnaam = txtGebruikersnaam.Text;
                login.wachtwoord = pswWachtwoord.Password;
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
            bool wwVereisten = false;

            if (!string.IsNullOrWhiteSpace(pswWachtwoord.Password))
            {
                if (pswWachtwoord.Password.Length >= 5)
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
                    lblWachtwoordAlert.Content = "❌ Wachtwoord moet minstens 5 tekens bevatten!";
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
                if (!string.IsNullOrWhiteSpace(gebruikersNaam))
                {
                    if (gebruikersNaam.Length >= 3)
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

    }
}
