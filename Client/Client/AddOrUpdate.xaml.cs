using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace Client
{
    public partial class AddOrUpdate : Window
    {
        public string name { get; set; }
        public string map { get; set; }
        public BitmapImage image { get; set; }

        Card card;
        public AddOrUpdate(Card card)
        {
            this.card = card;
            InitializeComponent();
            textBox.Text = card.name;
            if (card.image != null)
            {
                image = card.image;
                img.Source = image;

            }

            else
            {
                image = new BitmapImage(
                   new Uri("/Images/no_image.jpg", UriKind.Relative))
                {
                    CreateOptions = BitmapCreateOptions.IgnoreImageCache
                };
                img.Source = image;
            }
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.DecodePixelHeight = 200;
                    bitmap.EndInit();
                    image = bitmap;
                    img.Source = bitmap;
                }
                catch
                {
                    image = new BitmapImage(
                       new Uri("/Images/no_image.jpg", UriKind.Relative))
                    {
                        CreateOptions = BitmapCreateOptions.IgnoreImageCache
                    };
                    img.Source = image;
                    Console.WriteLine("Encoding error.");
                }
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "" && textBox.Text != null)
            {
                textBox.BorderBrush = Brushes.Green;
                name = textBox.Text;
                try
                {
                    MemoryStream ms = new MemoryStream();
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(ms);
                    byte[] bitmapdata = ms.ToArray();
                    map = Convert.ToBase64String(bitmapdata);
                }
                catch
                {
                    MessageBox.Show("Encoding error.");
                }
                
                DialogResult = true;
            }
            else
                textBox.BorderBrush = Brushes.Red;
        }

        private void cancelButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
