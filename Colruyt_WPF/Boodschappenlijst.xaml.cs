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
        List<Lijst_Product> productenInBoodschappenLijst = new List<Lijst_Product>();
        List<Product> productenInWinkel = new List<Product>();
        Product product = new Product();

        decimal? TotaalPrijs;
        CustomProduct customproduct;
        List<CustomProduct> productDisplayLijst;




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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            helperclass.DataPasses(this, new OverzichtBoodschappenlijsten(gebruiker), gebruiker);
        }

        private void btnNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            helperclass.DataPasses(this, new CategorieScherm(gebruiker, lijst), gebruiker);
        }

        private void btnProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.Tag.ToString());
            Lijst_Product product = new Lijst_Product();
            product.id = id;
            DatabaseOperations.VerwijderenProductInLijst(product);
            RefreshList();
        }

        public void RefreshList()
        {
            TotaalPrijs = 0;
            productenInBoodschappenLijst = DatabaseOperations.ProductenOphalenOpLijst(lijst);
            productenInWinkel = DatabaseOperations.ProductenOphalen();
            productDisplayLijst = new List<CustomProduct>();

            foreach (Lijst_Product pInLijst in productenInBoodschappenLijst)
            {
                foreach (Product pInWinkel in productenInWinkel)
                {
                    if (pInLijst.productId == pInWinkel.id)
                    {
                        customproduct = new CustomProduct();
                        customproduct.Naam = pInWinkel.naam;
                        customproduct.Id = pInLijst.id;
                        customproduct.Optelprijs = pInWinkel.prijs;
                        customproduct.Prijs = $"€ {pInWinkel.prijs}";
                        productDisplayLijst.Add(customproduct);
                    }
                }
            }

            foreach (CustomProduct CPprijs in productDisplayLijst)
            {
                TotaalPrijs += CPprijs.Optelprijs;
            }

            lblTotaalprijs.Content = $"Totaalprijs: € {TotaalPrijs}";

            lstBoodschappenlijsten.ItemsSource = null;
            lstBoodschappenlijsten.Items.Clear();
            lstBoodschappenlijsten.ItemsSource = productDisplayLijst;
            lstBoodschappenlijsten.Items.Refresh();
        }

        internal class CustomProduct
        {
            public Product Product { get; set; }
            public string Naam { get; set; }
            public int Id { get; set; }
            public string Prijs { get; set; }
            public decimal? Optelprijs { get; set; }
            public int Aantal { get; set; }
        }

    }
}
