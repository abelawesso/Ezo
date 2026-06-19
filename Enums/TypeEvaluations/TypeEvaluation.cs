using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal enum TypeEvaluation
    {
        [Description("Évaluation native")]
        Native,
        [Description("Évaluation intégrée")]
        Integre
    }
}
