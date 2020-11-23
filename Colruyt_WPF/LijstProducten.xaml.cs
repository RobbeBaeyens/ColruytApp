﻿using System;
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
        private Login gebruiker = null;
        private Categorie categorie;
        private Helper helper = new Helper();
        private List<Product> productLijst;
        private List<ProductLijst> productLijstLijst = new List<ProductLijst>();
        public LijstProducten()
        {
            InitializeComponent();
        }

        public LijstProducten(Login gebruiker, Categorie categorie)
        {
            InitializeComponent();

            this.gebruiker = gebruiker;
            this.categorie = categorie;

            Title = $"Colruyt lijst producten uit {categorie.naam}!";
            Console.WriteLine(categorie.naam);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            productLijst = DatabaseOperations.ProductenOphalen();
            lblNaamCategorie.Content = categorie.naam;

            foreach (Product productIn in productLijst)
            {
                ProductLijst ProductLijst = new ProductLijst();
                ProductLijst.Afdruk = $"{productIn.naam} {productIn.prijs}";
                productLijstLijst.Add(ProductLijst);
            }

            lstProductenLijst.ItemsSource = productLijstLijst;
            lstProductenLijst.Items.Refresh();

        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            //helper.DataPasses(this, new CategorieScherm(gebruiker), gebruiker);
            CategorieScherm categorieScherm = new CategorieScherm(gebruiker);
            this.Close();
            categorieScherm.Show();
        }

        internal class ProductLijst
        {
            public string Afdruk { get; set; }
        }
    }
}
