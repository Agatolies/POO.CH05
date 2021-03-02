using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chapitre05
{
    static class EAN13
    {
        public static bool Valider(string code)
        {
            bool a13Chiffres = Regex.IsMatch(code, "^[0-9]{13}$");
            if (!a13Chiffres)
                return false;

            string les12PremiersChiffres = code.Substring(0, 12);
            string le13EmeChiffre = code.Substring(12);
            string checkDigit = CalculerCD(les12PremiersChiffres);

            bool estValide = le13EmeChiffre == checkDigit;
            return estValide;
        }

        public static string CalculerCD(string code)
        {
            string checkDigit = string.Empty;   // est identique à ""
            bool estValide = Regex.IsMatch(code, "^[0-9]{12}$");
            if (!estValide)
                throw new Exception("Ce n'est pas valide");

            int sommeChiffresPositionsPairs = 0;
            int sommeChiffresPositionsImpairs = 0;

            for (int i = 0; i < code.Length; i++)
            {
                string chiffreEnChaine = code.Substring(i, 1);

                int chiffre = Convert.ToInt32(chiffreEnChaine);

                bool estIndexPairPositionImpair = i % 2 == 0;
                if (estIndexPairPositionImpair)
                {
                    sommeChiffresPositionsImpairs += chiffre;
                }
                else
                {
                    sommeChiffresPositionsPairs += chiffre;
                }
            }

            int iPlus3PModulo10 = (sommeChiffresPositionsImpairs + (3 * sommeChiffresPositionsPairs)) % 10;

            if (iPlus3PModulo10 == 0)
            {
                checkDigit = "0";
            }
            else
            {
                checkDigit = (10 - iPlus3PModulo10).ToString();
            }

            return checkDigit;
        }
    }
}
