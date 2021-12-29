using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Seeds
{
    public class SeedRandomEnglishWord : ISeed
    {
        private readonly Random _random = new Random();

        public List<EnglishGroup> GetEnglishGroups()
        {
            var countGroup = 25;
            var result = new List<EnglishGroup>();

            for (int i = 0; i < countGroup; i++)
            {
                result.Add(CreateEnglishGroup($"Group{i}", 50));
            }

            return result;
        }

        private EnglishGroup CreateEnglishGroup(string groupName, int wordCount)
        {
            var group = new EnglishGroup(){ Name = groupName };

            for (int i = 0; i < wordCount; i++)
            {
                group.EnglishWords.Add(CreateRandomEnglishWord(group));
            }

            return group;
        }

        private EnglishWord CreateRandomEnglishWord(EnglishGroup group)
        {
            return new EnglishWord()
            {
                Phrase = GetRandomString(10),
                Transcription = GetRandomString(15),
                Translation = GetRandomString(20),
                Example = GetRandomString(30),
                PictureUri = GetRandomString(30),
                EnglishGroup = group
            };
        }

        private string GetRandomString(int charCount)
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
