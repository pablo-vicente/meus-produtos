using System.Linq;

namespace DevIO.Business.Core.Validations.Documentos
{
    public class Utils
    {
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = valor.Where(s => char.IsDigit(s)).Aggregate("", (current, s) => current + s);
            return onlyNumber.Trim();
        }
    }
}