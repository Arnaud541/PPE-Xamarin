using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationPourEcole
{
    public class Livre
    {
        private int id;
        private string titre;
        private int id_utilisateur;
        private DateTime date_livre;
        private string img;

        public string Titre { get => titre; set => titre = value; }
        public int Id { get => id; set => id = value; }
        public int IdUtilisateur { get => id_utilisateur; set => id_utilisateur = value; }
        public DateTime Date_livre { get => date_livre; set => date_livre = value; }
        public string Img { get => img; set => img = value; }

        public Livre(string titre, int id, DateTime dateLivre)
        {
            this.Id = id;
            this.Titre = titre;
            this.date_livre = dateLivre;

        }

    }
}
