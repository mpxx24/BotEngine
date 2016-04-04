using System;
using System.Collections.Generic;
using System.Globalization;

namespace WikipediaApi.Helpers {

    public static class WikiApiHelper {
        /// <summary>
        ///     Prepares the word for searching.
        /// </summary>
        /// <param name="wordToClear">The word to clear.</param>
        /// <returns>properly formated word</returns>
        public static List<string> PrepareWordForSearching(string wordToClear) {
            var possibleCombinations = new List<string>
            {
                wordToClear,
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(wordToClear.ToLower()),
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(wordToClear.Replace(' ', '_').ToLower()),
                CultureInfo.CurrentCulture.TextInfo.ToLower(wordToClear)
            };

            return possibleCombinations;
        }

        /// <summary>
        ///     Clears the article.
        /// </summary>
        /// <returns>article without e.g. [[ ]] or <ref></ref> </returns>
        public static string ClearArticle(string article) {
            var result = RemoveRef(article);

            return result;
        }

        /// <summary>
        ///     Removes the <ref></ref> from article and text between those tags.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>article without <ref></ref> in it</returns>
        private static string RemoveRef(string article) {
            var indexFrom = article.IndexOf("<ref>", StringComparison.Ordinal) + "<ref>".Length;
            var indexTo = article.LastIndexOf("</ref>", StringComparison.Ordinal);
            var res = article.Substring(indexFrom, indexTo - indexFrom);

            return article.Replace(res, "").Replace("<ref>", string.Empty).Replace("</ref>", string.Empty);
        }
    }

}