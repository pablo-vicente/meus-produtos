using System.Globalization;

namespace DevIO.AppMvc
{
    public class CultureConfig
    {
        public static void RegistreCulture()
        {
            var culture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}