using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Extensions;

namespace WenElevating.Todo.Commons.Helpers
{
    public class LocalizationHelper
    {
        public static string? GetLocalizationString(string key, string resourceFileName = "Resources")
        {
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var fullKey = assemblyName + ":" + resourceFileName + ":" + key;
            var locExtension = new LocExtension(fullKey);
            locExtension.ResolveLocalizedValue(out string? localizedString);
            return localizedString;
        }

        public static void SetLocalizationLanguage(string language)
        {
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = CultureInfo.CreateSpecificCulture(language);
        }
    }
}
