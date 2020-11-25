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

namespace Colruyt_WPF.Dialog
{
    /// <summary>
    /// Interaction logic for WachtwoordVergeten.xaml
    /// </summary>
    public partial class WachtwoordVergeten : Window
    {
        Helper helperClass = new Helper();
        List<Login> gebruikers;

        PasswordHasher secure = new PasswordHasher();

        private string changeWWCode;
        private DateTime timeCodeCreated;

        SolidColorBrush error = new SolidColorBrush(Colors.Red);
        SolidColorBrush correct = new SolidColorBrush(Colors.Green);
        SolidColorBrush info = new SolidColorBrush(Colors.Orange);

        public string ResponseText
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }
        public WachtwoordVergeten()
        {
            InitializeComponent();
            groupEmail.Visibility = Visibility.Visible;
            txtEmail.Focus();
            groupHerstelcode.Visibility = Visibility.Collapsed;
            groupHerstelww.Visibility = Visibility.Collapsed;
            this.Height = 300;

            try
            {
                gebruikers = DatabaseOperations.OphalenGebruikers();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                this.Close();
            }
        }

        private void btnGetWwRestoreCode_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            if(!string.IsNullOrWhiteSpace(email) && helperClass.IsValidEmailAddress(email))
            {
                foreach (Login gebruiker in gebruikers)
                {
                    if (secure.VerifyHashedPassword(gebruiker.email, email) == PasswordHasher.PasswordVerificationResult.Success)
                    {
                        groupEmail.Visibility = Visibility.Collapsed;
                        groupHerstelcode.Visibility = Visibility.Visible;
                        txtRestoreCode.Focus();
                        groupHerstelww.Visibility = Visibility.Collapsed;
                        lblCodeAlert.Text = $"❗ Email met code verzonden naar {email}!";
                        lblCodeAlert.Foreground = info;
                        this.Height = 320;
                        timeCodeCreated = DateTime.Now;
                        changeWWCode = generateID(1, email);
                        MessageBoxResult result = MessageBox.Show($"Code:\n{changeWWCode}\nSent to:\n{email}\n\nThis code is invalid after 5 minutes!\nDo you want to copy the code to clipboard?", "Email placeholder example", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                        if (result == MessageBoxResult.Yes)
                            Clipboard.SetText(changeWWCode);
                    }
                    else
                    {
                        lblEmailAlert.Content = "❌ Deze gebruiker bestaat niet!";
                        lblEmailAlert.Foreground = error;
                    }
                }
            }
            else
            {
                lblEmailAlert.Content = "❌ Geef een geldig emailadres in!";
                lblEmailAlert.Foreground = error;
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            lblEmailAlert.Content = "";
            groupEmail.Visibility = Visibility.Visible;
            txtEmail.Focus();
            groupHerstelcode.Visibility = Visibility.Collapsed;
            groupHerstelww.Visibility = Visibility.Collapsed;
            this.Height = 300;
        }

        private void btnCheckCode_Click(object sender, RoutedEventArgs e)
        {
            string code = txtRestoreCode.Text;
            DateTime timeNow = DateTime.Now;
            TimeSpan timePassed = timeNow - timeCodeCreated;

            if ((changeWWCode == code) && (timePassed.TotalMinutes < 5))
            {
                lblEmailAlert.Content = "";
                groupEmail.Visibility = Visibility.Collapsed;
                groupHerstelcode.Visibility = Visibility.Collapsed;
                groupHerstelww.Visibility = Visibility.Visible;
                txtww1.Focus();
                this.Height = 400;
            }
            else
            {
                lblCodeAlert.Text = "❌ Herstelcode incorrect!";
                lblCodeAlert.Foreground = error;
            }
        }

        private void btnWijzigWw_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ww_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        public string generateID(int generatorType, string sourceUrl = "") // 0: Guid(long)  |  1: GuidWithSourceHash(long)  |  else: timeTickHex(short)
        {
            switch (generatorType)
            {
                case 0:
                    return Guid.NewGuid().ToString("N").ToUpper().Replace("-", "");
                case 1:
                    string hash = "";
                    byte[] hashSet = Encoding.ASCII.GetBytes(sourceUrl);
                    foreach( byte testhash in hashSet)
                    {
                        hash += testhash.ToString("X");
                    }
                    return string.Format("{0}{1:N}", hash, Guid.NewGuid().ToString("N").ToUpper().Replace("-",""));
                default:
                    return long.Parse(DateTime.UtcNow.Ticks.ToString()).ToString("X");
            }
        }

        private bool hitEnter(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                return true;
            return false;
        }

        private bool hitEsc(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                return true;
            return false;
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (hitEnter(e))
                btnGetWwRestoreCode_Click(sender, e);
        }

        private void txtRestoreCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (hitEnter(e))
                btnCheckCode_Click(sender, e);
            if (hitEsc(e))
                btnBack_Click(sender, e);
        }

        private void txtww_KeyDown(object sender, KeyEventArgs e)
        {
            if (hitEnter(e))
                btnWijzigWw_Click(sender, e);
        }
    }
}
