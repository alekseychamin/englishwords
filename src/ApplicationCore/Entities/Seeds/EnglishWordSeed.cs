using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Seeds
{
    public class EnglishWordSeed
    {
        private static Random _random = new Random();

        public static List<EnglishGroup> Seed()
        {
            var countGroup = 3;
            var result = new List<EnglishGroup>();

            for (int i = 0; i < countGroup; i++)
            {
                result.Add(CreateEnglishGroup($"Group{i}", 3));
            }

            return result;
        }

        private static EnglishGroup CreateEnglishGroup(string groupName, int wordCount)
        {
            var group = new EnglishGroup
            { 
                Name = groupName,
                EnglishWords = new List<EnglishWord>()
            };
            
            for (int i = 0; i < wordCount; i++)
            {
                group.EnglishWords.Add(CreateRandomEnglishWord(group));
            }

            return group;
        }

        private static EnglishWord CreateRandomEnglishWord(EnglishGroup group)
        {
            return new EnglishWord 
            {
                Phrase = GetRandomString(10),
                Transcription = GetRandomString(15),
                Translation = GetRandomString(20),
                Example = GetRandomString(30),
                EnglishGroup = group
            };
        }

        private static string GetRandomString(int charCount)
        {
            var chars = "abcdefghjklmnsquwxyz123456";
            var result = new StringBuilder(charCount);

            for (int i = 0; i < chars.Length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}
