[assembly: Xamarin.Forms.Dependency(typeof(Ventas.iOS.Implementations.Localize))]
namespace Ventas.iOS.Implementations
{
   
    using System.Globalization;
    using System.Threading;
    using Foundation;
    using Helpers;
    using Interfaces;
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }
            //this gets called a lot -try/catch can be expensive so consider changing or sometime
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallBackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new System.Globalization.CultureInfo("en");
                }
            }
            return ci;
        }
        public void SetLocale (CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        string iOSToDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            switch (iOSLanguage)
            {
                case "ms-MY":
                case "ms-MG":
                    netLanguage = "ms";
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;

            }
            return netLanguage;
        }
        string ToDotnetFallBackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier
            switch (platCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; // fallback to portuguese(portugal)
                    break;
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to german (Switzerland) for this app
                    break;
                    //add more application-specific case here (if requiered)
                    //ONLY use cultures that have been tested and  know to work
            }
            return netLanguage;

        }

    }
}