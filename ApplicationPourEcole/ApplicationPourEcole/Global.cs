using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationPourEcole
{
    public class Global
    {
        private static string jwt;

        public static int IdUtilisateur { get => idUtilisateur; set => idUtilisateur = value; }
        public static string Jwt { get => jwt; set => jwt = value; }

        private static int idUtilisateur;
    }
}
