using Colruyt_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Lijst_Product lijstproduct = DatabaseOperations.ProductenOphalenOpLijstMetPId(lijst, id)[0];
            DatabaseOperations.VerwijderenProductInLijst(lijstproduct);
            RefreshList();
        }

        public void RefreshList()
        {
            TotaalPrijs = 0;
            productenInBoodschappenLijst = DatabaseOperations.ProductenOphalenOpLijst(lijst);
            List<int> productIdLijst = new List<int>();
            productDisplayLijst = new List<CustomProduct>();

            foreach (Lijst_Product pInLijst in productenInBoodschappenLijst)
            {
                productIdLijst.Add(pInLijst.productId);
                productIdLijst = productIdLijst.Distinct().ToList();
            }

            foreach (int id in productIdLijst)
            {
                Console.WriteLine("productIdLijst:" + id);
            }

            foreach (int productId in productIdLijst)
            {
                Product pInWinkel = DatabaseOperations.ProductOphalenOpId(productId);
                customproduct = new CustomProduct();
                customproduct.Aantal = DatabaseOperations.ProductenOphalenOpLijstMetPId(lijst, pInWinkel.id).Count();
                customproduct.Naam = "(" + customproduct.Aantal + ") " + pInWinkel.naam;
                customproduct.Id = pInWinkel.id;
                customproduct.Optelprijs = pInWinkel.prijs;
                customproduct.Prijs = $"€ {pInWinkel.prijs}";
                Console.WriteLine(customproduct.Naam + "(" + customproduct.Aantal + ")");
                productDisplayLijst.Add(customproduct);
            }

            foreach (CustomProduct CPprijs in productDisplayLijst)
            {
                TotaalPrijs += CPprijs.Optelprijs * CPprijs.Aantal;
            }

            lblTotaalprijs.Content = $"Totaalprijs: € {TotaalPrijs}";

            lstBoodschappenlijsten.ItemsSource = null;
            lstBoodschappenlijsten.Items.Clear();
            lstBoodschappenlijsten.ItemsSource = productDisplayLijst;
            lstBoodschappenlijsten.Items.Refresh();
        }

        internal class CustomProduct
        {
            public string Naam { get; set; }
            public int Id { get; set; }
            public string Prijs { get; set; }
            public decimal? Optelprijs { get; set; }
            public int Aantal { get; set; }
        }

    }
}
