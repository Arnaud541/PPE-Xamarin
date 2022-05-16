using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicationPourEcole
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluationsUtilisateur : ContentPage
    {
        public ObservableCollection<Evaluation> lesEvaluations = new ObservableCollection<Evaluation>();
        public EvaluationsUtilisateur()
        {
            
            InitializeComponent();
            recupererEvaluations();
            EvaluationView.ItemsSource = lesEvaluations;

            Debug.WriteLine(lesEvaluations);
            
        }

        public void AffecterEvaluations(List<Evaluation> lesEvaluations)
        {
            foreach (Evaluation e in lesEvaluations)
            {
                
                this.lesEvaluations.Add(e);
            }
        }

        public async void recupererEvaluations()
        {
            var httpClient = new HttpClient();
            int idUser = Global.IdUtilisateur;
            var url = "http://192.168.56.1:80/projet_xamarin/index.php?action=evaluation&idUtilisateur="+idUser+"";
            var uri = new Uri(url);
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var test = JsonConvert.DeserializeObject<List<Evaluation>>(content);
                this.AffecterEvaluations(test);
            }
         
        }

        private void deconnexion_Clicked(object sender, EventArgs e)
        {
            Global.Jwt = null;
            Navigation.PopToRootAsync();
        }
    }
}