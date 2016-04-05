using System;
using WikipediaApi;

namespace BotEngine {

    internal class Program {
        private static void Main() {
            var article = WikiApi.GetWikipediaArticle("Rzekotka drzewna");
            Console.WriteLine(article);

            Console.ReadKey();
        }
    }

}