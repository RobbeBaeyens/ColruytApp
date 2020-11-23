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

            lstBoodschappenlijsten.Items.Clear();
            lstBoodschappenlijsten.Items.Refresh();

            boodschappenlijsten = DatabaseOperations.OphalenLijstjes(gebruiker);

            foreach(Lijst lijst in boodschappenlijsten)
            {
                CustomLijst customLijst = new CustomLijst();
                customLijst.Lijst = lijst;
                customLijst.HexValue = (SolidColorBrush)(new BrushConverter().ConvertFrom(lijst.kleurHex));
                customLijst.InvertedHexValue = invertColor(lijst.kleurHex);
                customLijst.ColorInitial = lijst.naam.Substring(0, 1).ToUpper();
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


        private static Color HexToColor(string hexColor)
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

            return Color.FromRgb(red, green, blue);
        }
        private SolidColorBrush invertColor(string value)
        {
            if (value != null)
            {
                Color ColorToConvert = HexToColor(value);
                Color invertedColor = Color.FromRgb((byte)~ColorToConvert.R, (byte)~ColorToConvert.G, (byte)~ColorToConvert.B);
                return new SolidColorBrush(invertedColor);
            }
            else
            {
                return new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }
    }
}
