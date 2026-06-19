using Core;

namespace Core
{

    internal class Evaluateur
    {
        //Dictionnaire des fonctions mathématiques supportées (sqrt, sin, cos, tan, log)
        private static readonly Dictionary<string, Func<decimal, decimal>> Fonctions = new (StringComparer.OrdinalIgnoreCase)
        {
            { "sqrt", x => (decimal)Math.Sqrt((double)x) },
            { "sin",  x => (decimal)Math.Sin((double)x) },
            { "cos", x =>(decimal) Math.Cos((double) x) },
            { "tan", x =>(decimal) Math.Tan((double) x) },
            { "log", x =>(decimal) Math.Log((double) x) }
        };

        //Dictionnaire des opérateurs mathématiques supportés (+,-,*,/)
        private static readonly Dictionary<char, Func<decimal, decimal, decimal>> Operateurs = new()
                    {
            { '+', (a, b) => a + b },
            { '-', (a, b) => a - b },
            { '*', (a, b) => a * b },
            { '/', (a, b) => b == 0 ? throw new DivideByZeroException("Division par zéro non autorisée.") : a / b }
        };


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
