using System;

using WikipediaApi;

namespace BotEngine {

    internal class Program {
        private static void Main() {
            var article = WikiApi.GetWikipediaArticle("application programming interface");
            Console.WriteLine(article);

            Console.ReadKey();
        }
    }

}