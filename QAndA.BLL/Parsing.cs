using Newtonsoft.Json;
using QAndA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace QAndA.BLL
{
    public class Parsing
    {
        public static string ConvertTxtToJson(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                var result = new List<Question>();
                string line;
                int currentId = 1;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line) || line.EndsWith(";") )
                        continue;
                    try
                    {
                        result.Add(ParseLine(line, currentId));
                        currentId++;
                    }catch
                    {

                    }
                }
                var json =  JsonConvert.SerializeObject(result);
                return json.ToString();
            }
        
        }
        public static Question ParseLine(string line, int newId)
        {
            line = line.Replace("\"", "");
            var qLine = line.Split('~');
            var q = new Question()
            {
                id = newId,
                question = qLine[0],
                author = qLine[1],
            };
            var options = new List<Option>();
            foreach (var op in qLine[2].ToString().Split('¬'))
                options.Add(new Option() {
                    text  = op,
                    answer = "false"
                });
            options[0].answer = "true";
            options.Shuffle();
            q.options = options;
            return q;
        }
    }
}
