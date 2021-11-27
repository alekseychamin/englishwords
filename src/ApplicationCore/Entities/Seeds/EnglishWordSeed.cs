﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Seeds
{
    public class EnglishWordSeed
    {
        private static Random _random = new Random();

        public static List<EnglishGroup> Seed()
        {
            var countGroup = 25;
            var result = new List<EnglishGroup>();

            for (int i = 0; i < countGroup; i++)
            {
                result.Add(CreateEnglishGroup($"Group{i}", 50));
            }

            return result;
        }

        private static EnglishGroup CreateEnglishGroup(string groupName, int wordCount)
        {
            var group = new EnglishGroup(name: groupName);
            
            for (int i = 0; i < wordCount; i++)
            {
                group.AddItem(CreateRandomEnglishWord(group));
            }

            return group;
        }

        private static EnglishWord CreateRandomEnglishWord(EnglishGroup group)
        {
            return new EnglishWord(
                phrase: GetRandomString(10),
                transcription: GetRandomString(15),
                translation: GetRandomString(20),
                example: GetRandomString(30),
                pictureUri: GetRandomString(30),
                englishGroup: group);
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
