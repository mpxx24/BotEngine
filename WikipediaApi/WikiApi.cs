using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using WikipediaApi.Helpers;

namespace WikipediaApi {

    public static class WikiApi {
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
        private static string url {
            get {
                return string.Format("https://pl.wikipedia.org/w/api.php?action=query&titles={0}&prop=revisions&rvprop=content&format=json", word);
            }
        }


        /// <summary>
        /// Gets the response object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><see cref="RootObject" /> object with data</returns>
        private static RootObject GetResponseObject(string name) {
            //replace spaces with _
            //uppercase words
            word = WikiApiHelper.PrepareWordForSearching(name);

            var request = (HttpWebRequest) WebRequest.Create(url);
            try {
                var response = request.GetResponse();
                using (var responseStream = response.GetResponseStream()) {
                    if (responseStream != null) {
                        var reader = new StreamReader(responseStream, Encoding.UTF8);
                        return JsonConvert.DeserializeObject<RootObject>(reader.ReadToEnd());
                    }
                    return new RootObject();
                }
            }
            catch (WebException ex) {
                var errorResponse = ex.Response;
                using (var responseStream = errorResponse.GetResponseStream()) {
                    if (responseStream != null) {
                        var reader = new StreamReader(responseStream, Encoding.UTF8);
                        var errorText = reader.ReadToEnd();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the wikipedia article.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>string with Article</returns>
        public static string GetWikipediaArticle(string name) {
            var responseObject = GetResponseObject(name);
            var article = responseObject.Query.Pages.ArtInfo.Revisions[0].Content;
            return ClearArticle(article);
        }

        /// <summary>
        /// Clears the article.
        /// </summary>
        /// <returns>article without e.g. [[ ]] or <ref></ref> </returns>
        private static string ClearArticle(string article) {
            var result = "";

            return result;
        }
    }

}