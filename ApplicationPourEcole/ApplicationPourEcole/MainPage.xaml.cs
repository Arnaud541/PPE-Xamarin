using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApplicationPourEcole
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Utilisateur> lesutilisateur = new ObservableCollection<Utilisateur>();
        public AjouterNotesLivres an;
        

        public MainPage()
        {
            InitializeComponent();
            this.confirmLabel.Text = null;
            // COMMANDE POUR REVENIR A LA PAGE DE CONNEXION : Navigation.PopToRootAsync();
            // POUR LE BOUTON DE DECONNEXION

        }

        protected Dictionary<string, string> GetTokenInfo(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            return TokenInfo;
        }


        public void Connexion(object sender, EventArgs e)
        {

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (sender2, cert, chain, sslPolicyErrors) => true;
            var authHeader = new AuthenticationHeaderValue("Bearer", "");
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = authHeader;

            JObject oJsonObject = new JObject();
            oJsonObject.Add("login", Login.Text);
            oJsonObject.Add("mdp", Mdp.Text);

            string tmp = oJsonObject.ToString();

            var oTaskPostAsync = client.PostAsync("http://192.168.56.1:80/projet_xamarin/index.php?action=utilisateur", new StringContent(oJsonObject.ToString(), Encoding.UTF8)).Result;
            string content = oTaskPostAsync.Content.ReadAsStringAsync().Result;

            
            if(content != "false")
            {
                Dictionary<string, string> resuToken = this.GetTokenInfo(content);
                string partieUser = resuToken.ElementAt(5).Value;
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(partieUser);

                char[] separators = new char[] { ' ', ':' };
                string[] test = values.ElementAt(0).Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                Global.IdUtilisateur = Convert.ToInt32(test[1]);
                Global.Jwt = content;
                Navigation.PushAsync(an);
            }
            else
            {
                if (content == "false")
                {
                    
                    confirmLabel.Text = "Vos identifiants sont incorrects !";
                }
            }
        }

        

    }
}
