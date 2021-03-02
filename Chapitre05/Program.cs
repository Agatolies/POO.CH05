using System;

namespace Chapitre05
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine(Panier.GetNumero());

            var panier = new Panier();

            panier.AjouterArticle("9782810617500", "Graphisme 2D en C#", 39.90M, 530);
            panier.AjouterArticle("9782810617500");
            panier.AjouterArticle("9782810617500");
            panier.AjouterArticle("9782035840882", "Le petit larousse", 29.5m, 980);
            foreach (LignePanier item in panier)
            {
                Console.WriteLine(item.EnChaine);
            }
            panier.SupprimerArticle("9782810617500");
            panier.SupprimerArticle("9782035840882");
            panier.SupprimerArticle("978235840882");
            
            foreach (LignePanier item in panier)
            {
                Console.WriteLine(item.EnChaine);
            }

            Console.WriteLine(panier.EnChaine);

            Console.ReadLine();
        }
    }
}
