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
    /// Interaction logic for Categorie.xaml
    /// </summary>
    public partial class CategorieScherm : Window
    {
        //Attributen
        Login gebruiker = null;
        Lijst lijst = null;
        LijstProducten LijstProducten;
        List<Categorie> categorieLijst = new List<Categorie>();
        Helper helper = new Helper();



        public CategorieScherm()
        {
            InitializeComponent();
        }

        public CategorieScherm(Login gebruiker, Lijst lijst)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;
            this.lijst = lijst;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categorieLijst = DatabaseOperations.OphalenCatogorieën();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            if (lijst != null)
            {
                helper.DataPasses(this, new Boodschappenlijst(gebruiker, lijst), gebruiker);
            }
        }



        //Categorie knoppen
        private void btnCategorie_Click(object sender, RoutedEventArgs e)
        {
            var categorie = ((Button)sender).Tag;
            ZoekCategorie(categorie.ToString());
        }


        //Categorie Zoeken
        private void ZoekCategorie(string catInput)
        {
            foreach (Categorie cat in categorieLijst)
            {
                //Zowel equals als contains ==> zekere validatie
                if (cat.naam.Equals(catInput) || cat.naam.Contains(catInput))
                {
                    if (lijst != null)
                    {
                        helper.DataPasses(this, new LijstProducten(gebruiker, cat, lijst), gebruiker);
                    }
                }
            }
        }
    }
}
