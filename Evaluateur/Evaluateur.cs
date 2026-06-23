using Core;

namespace Core
{
    /// <summary>
    /// Classe responsable de l'évaluation des expressions mathématiques. Elle prend en charge deux types d'évaluation:
    /// Intégré (qui utilise les fonctions et opérateurs définis dans les dictionnaires) et Native (qui utilise la classe DataTable.Compute).
    /// </summary>
    public class Evaluateur
    {
        //Dictionnaire des fonctions mathématiques supportées (sqrt, sin, cos, tan, log)
        private static readonly Dictionary<string, Func<decimal, decimal>> Fonctions = new(StringComparer.OrdinalIgnoreCase)
        {
            { "sqrt", x => (decimal)Math.Sqrt((double)x) },
            { "sin",  x => (decimal)Math.Sin((double)x) },
            { "cos", x =>(decimal) Math.Cos((double) x) },
            { "tan", x =>(decimal) Math.Tan((double) x) },
            { "log", x =>(decimal) Math.Log((double) x) },

        };

        //Dictionnaire des opérateurs mathématiques supportés (+,-,*,/)
        private static readonly Dictionary<char, Func<decimal, decimal, decimal>> Operateurs = new()
                    {
            { '+', (a, b) => a + b },
            { '-', (a, b) => a - b },
            { '*', (a, b) => a * b },
            { '/', (a, b) => b == 0 ? throw new DivisionParZeroException("Division par zéro non autorisée.") : a / b },
            { '^', (a, b) => (decimal)Math.Pow((double)a, (double)b) }
        };

        public static decimal EvaluationIntegre(string expression)
        {
            var elements = new Lecteur().Lire(expression);
            var ordonnance = new Ordonnanceur().Ordonner(elements);

            var pile = new Stack<decimal>();
            foreach (var element in ordonnance)
            {
                switch(element.Type)
                {
                    case TypeElement.Nombre:
                        pile.Push(decimal.Parse(element.Valeur, System.Globalization.CultureInfo.InvariantCulture));
                        break;
                    case TypeElement.Operateur:
                       if(pile.Count<2)
                            throw new ExpressionInvalideException("Expression invalide: opérateur sans suffisamment d'opérandes.");
                        decimal droite = pile.Pop(), gauche = pile.Pop();
                        var resultat = Operateurs[element.Valeur[0]](gauche, droite);
                        pile.Push(resultat);
                        break;
                    case TypeElement.Fonction:
                        if(pile.Count<1)
                            throw new ExpressionInvalideException("Expression invalide: fonction sans suffisamment d'opérandes.");

                        pile.Push(Fonctions[element.Valeur](pile.Pop()));               
                        break;
                    case TypeElement.ParentheseOuvrante:
                        pile.Push(0);
                        break;
                    case TypeElement.ParentheseFermante:
                        pile.Push(1);
                        break;
                }
            }
            if(pile.Count != 1)
                throw new ExpressionInvalideException("Expression invalide: résultat final incorrect.");
            return pile.Pop();
        }

        public static decimal Evaluer(string expression, TypeEvaluation typeEvaluation)
        {
            try
            {
                switch (typeEvaluation)
                {
                    case TypeEvaluation.Native:
                        //Évaluation de l'expression en utilisant la classe DataTable.Compute
                        var result = new System.Data.DataTable().Compute(expression, null);
                        return Convert.ToDecimal(result);

                    case TypeEvaluation.Integre:
                        //Evaluation en se basant sur les fonctions et opérateurs définis dans les dictionnaires
                        return EvaluationIntegre(expression);

                    default:
                        throw new ExpressionInvalideException("Type d'évaluation non supporté");
                }
            }
            catch (Exception ex)
            {
                throw new ExpressionInvalideException($"Erreur lors de l'évaluation de l'expression: {ex.Message}", ex);
            }
        }
    
    }
}
