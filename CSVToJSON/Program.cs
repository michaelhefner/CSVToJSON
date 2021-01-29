using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSVToJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertCsvToJson(args[0]);
        }

        private static void ConvertCsvToJson(string originalPath)
        {
            var paths = Directory.GetFiles(originalPath);
            if(paths.Length > 0)
                foreach (var path in paths)
                {
                    var rosterData = File.ReadAllLines(path);
                    var stringBuilder = new StringBuilder("[");
                    var keys = new List<string>();

                    foreach (var s in rosterData)
                    {
                        var value = s.Split(",");

                        if (rosterData[0] == s) foreach (var vals in value) keys.Add(vals);
                        else
                        {
                            if (value[0].Length > 0)
                            {
                                if (rosterData[1] != s) stringBuilder.Append(',');

                                for (var i = 0; i < keys.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        stringBuilder.Append("{\n");
                                        stringBuilder.Append($"\"{keys[i]}\": ");
                                        stringBuilder.Append($"\"{value[i]}\",\n");
                                    }
                                    else if (i == keys.Count - 1)
                                    {
                                        stringBuilder.Append($"\"{keys[i]}\": ");
                                        stringBuilder.Append($"\"{value[i]}\"\n");
                                    }
                                    else
                                    {
                                        stringBuilder.Append($"\"{keys[i]}\": ");
                                        stringBuilder.Append($"\"{value[i]}\",\n");
                                    }
                                }
                                stringBuilder.Append('}');
                            }
                        }
                    }
                    stringBuilder.Append("]");
                    Console.WriteLine(stringBuilder.ToString());
                    var file = path;
                    file = file.Substring(0, file.LastIndexOf('/') + 1) + "JSON/" + file.Substring(file.LastIndexOf('/') + 1);
                    file = file.Substring(0, file.LastIndexOf('.'));
                    File.WriteAllText($"{file}.json", stringBuilder.ToString());
                }
            else
            {
                Console.WriteLine("No files found in CSVToConvert folder");
            }
        }
        private static void GetObjectStructure(string originalPath)
        {
            var paths = Directory.GetFiles(originalPath);
            foreach (var path in paths)
            {
                var rosterData = File.ReadAllLines(path);
                var stringBuilder = new StringBuilder("[");
                var keys = new List<string>();

                foreach (var s in rosterData)
                {
                    var value = s.Split(",");

                    if (rosterData[0] == s) foreach (var vals in value) keys.Add(vals);
                    else
                    {
                        if (value[0].Length > 0)
                        {
                            if (rosterData[1] != s) stringBuilder.Append(',');

                            for (var i = 0; i < value.Length; i++)
                            {
                                if (i == 0)
                                {
                                    stringBuilder.Append("{\n");
                                    stringBuilder.Append($"\"{keys[i]}\": ");
                                    stringBuilder.Append($"\"{value[i]}\",\n");
                                }
                                else if (i == value.Length - 1)
                                {
                                    stringBuilder.Append($"\"{keys[i]}\": ");
                                    stringBuilder.Append($"\"{value[i]}\"\n");
                                }
                                else
                                {
                                    stringBuilder.Append($"\"{keys[i]}\": ");
                                    stringBuilder.Append($"\"{value[i]}\",\n");
                                }
                            }
                            stringBuilder.Append('}');
                        }
                    }
                }
                stringBuilder.Append("]");
                Console.WriteLine(stringBuilder.ToString());
                var file = path;
                file = file.Substring(0, file.LastIndexOf('/') + 1) + "json/" + file.Substring(file.LastIndexOf('/') + 1);
                file = file.Substring(0, file.LastIndexOf('.'));
                File.WriteAllText($"{file}.json", stringBuilder.ToString());
            }
        }
    }
}