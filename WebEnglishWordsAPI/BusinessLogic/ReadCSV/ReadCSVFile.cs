using DataAccess.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessLogic.ReadCSV
{
    public class ReadCSVFile : IReadCSVFile
    {
        private readonly ILogger<ReadCSVFile> _logger;

        public ReadCSVFile(ILogger<ReadCSVFile> logger)
        {
            _logger = logger;
        }

        public List<EnglishWord> Read(string fileName)
        {
            var keyNamePropIndex = new Dictionary<string, int>();

            var output = new List<EnglishWord>();

            var fullFileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}";

            List<string> lines = File.ReadAllLines(fullFileName, Encoding.UTF8).ToList();

            GetKeyNamePropertyIndex(keyNamePropIndex, lines[0]);

            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var englishWord = GetEnglishWord(line, keyNamePropIndex);

                output.Add(englishWord);
            }

            return output;
        }

        private string[] GetSubStrBySeparator(string line, char separator)
        {
            return line.Split(separator);
        }

        private void GetKeyNamePropertyIndex(Dictionary<string, int> keyNamePropIndex, string line)
        {
            var listStr = GetSubStrBySeparator(line, ';');

            for (int i = 0; i < listStr.Length; i++)
            {
                keyNamePropIndex.Add(listStr[i], i);
            }
        }

        private EnglishWord GetEnglishWord(string line, Dictionary<string, int> keyNamePropIndex)
        {
            EnglishWord englishWord = null;

            var listStr = GetSubStrBySeparator(line, ';');

            var propertyInfo = typeof(EnglishWord).GetProperties();

            foreach (var prop in propertyInfo)
            {
                if (keyNamePropIndex.ContainsKey(prop.Name))
                {
                    if (englishWord is null)
                        englishWord = new EnglishWord();

                    if (prop.Name.Equals("Category"))
                    {
                        var category = new Category { Name = listStr[keyNamePropIndex[prop.Name]] };
                        englishWord.Category = category;
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        DateTime dateTime;

                        if (DateTime.TryParse(listStr[keyNamePropIndex[prop.Name]], out dateTime))
                            prop.SetValue(englishWord, dateTime);
                        else
                            _logger.LogWarning("Date is invalid in line: {0}", listStr[keyNamePropIndex[prop.Name]]);
                    }
                    else
                        prop.SetValue(englishWord,
                                      Convert.ChangeType(listStr[keyNamePropIndex[prop.Name]], prop.PropertyType));
                }

            }

            return englishWord;
        }
    }
}
