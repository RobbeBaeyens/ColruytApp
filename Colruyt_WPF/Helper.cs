using Colruyt_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Colruyt_WPF
{
    class Helper
    {
        public bool DataPasses(Window thisWindow, Window targetWindow, Login gebruiker)
        {
            if (gebruiker != null)
            {
                thisWindow.Close();
                targetWindow.Show();
                return true;
            }
            else
            {
                MessageBox.Show("Er is iets fout gelopen met de login", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                Gebruikers gebruikers = new Gebruikers();
                thisWindow.Close();
                gebruikers.Show();
                return false;
            }
        }

        public void Logout(Window thisWindow)
        {
            MessageBoxResult result = MessageBox.Show("Bent u zeker dat u terug wilt gaan?\n(U wordt uitgelogd als u doorgaat.)", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Gebruikers gebruikers = new Gebruikers();
                thisWindow.Close();
                gebruikers.Show();
            }
        }

        public bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }
}
