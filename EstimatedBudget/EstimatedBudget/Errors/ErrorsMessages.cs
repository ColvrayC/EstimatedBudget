using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.Errors
{
    public static class ErrorsMessages
    {
        //String
        public const string Length = "Votre entrée est trop volumineuse.";

        //Int
        public const string Int = "Doit contenire une valeur numérique ou une valeur positive.";

        //Decimal
        public const string Decimale = "Doit contenire une valeur numérique ou une valeur dans l'intervale.";
        public const string NumericNegative = "Les valeurs négative ne sont pas autorisées.";
        public const string Euro = "La valeure doit contenir une valeure monnétaire.";

        //Requierd
        public const string Required = "Ce champs est requis.";

        //Already Exist
        public const string IsExist = "Cette valeur existe déjà.";
    }
}
