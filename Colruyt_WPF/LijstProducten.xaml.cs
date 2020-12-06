using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Colruyt_DAL;
using Colruyt_WPF.Dialog;

namespace Colruyt_WPF
{
    /// <summary>
    /// Interaction logic for LijstProducten.xaml
    /// </summary>
    public partial class LijstProducten : Window
    {
        private Login gebruiker = null;
        private Lijst lijst = null;
        private Categorie categorie = null;
        private Helper helper = new Helper();
        private List<Product> productLijst = new List<Product>();

        private List<Product> geselecteerdeProducten = new List<Product>();

        public LijstProducten()
        {
            InitializeComponent();
        }

        public LijstProducten(Login gebruiker, Categorie categorie, Lijst lijst)
        {
            InitializeComponent();

            this.gebruiker = gebruiker;
            this.categorie = categorie;
            this.lijst = lijst;

            RefreshList();

            Title = $"Colruyt lijst producten uit {categorie.naam}!";
            Console.WriteLine(categorie.naam);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblNaamCategorie.Content = categorie.naam;
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            if (lijst != null)
            {
                helper.DataPasses(this, new CategorieScherm(gebruiker, lijst), gebruiker);
            }
        }


        private void btnProductToevoegen_Click(object sender, RoutedEventArgs e)
        {
            foreach (Product product in geselecteerdeProducten)
            {
                Lijst_Product lijst_Product = new Lijst_Product();
                lijst_Product.productId = product.id;
                lijst_Product.lijstId = lijst.id;
                DatabaseOperations.ToevoegenProductInLijst(lijst_Product);
            }
        }

        private void lstProductenLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstProductenLijst.SelectedIndex = -1;
        }

        private void tgbSelecteerProduct_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tgb = (ToggleButton)sender;
            int id = int.Parse(tgb.Tag.ToString());
            Console.WriteLine(id);

            Product product = DatabaseOperations.ProductOphalenOpId(id);

            if (tgb.IsChecked == true)
            {
                geselecteerdeProducten.Add(product);
            }
            else
            {
                if (geselecteerdeProducten.Contains(product))
                {
                    geselecteerdeProducten.Remove(product);
                }
            }
        }

        private void btnNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductToevoegen productToevoegen = new ProductToevoegen(categorie);
            productToevoegen.Owner = this;
            productToevoegen.Show();
        }

        //Lijst refresh
        public void RefreshList()
        {
            productLijst.Clear();
            productLijst = DatabaseOperations.ProductenOphalen(categorie);

            lstProductenLijst.ItemsSource = null;
            lstProductenLijst.Items.Clear();
            lstProductenLijst.ItemsSource = productLijst;
            lstProductenLijst.Items.Refresh();
            foreach (Product item in productLijst)
            {
                Console.WriteLine(item.id);
            }
        }

        private void btnVerwijderProduct_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.Tag.ToString());
            Product product = new Product();
            product.id = id;
            DatabaseOperations.VerwijderenProduct(product);
            RefreshList();
        }
    }
}
