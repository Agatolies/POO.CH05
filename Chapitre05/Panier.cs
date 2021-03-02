using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Chapitre05
{
    public class Panier
    {
        //- Variables d'instance statiques
        private static long numeroProchainPanier;

        //- Constructeur statique
        static Panier()
        {
            numeroProchainPanier = 5;
        }

        //- Méthode statique
        public static string GetNumero()
        {
            string numeroProchainPanierChaine = numeroProchainPanier.ToString("D8");

            numeroProchainPanier += 5;

            return numeroProchainPanierChaine;
        }

        //- Variables d'instances
        private string numero;
        private DateTime dateCreation;
        private ArrayList lignes;

        //- Constructeur(s)
        public Panier()
        {
            this.dateCreation = DateTime.Now;
            this.numero = Panier.GetNumero();
            this.lignes = new ArrayList();
        }


        //- Indexeur
        public LignePanier this[string codeEAN] //Ici LignePanier est le type de retour. L'indexeur n'a pas de nom
        {
            get
            {
                LignePanier retVal = null;

                foreach (LignePanier lp in this)
                    if (lp.CodeEAN13 == codeEAN)
                    {
                        retVal = lp;
                        break;
                    }

                return retVal;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.lignes.GetEnumerator();
        }

        //- Propriétés
        public int Poids
        {
            get
            {
                int poids = 0;

                foreach (LignePanier lp in this) //- "this" parce qu'on a un énumérateur
                {
                    poids += lp.Poids * lp.Quantite;
                }

                return poids;
            }
        }

        public decimal MontantTVA 
        { 
            get
            {
                return Math.Round(this.PrixHT * 0.21m, 2);
            }
        }
        public decimal PrixHT 
        {
            get
            {
                decimal prixHT = 0;

                foreach (LignePanier lp in this)
                {
                    prixHT += lp.PrixHT * lp.Quantite;
                }

                return Math.Round(prixHT, 2);

                //return lignes.Sum(lp => lp.PrixHt);      Voir chapitre 8 - partie Linq
            }
        }
        public decimal PrixTTC 
        { 
            get
            {
                decimal prixTTC = PrixHT + MontantTVA;

                return Math.Round(prixTTC, 2);
            }
        }

        public string EnChaine
        {
            get
            {
                StringBuilder builder = new StringBuilder()
                    .AppendLine($"Panier : {this.numero}")
                    .AppendLine($"Date   : {this.dateCreation.ToShortDateString()}")
                    .Append('*', 18)
                    .AppendLine()
                    .AppendLine();

                bool estVide = lignes.Count == 0;

                builder = estVide                     // fucking ternaire
                    ? builder.AppendLine("Vide.")
                    : builder.AppendJoin('\n', ObtenirLignesEnChaine())
                        .AppendLine()
                        .AppendLine()
                        .AppendLine($"Prix total HT  : {PrixHT} Euros")
                        .AppendLine($"TVA            : {MontantTVA} Euros")
                        .AppendLine($"Prix total TTC : {PrixTTC} Euros")
                        .AppendLine($"Poids total    : {(decimal)Poids / 1000} Kgs.");

                return builder.ToString();// La classe builder n'affichera rien si on n'ajoute cette dernière méthode car le get attend un string
            }
        }


        //- Méthodes
        public bool AjouterArticle(string codeEAN, string description, decimal prixArticle, int poidsArticle)
        {
            try
            {
                LignePanier lignePanier = this[codeEAN];

                bool existeDeja = lignePanier != null;
                if (existeDeja)
                {
                    lignePanier.Quantite++;
                }
                else
                {
                    lignePanier = new LignePanier(codeEAN, description, poidsArticle, prixArticle);

                    this.lignes.Add(lignePanier);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AjouterArticle(string codeEAN13)
        {
            LignePanier lignePanier = this[codeEAN13];

            bool existe = lignePanier != null;   //si panier vide, vaudra false, sinon, true
            if (existe)
                lignePanier.Quantite++;

            return existe;
            
        }

        public bool SupprimerArticle(string codeEAN13)
        {

            LignePanier lignePanier = this[codeEAN13];

            bool existe = lignePanier != null;

            if (existe)
            {
                bool estDernierArticle = lignePanier.Quantite == 1; 
                if (estDernierArticle)
                {
                    this.lignes.Remove(lignePanier);
                }
                else
                {
                    lignePanier.Quantite--;
                }
            }

            return existe;
        }

        private List<string> ObtenirLignesEnChaine()
        {
            List<string> lignesEnChaine = new List<string>();

            foreach (LignePanier lp in this)
            {
                lignesEnChaine.Add(lp.EnChaine);
            }

            return lignesEnChaine;
        }
    }
}
