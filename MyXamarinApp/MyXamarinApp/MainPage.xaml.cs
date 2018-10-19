using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyXamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            var text = TextBox1.Text;
            var locationTask = Geolocation.GetLastKnownLocationAsync();
            Button1.Text = String.Format("Hello {0}", text);
            //https://cn.bing.com/search?q=hello
            var sourceStr = String.Format("https://cn.bing.com/search?q={0}", text);
            Wv.Source = sourceStr;
            locationTask.ContinueWith((a) =>
            {
                Location location = a.Result;
                Button1.Text += String.Format("[{0},{1}]", location.Longitude, location.Latitude);
            });

            WebClient webClient = new WebClient();


            var alertTask = DisplayAlert("Alert", "Hello", "OK", "NO");

            alertTask.ContinueWith((b) =>
            {
                DisplayAlert("Alert again", String.Format("You clicked {0}!", (b.Result == true) ? "OK" : "NO"), "ok", "no");

                OutputLabel.Text += String.Format("You clicked {0}!", (b.Result == true) ? "OK" : "NO");

            });


            var downTask = webClient.DownloadDataTaskAsync(new Uri(sourceStr));
            downTask.ContinueWith((a) =>
            {
                OutputLabel.Text += String.Format("{0}", a.Result.Length);
            });

            string filename = "file:///android_asset/someText.txt";
            if (File.Exists(filename))
            {
                var bytes = File.ReadAllBytes(filename);
                var str = System.Text.Encoding.Default.GetString(bytes);
                OutputLabel.Text += str;
            }
            else
            {

            }





        }
    }
}
