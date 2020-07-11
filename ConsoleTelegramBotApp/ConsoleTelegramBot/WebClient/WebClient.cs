using NLog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleTelegramBot
{
    public class WebClient : IWebClient
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly ILogger _logger;

        public WebClient(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<string> PostNewWord(string url, object obj)
        {
            _client.DefaultRequestHeaders.Accept.Clear();

            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Add("Telegram-bot", "English words to learn");

            var objStr = JsonSerializer.Serialize(obj);
            var content = new StringContent(objStr, Encoding.UTF8, "application/json");

            try
            {
                var result = await _client.PostAsync(url, content);
                return result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ocuried");                
                return ex.Message;
            }
        }

        public async Task<string> PutNewWord(string url, object obj)
        {
            _client.DefaultRequestHeaders.Accept.Clear();

            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Add("Telegram-bot", "English words to learn");

            var objStr = JsonSerializer.Serialize(obj);
            var content = new StringContent(objStr, Encoding.UTF8, "application/json");

            try
            {
                var result = await _client.PutAsync(url, content);
                return result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ocuried");
                return ex.Message;
            }
        }

        public async Task<string> DeleteWord(string url)
        {
            _client.DefaultRequestHeaders.Accept.Clear();

            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Add("Telegram-bot", "English words to learn");

            try
            {
                var result = await _client.DeleteAsync(url);
                return result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ocuried");
                return ex.Message;
            }
        }

        public async Task<string> GetStringFromUrl(string url)
        {
            _client.DefaultRequestHeaders.Accept.Clear();

            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Add("Telegram-bot", "English words to learn");

            try
            {
                return await _client.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ocuried");
                return ex.Message;
            }
        }
    }
}
