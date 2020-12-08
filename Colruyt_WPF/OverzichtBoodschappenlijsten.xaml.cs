using Colruyt_DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for OverzichtBoodschappenlijsten.xaml
    /// </summary>
    public partial class OverzichtBoodschappenlijsten : Window
    {
        List<Lijst> boodschappenlijsten;
        List<CustomLijst> customLijstLijst = new List<CustomLijst>();

        Login gebruiker = null;
        Helper helperClass = new Helper();

        public OverzichtBoodschappenlijsten()
        {
            InitializeComponent();
        }

        public OverzichtBoodschappenlijsten(Login gebruiker)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;

            Title = "Colruyt boodschappenlijsten | Welkom " + gebruiker.gebruikersnaam + "!";

            LoadLijsten(gebruiker);
        }

        private void LoadLijsten(Login gebruiker)
        {
            lstBoodschappenlijsten.ItemsSource = null;
            customLijstLijst.Clear();
            lstBoodschappenlijsten.Items.Clear();
            lstBoodschappenlijsten.Items.Refresh();

            boodschappenlijsten = DatabaseOperations.OphalenLijstjes(gebruiker);

            foreach (Lijst lijst in boodschappenlijsten)
            {
                CustomLijst customLijst = new CustomLijst();
                customLijst.Lijst = lijst;
                customLijst.HexValue = (SolidColorBrush)(new BrushConverter().ConvertFrom(lijst.kleurHex));
                System.Drawing.Color color = HexToColor(lijst.kleurHex);
                double colorBrightness = color.GetBrightness();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(colorBrightness);
                customLijst.ColorInitial = (colorBrightness > .49) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                customLijst.Initial = lijst.naam.Substring(0, 1).ToUpper();
                customLijst.Id = lijst.id.ToString();
                customLijstLijst.Add(customLijst);
            }

            lstBoodschappenlijsten.ItemsSource = customLijstLijst;
            lstBoodschappenlijsten.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            helperClass.Logout(this);
        }

        private void btnNieuweBoodschappenlijst_Click(object sender, RoutedEventArgs e)
        {
            helperClass.DataPasses(this, new LijstBewerkenToevoegen(gebruiker), gebruiker);
        }

        private void lijstBewerken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(((Button)sender).Tag.ToString());
                Lijst lijst = DatabaseOperations.OphalenLijst(id);
                helperClass.DataPasses(this, new LijstBewerkenToevoegen(gebruiker, lijst), gebruiker);
            }
            catch(Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
            }
        }

        private void lijstVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(((Button)sender).Tag.ToString());
            try
            {
                Lijst lijst = DatabaseOperations.OphalenLijst(id);
                List<Lijst_Product> lijst_Products = DatabaseOperations.ProductenOphalenOpLijst(lijst);
                int hasProducts = lijst_Products.Count();
                if(hasProducts >= 1)
                {
                    MessageBoxResult result = MessageBox.Show("Boodschappenlijst " + lijst.naam + " bevat nog producten.\nBen je zeker dat je deze volledig wilt verwijderen?", "Boodschappenlijst", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if(result == MessageBoxResult.Yes)
                    {
                        foreach(Lijst_Product lijst_Product in lijst_Products)
                        {
                            DatabaseOperations.VerwijderenProductInLijst(lijst_Product);
                        }

                        if (DatabaseOperations.VerwijderenLijstje(lijst) == 1)
                        {
                            LoadLijsten(gebruiker);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("verwijder " + id);
                        }
                    }
                }
                else
                {
                    if (DatabaseOperations.VerwijderenLijstje(lijst) == 1)
                    {
                        LoadLijsten(gebruiker);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("verwijder " + id);
                    }
                }
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.WriteLine("verwijder " + id + " mislukt");
            }
        }

        private static System.Drawing.Color HexToColor(string hexColor)
        {
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            byte red = 0;
            byte green = 0;
            byte blue = 0;

            if (hexColor.Length == 8)
            {
                //We need to remove the preceding FF
                hexColor = hexColor.Substring(2);
            }

            if (hexColor.Length == 6)
            {
                //#RRGGBB
                red = byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                //#RGB
                red = byte.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = byte.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = byte.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
            }

            return System.Drawing.Color.FromArgb(1, red, green, blue);
        }

        private void LijstSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(((Button)sender).Tag.ToString());
                Lijst lijst = DatabaseOperations.OphalenLijst(id);
                helperClass.DataPasses(this, new Boodschappenlijst(gebruiker, lijst), gebruiker);
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
            }
        }

        private void lstBoodschappenlijsten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = lstBoodschappenlijsten.SelectedIndex;
                int lijstId = customLijstLijst[index].Lijst.id;
                Lijst lijst = DatabaseOperations.OphalenLijst(lijstId);
                helperClass.DataPasses(this, new Boodschappenlijst(gebruiker, lijst), gebruiker);
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
            }
        }
    }
}
