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
    /// Interaction logic for Boodschappenlijst.xaml
    /// </summary>
    public partial class Boodschappenlijst : Window
    {
        Helper helperclass = new Helper();
        Login gebruiker = null;
        Lijst lijst = null;

        public Boodschappenlijst()
        {
            InitializeComponent();
        }
        public Boodschappenlijst(Login gebruiker, Lijst lijst)
        {
            InitializeComponent();

            this.gebruiker = gebruiker;
            this.lijst = lijst;
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            helperclass.DataPasses(this, new OverzichtBoodschappenlijsten(gebruiker), gebruiker);
        }

        private void btnNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            //helperclass.DataPasses(this, new CategorieScherm(gebruiker, lijst), gebruiker);
        }
    }
}
