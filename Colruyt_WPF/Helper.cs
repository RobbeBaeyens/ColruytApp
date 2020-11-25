using Colruyt_DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                Login gebruiker = new Login() { email = null, wachtwoord = null };
                RememberMe(false, gebruiker);

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




        string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ColruytApp";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ColruytApp\\rememberme.json";

        public void RememberMe(bool remember, Login gebruiker)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            Console.WriteLine();
            Console.WriteLine("GetFolderPath: {0}\\ColruytApp\\rememberme.json",
                         Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            string email = remember ? gebruiker.email : null;
            string ww = remember ? gebruiker.wachtwoord : null;

            Console.WriteLine("write");
            Console.WriteLine(remember);
            if(remember)
            {
                Console.WriteLine(email);
                Console.WriteLine(ww);
            }

            RememberMeData rememberMeData = new RememberMeData
            {
                RememberMe = remember,
                Email = email,
                Password = ww
            };
            try
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, rememberMeData);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public RememberMeData RemeberedMe()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    RememberMeData rememberMeData = (RememberMeData)serializer.Deserialize(file, typeof(RememberMeData));

                    Console.WriteLine("read");
                    Console.WriteLine(rememberMeData.RememberMe);
                    if(rememberMeData.RememberMe)
                    {
                        Console.WriteLine(rememberMeData.Email);
                        Console.WriteLine(rememberMeData.Password);
                    }

                    return rememberMeData;

                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return new RememberMeData() { Email = "", Password = "", RememberMe = false };
        }
    }
}
