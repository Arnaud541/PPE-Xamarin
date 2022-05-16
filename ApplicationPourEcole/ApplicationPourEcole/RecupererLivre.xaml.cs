using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicationPourEcole
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecupererLivre : ContentPage
    {
        public static AjouterNotesLivres maPageDeLivres;
        public static int noteLivreEnCours;
        public static int idLivreEnCours;
        
        public RecupererLivre()
        {
            InitializeComponent();
            this.Titre.Text = AjouterNotesLivres.LeLivreEnCours.Titre;
            this.ImageLivre.Source = AjouterNotesLivres.LeLivreEnCours.Img;
                  
        }

        

        public void ajouter_Clicked(object sender, EventArgs e)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (sender2, cert, chain, sslPolicyErrors) => true;
            var authHeader = new AuthenticationHeaderValue("Bearer", "");
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = authHeader;
            JObject oJsonObject = new JObject();
            noteLivreEnCours = Convert.ToInt32(picker.Items.ElementAt(picker.SelectedIndex));
            idLivreEnCours = AjouterNotesLivres.LeLivreEnCours.Id;
            oJsonObject.Add("commentaire", Convert.ToString(Commentaire.Text));
            oJsonObject.Add("note", noteLivreEnCours);
            oJsonObject.Add("idLivre", idLivreEnCours);
            oJsonObject.Add("idUtilisateur", Global.IdUtilisateur);
            string tmp = oJsonObject.ToString();
            Debug.WriteLine(oJsonObject);
            var oTaskPostAsync = client.PostAsync("http://192.168.56.1:80/projet_xamarin/index.php?action=evaluation", new StringContent(oJsonObject.ToString(), Encoding.UTF8)).Result;
            string content = oTaskPostAsync.Content.ReadAsStringAsync().Result;

            if(content == "true")
            {
                EvaluationsUtilisateur eu = new EvaluationsUtilisateur();
                Navigation.PushAsync(eu);

            }
            
            
        }
    }


}