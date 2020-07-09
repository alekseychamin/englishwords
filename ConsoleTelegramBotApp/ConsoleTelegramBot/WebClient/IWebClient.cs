﻿using System.Threading.Tasks;

namespace ConsoleTelegramBot
{
    public interface IWebClient
    {
        Task<string> GetRandomWord(string url);
        Task<string> PostNewWord(string url, object obj);
    }
}