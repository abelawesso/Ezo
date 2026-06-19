using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class Lecteur
    {
        private static readonly HashSet<string> Fonctions = new(StringComparer.OrdinalIgnoreCase)
        {
            "sqrt", "sin", "cos", "tan", "log"
        };

        private static readonly HashSet<char> Operateurs = new() { '+', '-', '*', '/' };
   
        public List<TypeElementInfo> Lire(string expression) 
        {
            var elements = new List<TypeElementInfo>();
            int i = 0;
            expression = expression.Trim();

            while(i< expression.Length)
            {
                char c = expression[i];
                if(char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }
                if(char.IsDigit(c) || c== '.' || (c== '-' && EstMoinsUnaire(elements)))
                {
                    elements.Add(LireNombre(expression, ref i));
                }
                else if(char.IsLetter(c))
                {
                    elements.Add(LireFonction(expression, ref i));
                }
                else if(Operateurs.Contains(c))
                {
                    elements.Add(new TypeElementInfo(TypeElement.Operateur, c.ToString()));
                    i++;
                }
                else
                {
                    throw new ExpressionInvalideException($"Caractère invalide: {c}");
                }
            }

            return elements;
        }

        private static bool EstMoinsUnaire(List<TypeElementInfo> elements)
        {
            if (elements.Count == 0)
                return true;
            var dernierElement = elements.Last();
            return dernierElement.Type == TypeElement.Operateur ||
                   dernierElement.Type == TypeElement.ParentheseOuvrante;
        }

        private static TypeElementInfo LireNombre(string expression, ref int index)
        {
            int debut = index, pointDecimalTrouve = 0;
            if (expression[index] == '-')
            {
                index++;
            }

            while (index < expression.Length && (char.IsDigit(expression[index]) || expression[index] == '.'))
            {
                if (expression[index] == '.' && ++pointDecimalTrouve > 1)
                {                  
                        throw new ExpressionInvalideException("Nombre invalide: plusieurs points décimaux trouvés.");
                }
                index++;
            }
            return new TypeElementInfo(TypeElement.Nombre, expression[debut..index]);
        }
   
        private static TypeElementInfo LireFonction(string expression, ref int index)
        {
            int debut = index;
            while (index < expression.Length && char.IsLetter(expression[index]))
            {
                index++;
            }
            string nomFonction = expression[debut..index];
            if (!Fonctions.Contains(nomFonction))
            {
                throw new ExpressionInvalideException($"Fonction inconnue: {nomFonction}");
            }
            return new TypeElementInfo(TypeElement.Fonction, nomFonction);
        }
    
    }
}
