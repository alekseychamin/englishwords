using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleTelegramBot
{
    public interface IWebClient
    {
        Task<string> GetStringFromUrl(string url);
        Task<string> PostNewWord(string url, object obj);
    }
}