﻿using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using ConsoleTelegramBot.Updates;
using NLog;
using System.Collections.Generic;
using Telegram.Bot;

namespace ConsoleTelegramBot
{
    public interface IConfiguration
    {
        string BotToken { get; }
        string UrlRandomWord { get; }
        string UrlCategory { get; }
        string UrlEnglishWord { get; }

        IOperation Operation { get; }
        ITelegramBotClient Bot { get; }
        Dictionary<string, INamedCommand> ListCommand { get; }
        List<IUniqueChatId> UniqueChatIds { get; }
        ILogger Logger { get; }
        ISendMessageCommand SendMessageCommand { get; set; }
        ICommand ShowAllCommand { get; }        
        IUpdate TextMessageUpdate { get; }
        IUpdate CallBackQueryUpdate { get; }
        IUpdate UnknownMessageUpdate { get; }
        IWebClient WebClient { get; }        
    }
}