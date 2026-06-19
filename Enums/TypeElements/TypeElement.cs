using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal enum TypeElement
    {
        Nombre,
        Operateur,
        Fonction,
        ParentheseOuvrante,
        ParentheseFermante
    }

    internal record TypeElementInfo(TypeElement Type, string Valeur);
}
