using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicationPourEcole
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouterNotesLivres : ContentPage
    {
        public ObservableCollection<Livre> lesLivres = new ObservableCollection<Livre>();
        public static Livre LeLivreEnCours;
  

        public AjouterNotesLivres()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            
            LivreView.ItemsSource = lesLivres;
            LivreView.ItemSelected += LivreView_ItemSelected;
            LeLivreEnCours = null;

        }


        public void AffecterLivre(List<Livre> liste)
        {
            foreach (Livre l in liste)
            {
                lesLivres.Add(l);
            }
        }

        private void LivreView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            AjouterNotesLivres.LeLivreEnCours = (Livre)e.SelectedItem;
            RecupererLivre.maPageDeLivres = this;
            RecupererLivre p2 = new RecupererLivre();
            Navigation.PushAsync(p2);

        }

        public void AccessToEvaluations(object sender, EventArgs e)
        {
            EvaluationsUtilisateur eu = new EvaluationsUtilisateur();
            Navigation.PushAsync(eu);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void deconnexion_Clicked(object sender, EventArgs e)
        {
            Global.Jwt = null;
            Navigation.PopToRootAsync();
        }
    }
}