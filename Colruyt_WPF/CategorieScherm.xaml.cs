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
        Login Gebruiker;
        LijstProducten LijstProducten;
        List<Categorie> categorieLijst = new List<Categorie>();
        Categorie categorie = new Categorie();
        public string CategorieNaam = "";



        public CategorieScherm()
        {
            InitializeComponent();
        }

        public CategorieScherm(Login gebruiker)
        {
            Gebruiker = gebruiker;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categorieLijst = DatabaseOperations.OphalenCatogorieën();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {

        }



        //Categorie knoppen
        private void btnWijn_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Wijn";
            ZoekCategorie(CategorieNaam);
        }

        private void btnChips_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Chips";
            ZoekCategorie(CategorieNaam);
        }

        private void btnKoekjes_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Koekjes";
            ZoekCategorie(CategorieNaam);
        }

        private void btnDieet_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Dieetvoeding";
            ZoekCategorie(CategorieNaam);
        }

        private void btnLichaamVerzorging_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Lichaamsverzorging";
            ZoekCategorie(CategorieNaam);
        }

        private void btnHuisdieren_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Huisdieren";
            ZoekCategorie(CategorieNaam);
        }

        private void btnOnderhoud_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Onderhoud";
            ZoekCategorie(CategorieNaam);
        }

        private void btnNietVoeding_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Niet voeding";
            ZoekCategorie(CategorieNaam);
        }

        private void btnGroenten_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Groenten";
            ZoekCategorie(CategorieNaam);
        }

        private void btnZuivel_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "zuivel";
            ZoekCategorie(CategorieNaam);
        }

        private void btnBroodje_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Brood";
            ZoekCategorie(CategorieNaam);
        }

        private void btnKruiden_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Kruidenierswaren";
            ZoekCategorie(CategorieNaam);
        }

        private void btnBereiden_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Bereidingen";
            ZoekCategorie(CategorieNaam);
        }

        private void Beenhouwerij_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Beenhouwerij";
            ZoekCategorie(CategorieNaam);
        }

        private void btnConserven_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Conserven";
            ZoekCategorie(CategorieNaam);
        }

        private void btnBaby_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Baby";
            ZoekCategorie(CategorieNaam);
        }

        private void btnDranken_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Dranken";
            ZoekCategorie(CategorieNaam);
        }

        private void btnDiepvries_Click(object sender, RoutedEventArgs e)
        {
            CategorieNaam = "Diepvries";
            ZoekCategorie(CategorieNaam);
        }


        //Categorie Zoeken
        private void ZoekCategorie(string naam)
        {
            foreach (Categorie catNaam in categorieLijst)
            {
                if (catNaam.naam.Contains(naam))
                {
                    LijstProducten = new LijstProducten(Gebruiker, catNaam);
                }
            }
        }
    }
}
