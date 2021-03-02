using System;
using System.Collections.Generic;
using System.Text;

namespace Chapitre05
{
    public class LignePanier
    {
        private string codeEAN13;
        private string description;
        private int poids;
        private decimal prixHT;
        private int quantite;

        public LignePanier(string codeEAN13, string description, int poids, decimal prixHT)
        {
            CodeEAN13 = codeEAN13;
            Poids = poids;
            PrixHT = prixHT;

            this.description = description;
            this.quantite = 1;
        }

        public string CodeEAN13
        {
            get
            {
                return codeEAN13;
            }

            private set
            {
                if (!EAN13.Valider(value))
                    throw new Exception("Code invalide");

                codeEAN13 = value;
            }
        }

        public string Description 
        { 
            get { return description; } 
        }

        public string EnChaine 
        { 
            get
            {
                decimal prixTotalHT = Quantite * PrixHT;
                return $"{CodeEAN13} : \"{Description}\" X {Quantite}, {prixTotalHT} Euros.";
                //return CodeEAN13 + " : \"" + Description + "\" X " + Quantite + ", " + prixTotalHT + " Euros.";
                //return string.Format("{0} : \"{1}\" X {2}, {3} Euros.", CodeEAN13, Description, Quantite, prixTotalHT);
            }
        }

        public int Poids
        {
            get { return poids; }
            private set
            {
                if (value <= 0)
                    throw new Exception("Poids invalide");

                poids = value;
            }
        }

        public decimal PrixHT
        {
            get
            {
                return Math.Round(prixHT, 2);
            }

            private set
            {
                if (value <= 0)
                    throw new Exception("Prix invalide");

                prixHT = value;
            }
        }

        //public decimal GetPrixHT()
        //{
        //    return prixHT;
        //}

        //private void SetPrixHT(decimal value)
        //{
        //    if(value <= 0)
        //            throw new Exception("Prix invalide");

        //    prixHT = value;
        //}

        public int Quantite
        {
            get { return quantite; }
            set
            {
                if (value <= 0)
                    throw new Exception("Quantité invalide");

                quantite = value;
            }
        }
    }
}
