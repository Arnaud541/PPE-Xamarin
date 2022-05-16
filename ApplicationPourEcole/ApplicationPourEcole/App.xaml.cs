using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poppins-Regular.ttf", Alias = "Poppins")]
[assembly: ExportFont("Poppins-Bold.ttf", Alias = "Poppins-Bold")]
namespace ApplicationPourEcole
{
    public partial class App : Application
    {
        AjouterNotesLivres anl = new AjouterNotesLivres();
        MainPage mp = new MainPage();
        public App()
        {
            
            InitializeComponent();
            mp.an = anl;
            MainPage = new NavigationPage(mp)
            {
                BarBackgroundColor = Color.FromHex("#B6FFCE")
            };

        }

        protected override async void OnStart()
        {
            var httpClient = new System.Net.Http.HttpClient();
            var url = "http://192.168.56.1:80/projet_xamarin/index.php?action=livre";
            var uri = new Uri(url);
            var response = await httpClient.GetAsync(uri);
            List<Livre> items = new List<Livre>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                items = JsonConvert.DeserializeObject<List<Livre>>(content);
                Debug.WriteLine(items);
            }

            anl.AffecterLivre(items);

            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        
    }
}
