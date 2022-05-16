using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationPourEcole
{
    public class Evaluation
    {
        private string img;
        private int note;
        private string commentaire;
        private DateTime date;

        public int Note { get => note; set => note = value; }
        public string Commentaire { get => commentaire; set => commentaire = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Img { get => img; set => img = value; }

        public Evaluation()
        {

        }
    }
}
