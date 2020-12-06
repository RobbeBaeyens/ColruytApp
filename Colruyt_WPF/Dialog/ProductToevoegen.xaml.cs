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

namespace Colruyt_WPF.Dialog
{
    /// <summary>
    /// Interaction logic for ProductToevoegen.xaml
    /// </summary>
    public partial class ProductToevoegen : Window
    {
        private Product product = new Product();
        private List<Product> productenLijst = new List<Product>();
        private Categorie _categorie = new Categorie();
        private int catid;

        public ProductToevoegen()
        {
            InitializeComponent();
        }

        public ProductToevoegen(Categorie categorie)
        {
            InitializeComponent();

            _categorie = categorie;
            catid = categorie.id;
        }

        private void btnWijzigWw_Click(object sender, RoutedEventArgs e)
        {
            bool test = false;
            bool test2 = false;

            productenLijst = DatabaseOperations.ProductenOphalen(_categorie);
            product.categorieId = catid;

            if (!string.IsNullOrWhiteSpace(txtProductnaam.Text))
            {
                product.naam = txtProductnaam.Text;
            }
            else
            {
                MessageBox.Show("Productnaam mag niet leeg zijn!");
                test2 = true;
            }

            if (!string.IsNullOrWhiteSpace(txtPrijs.Text))
            {
                if (decimal.TryParse(txtPrijs.Text, out decimal prijs))
                {
                    product.prijs = prijs;
                }
                else
                {
                    MessageBox.Show("Geen geldige prijs ingegeven!");
                    test2 = true;
                }
            }
            else
            {
                MessageBox.Show("Prijs mag niet leeg zijn!");
                test2 = true;
            }


            foreach (Product productIL in productenLijst)
            {
                if (productIL.naam == product.naam)
                {
                    test = true;
                }
            }

            if (!test2)
            {
                if (!test)
                {
                    DatabaseOperations.ToevoegenProduct(product);
                    ((LijstProducten)this.Owner).RefreshList();
                }
                else
                {
                    MessageBox.Show("Product bestaat al!");
                }
            }
        }
    }
}
