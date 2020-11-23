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
    /// Interaction logic for LijstProducten.xaml
    /// </summary>
    public partial class LijstProducten : Window
    {
        public LijstProducten()
        {
            InitializeComponent();
        }

        public LijstProducten(Login gebruiker, Categorie categorie)
        {
            InitializeComponent();

            Title = $"Colruyt lijst producten uit  " + gebruiker.gebruikersnaam + "!";
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
