using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Colruyt_DAL;

namespace Colruyt_WPF
{
    /// <summary>
    /// Interaction logic for LijstBewerkenToevoegen.xaml
    /// </summary>
    public partial class LijstBewerkenToevoegen : Window
    {
        private Image _theImage;
        private string hexValue = "#FFFFFF";

        SolidColorBrush error = new SolidColorBrush(Colors.Red);
        SolidColorBrush correct = new SolidColorBrush(Colors.Green);

        public Image TheImage
        {
            get
            {
                return _theImage;
            }

            set
            {
                _theImage = value;
            }
        }

        Login gebruiker = null;
        Helper helperClass = new Helper();

        public LijstBewerkenToevoegen()
        {
            InitializeComponent();
            CreateColorPicker();
        }

        public LijstBewerkenToevoegen(Login gebruiker)
        {
            InitializeComponent();
            CreateColorPicker();

            this.gebruiker = gebruiker;
        }

        private void CreateColorPicker()
        {
            DataContext = this;
            TheImage = new Image();
            TheImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/colorpicker.png"));
            TheImage.Width = 300;
            TheImage.Height = 300;
            TheImage.Stretch = Stretch.Fill;
            TheImage.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void ContentControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(TheImage);
            BitmapImage img = new BitmapImage(new Uri(@"pack://application:,,,/images/colorpicker.png"));
            System.Drawing.Bitmap bitmap = ConvertToBitmap(img);
            System.Drawing.Color pixelColor = bitmap.GetPixel((int)clickPoint.X, (int)clickPoint.Y);
            Brush brushColor = new SolidColorBrush(Color.FromArgb(255, (byte)pixelColor.R, (byte)pixelColor.G, (byte)pixelColor.B));
            SelectedColor.Fill = brushColor;

            hexValue = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(255, (byte)pixelColor.R, (byte)pixelColor.G, (byte)pixelColor.B));
        }

        public static System.Drawing.Bitmap ConvertToBitmap(BitmapSource bitmapSource)
        {
            var width = bitmapSource.PixelWidth;
            var height = bitmapSource.PixelHeight;
            var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            var bitmap = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, memoryBlockPointer);
            return bitmap;
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            helperClass.DataPasses(this, new OverzichtBoodschappenlijsten(gebruiker), gebruiker);
        }

        private void btnLijstAanmaken_Click(object sender, RoutedEventArgs e)
        {
            string lijstnaam = txtLijstnaam.Text;

            if(gebruiker != null)
            {
                if (!string.IsNullOrWhiteSpace(lijstnaam) && lijstnaam.Length >= 3)
                {
                    Lijst nieuweLijst = new Lijst();
                    nieuweLijst.datumAangemaakt = DateTime.Now;
                    nieuweLijst.kleurHex = hexValue;
                    nieuweLijst.naam = lijstnaam;
                    nieuweLijst.loginId = gebruiker.id;
                    if (DatabaseOperations.ToevoegenLijstje(nieuweLijst) == 1)
                    {
                        lblFormAlert.Content = $"✔ {nieuweLijst.naam} aangemaakt!";
                        lblFormAlert.Foreground = correct;
                        MessageBox.Show("Succesvol boodschappenlijst aangemaakt", "Boodschappenlijst", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
                        btnTerug_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Fout bij boodschappenlijst aanmaken, probeer opnieuw!", "Boodschappenlijst", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
                    }

                }
                else
                {
                    lblFormAlert.Content = "❌ Vul een naam in voor je boodschappenlijst!";
                    lblFormAlert.Foreground = error;
                }
            }
        }
    }
}
