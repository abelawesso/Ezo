using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core
{
    internal class Ordonnanceur
    {
        private static readonly Dictionary<string, (int Priorite, bool Gauche)> Ordre = new(StringComparer.OrdinalIgnoreCase)
        {
            { "sqrt", (3, false) },
            { "sin", (3, false) },
            { "cos", (3, false) },
            { "tan", (3, false) },
            { "log", (3, false) },
            { "*", (2, true) },
            { "/", (2, true) },
            { "+", (1, true) },
            { "-", (1, true) }
        };

        public Queue<TypeElementInfo> Ordonner(List<TypeElementInfo> elements)
        {
            var sortie = new Queue<TypeElementInfo>();
            var pile = new Stack<TypeElementInfo>();

            foreach (var element in elements)
            {
                switch (element.Type)
                {
                    case TypeElement.Nombre:
                        sortie.Enqueue(element);
                        break;
                    case TypeElement.Fonction:
                    case TypeElement.Operateur:                       
                        EmpilerOperateur(element, sortie, pile);
                        break;
                    case TypeElement.ParentheseOuvrante:
                        pile.Push(element);
                        break;
                    case TypeElement.ParentheseFermante:
                        TraiterParentheseFermante(sortie, pile);
                        break;
                }
            }
            while (pile.Count > 0)
            {
                if (pile.Peek().Type == TypeElement.ParentheseOuvrante || pile.Peek().Type == TypeElement.ParentheseFermante)
                    throw new ExpressionInvalideException("Parenthèses non équilibrées");
                sortie.Enqueue(pile.Pop());
            }
            return sortie;
        }
    
        private static void EmpilerOperateur(TypeElementInfo elementInfo, Queue<TypeElementInfo> sortie, Stack<TypeElementInfo> pile)
        {
            var (priorite, gauche) = Ordre[elementInfo.Valeur];

            while (pile.Count > 0 &&
                              (pile.Peek().Type == TypeElement.Fonction || pile.Peek().Type == TypeElement.Operateur) &&
                              ((Ordre[pile.Peek().Valeur].Priorite > Ordre[elementInfo.Valeur].Priorite) ||
                               (Ordre[pile.Peek().Valeur].Priorite == Ordre[elementInfo.Valeur].Priorite && Ordre[elementInfo.Valeur].Gauche)))
            {
                sortie.Enqueue(pile.Pop());
            }
            pile.Push(elementInfo);
        }

        private static void TraiterParentheseFermante(Queue<TypeElementInfo> sortie, Stack<TypeElementInfo> pile)
        {
            while (pile.Count > 0 && pile.Peek().Type != TypeElement.ParentheseOuvrante)
            {
                sortie.Enqueue(pile.Pop());
            }
            if (pile.Count == 0 || pile.Peek().Type != TypeElement.ParentheseOuvrante)
                throw new ExpressionInvalideException("Parenthèses non équilibrées");
            pile.Pop(); // Retirer la parenthèse ouvrante
        }
    }
}
