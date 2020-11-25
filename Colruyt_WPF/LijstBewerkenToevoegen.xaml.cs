﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Colruyt_DAL;
using System.Windows.Input;

namespace Colruyt_WPF
{
    /// <summary>
    /// Interaction logic for LijstBewerkenToevoegen.xaml
    /// </summary>
    public partial class LijstBewerkenToevoegen : Window
    {
        private Image _theImage;
        private string hexValue = "#FFFFFF";

        private bool bewerken = false;

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
        Lijst lijst = null;
        Helper helperClass = new Helper();

        public LijstBewerkenToevoegen()
        {
            init();
        }

        public LijstBewerkenToevoegen(Login gebruiker)
        {
            init();

            this.gebruiker = gebruiker;
        }

        public LijstBewerkenToevoegen(Login gebruiker, Lijst lijst)
        {
            init();

            this.gebruiker = gebruiker;
            this.lijst = lijst;

            bewerken = true;

            txtLijstnaam.Text = lijst.naam;
            hexValue = lijst.kleurHex;
            SelectedColor.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(hexValue));
        }

        public void init()
        {
            InitializeComponent();

            txtLijstnaam.Focus();

            CreateColorPicker();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (bewerken)
            {
                btnLijstAanmaken.Visibility = Visibility.Collapsed;
                btnLijstBewerken.Visibility = Visibility.Visible;
            }
            else
            {
                btnLijstAanmaken.Visibility = Visibility.Visible;
                btnLijstBewerken.Visibility = Visibility.Collapsed;
            }
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

        private void btnLijstBewerken_Click(object sender, RoutedEventArgs e)
        {
            string lijstnaam = txtLijstnaam.Text;

            if (gebruiker != null)
            {
                if (!string.IsNullOrWhiteSpace(lijstnaam) && lijstnaam.Length >= 3)
                {
                    try
                    {
                        Lijst updateLijst = DatabaseOperations.OphalenLijst(lijst.id);
                        updateLijst.kleurHex = hexValue;
                        updateLijst.naam = lijstnaam;
                        if (DatabaseOperations.AanpassenLijstje(updateLijst) == 1)
                        {
                            lblFormAlert.Content = $"✔ {updateLijst.naam} geupdate!";
                            lblFormAlert.Foreground = correct;
                            MessageBox.Show("Succesvol boodschappenlijst geupdate", "Boodschappenlijst", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
                            btnTerug_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Fout bij boodschappenlijst updaten, probeer opnieuw!", "Boodschappenlijst", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }

                }
                else
                {
                    lblFormAlert.Content = "❌ Vul een naam in voor je boodschappenlijst!";
                    lblFormAlert.Foreground = error;
                }
            }
        }

        private bool hitEnter(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                return true;
            return false;
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (hitEnter(e))
            {
                if (bewerken)
                {
                    btnLijstBewerken_Click(sender, e);
                }
                else
                {
                    btnLijstAanmaken_Click(sender, e);
                }
            }
        }
    }
}
