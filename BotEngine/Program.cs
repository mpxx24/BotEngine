using System;
using WikipediaApi;

namespace BotEngine {

    internal class Program {
        private static void Main() {
            var article = WikiApi.GetWikipediaArticle("Rzekotka drzewna");
            Console.WriteLine(article);


            //var str = "[[program komputerowy | programy kom]]";


            ////article.Replace(res, "").Replace("<ref>", string.Empty).Replace("</ref>", string.Empty);

            // var res = str.Split('|')[1]
            //     .Replace("]]", string.Empty)
            //     .TrimStart();

            // Console.WriteLine(res2);
            Console.ReadKey();
        }
    }

}