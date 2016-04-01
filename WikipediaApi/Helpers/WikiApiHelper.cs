using System.Globalization;

namespace WikipediaApi.Helpers {

    public static class WikiApiHelper {
        public static string PrepareWordForSearching(string wordToClear) {
            var result = "";

            result = wordToClear.Replace(' ', '_');
            result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());

            return result;
        }
    }

}