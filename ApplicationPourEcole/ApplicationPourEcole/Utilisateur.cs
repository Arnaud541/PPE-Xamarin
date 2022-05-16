using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationPourEcole
{
    public class Utilisateur
    {
        private int id;
        private string login;
        private string mdp;
     

        public int Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Mdp { get => mdp; set => mdp = value; }
        
        public Utilisateur() { }
        public Utilisateur(string login, string mdp)
        {
            this.Login = login;
            this.Mdp = mdp;
        }
     
    }
}
