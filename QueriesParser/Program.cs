using System;
using System.IO;
using Newtonsoft.Json;

namespace QueriesParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText("../../text.txt");
            text = text.Remove(text.Length - 4, 4);
            var queries = text.Split(new string[] { "------------------------------------------------------------------" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var query in queries)
            {
                var chunks = query.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var queryBodyAsString = chunks[1].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[0];
                var queryBody = JsonConvert.DeserializeObject<Query>(queryBodyAsString);
                var fileName = queryBody.query != null ? queryBody.query : queryBody.id;

                var fileContent = chunks[2];
                File.WriteAllText(string.Format("../../results/{0}", fileName), fileContent);
            }
        }
    }

    public class Query
    {
        public string query { get; set; }

        public string id { get; set; }
    }
}
