using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Newtonsoft.Json;

using WikipediaApi.Helpers;

namespace WikipediaApi {

    public static class WikiApi {
        /// <summary>
        ///     The names helper
        /// </summary>
        private static List<string> namesHelper = new List<string>();
        private static RootObject result = new RootObject();
        /// <summary>
        ///     Gets or sets the word.
        /// </summary>
        /// <value>
        ///     The word.
        /// </value>
        private static string word { get; set; }
        /// <summary>
        ///     Gets the URL.
        /// </summary>
        /// <value>
        ///     The URL.
        /// </value>
        private static string url => string.Format("https://pl.wikipedia.org/w/api.php?action=query&titles={0}&prop=revisions&rvprop=content&format=json", word);

        /// <summary>
        ///     Gets the response object.
        /// </summary>
        /// <param name="list">list of possible search parameters</param>
        /// <returns><see cref="RootObject" /> object with data</returns>
        private static RootObject GetResponseObject(List<string> list) {
            foreach (var wordToSearch in list) {
                word = wordToSearch;
                var request = (HttpWebRequest) WebRequest.Create(url);
                try {
                    var response = request.GetResponse();
                    using (var responseStream = response.GetResponseStream()) {
                        if (responseStream != null) {
                            var reader = new StreamReader(responseStream, Encoding.UTF8);
                            var wholeText = reader.ReadToEnd();
                            if (wholeText.Contains("\"missing\":\"\"")) {
                                namesHelper = namesHelper.Count == 0 ? list : namesHelper;
                                namesHelper.Remove(wordToSearch);
                                GetResponseObject(namesHelper);
                            }
                            else {
                                namesHelper.Clear();
                                result = JsonConvert.DeserializeObject<RootObject>(wholeText);
                            }
                        }
                        return result ?? new RootObject();
                    }
                }
                catch (WebException ex) {
                    var errorResponse = ex.Response;
                    using (var responseStream = errorResponse.GetResponseStream()) {
                        if (responseStream != null) {
                            var reader = new StreamReader(responseStream, Encoding.UTF8);
                            var errorText = reader.ReadToEnd();
                            //TODO: log
                        }
                        throw;
                    }
                }
            }
            return new RootObject();
        }

        /// <summary>
        ///     Gets the wikipedia article.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>string with Article</returns>
        public static string GetWikipediaArticle(string name) {
            var responseObject = GetResponseObject(WikiApiHelper.PrepareWordForSearching(name));
            var pages = responseObject.Query.Pages;
            var page = pages.First();
            var articleJson = page.Value.ToString();

            var article = JsonConvert.DeserializeObject<ArticleInfo>(articleJson);

            return article.Revisions[0].Content.Substring(0, 300);
        }
    }

}