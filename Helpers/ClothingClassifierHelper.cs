using System;

namespace Wardrobe.Helpers
{
    public static class ClothingClassifierHelper
    {
        public static string ClassifyType(string description)
        {
            if (string.IsNullOrEmpty(description))
                return "Bilinmiyor";

            description = description.ToLower();

            if (description.Contains("tişört") || description.Contains("gömlek") || description.Contains("kazak"))
                return "Üst";

            if (description.Contains("pantolon") || description.Contains("etek") || description.Contains("şort"))
                return "Alt";

            if (description.Contains("ayakkabı") || description.Contains("bot") || description.Contains("sneaker"))
                return "Ayakkabı";

            return "Diğer";
        }
    }
}
