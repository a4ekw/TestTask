using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Client
{
    public class Card
    {
        public string id { get; set; }
        public string name { get; set; }
        public string base64 { get; set; }
        public string map
        {
            get
            {
                return base64;
            }
            set
            {
                byte[] byteBuffer = Convert.FromBase64String(value);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(byteBuffer);
                bitmapImage.EndInit();
                image = bitmapImage;
                base64 = value;
            }
        }
        public BitmapImage image { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            load_List();
        }

        private void load_List()
        {

            string json = "";
            string address = "http://localhost:55067/cards";
            List<Card> list = new List<Card>();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(address);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader stream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                {
                    json = stream.ReadToEnd();
                }
                try
                {
                    var serializer = new JavaScriptSerializer();
                    list = serializer.Deserialize<List<Card>>(json);
                }
                catch
                {
                    label.Visibility = Visibility.Visible;
                    label.Content = "Deserialize error.";
                }
            }
            catch
            {
                label.Visibility = Visibility.Visible;
                label.Content = "HTTP error.";
            }

            listView.ItemsSource = list;
        }
        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listView.SelectedItems.Count > 1)
                updateButton.IsEnabled = false;
            else
                updateButton.IsEnabled = true;
            if (listView.SelectedItems.Count == 0)
                deleteButton.IsEnabled = false;
            else
                deleteButton.IsEnabled = true;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Card card = new Card();
            AddOrUpdate add = new AddOrUpdate(card);
            add.Owner = this;
            if (add.ShowDialog() == true)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        Card sCard = new Card();
                        sCard.id = Guid.NewGuid().ToString();
                        sCard.name = add.name;
                        sCard.map = add.map;
                        sCard.image = null;

                        var response = client.PostAsync("http://localhost:55067/cards",
                            new StringContent(
                                new JavaScriptSerializer().Serialize(sCard), Encoding.UTF8, "application/json")).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            Console.Write("Update: Success.");
                            load_List();
                        }
                        else
                            Console.Write("Server: Error.");
                    }
                    catch
                    {
                        Console.Write("Update: Error");
                    }
                }
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Card card = listView.SelectedItem as Card;
            AddOrUpdate update = new AddOrUpdate(card);
            update.Owner = this;
            if (update.ShowDialog() == true)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        Card sCard = new Card();
                        sCard.id = card.id;
                        sCard.name = update.name;
                        sCard.map = update.map;
                        sCard.image = null;

                        var response = client.PutAsync("http://localhost:55067/cards",
                            new StringContent(
                                new JavaScriptSerializer().Serialize(sCard), Encoding.UTF8, "application/json")).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            Console.Write("Update: Success.");
                            load_List();
                        }
                        else
                            Console.Write("Server: Error.");
                    }
                    catch
                    {
                        Console.Write("Update: Error");
                    }
                }
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
                int i = 1;
            foreach (Card card in listView.SelectedItems)
            {
                using (var client = new HttpClient())
                {
                    var response = client.DeleteAsync("http://localhost:55067/cards/" + card.id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Write("Delete: Success");
                    }
                    else
                    {
                        Console.Write("Delete: Error.");
                        Console.Write("Error at " + i + " position.");
                    }
                    if (i == listView.SelectedItems.Count)
                    {
                        load_List();
                        break;
                    }
                    i++;
                }
            }
        }
    }
}
