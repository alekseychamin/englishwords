using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleTelegramBot
{
    public interface IWebClient
    {
        Task<string> GetEntity(string url);
        Task<string> PostEntity(string url, object obj);
        Task<string> PutEntity(string url, object obj);
        Task<string> DeleteEntity(string url);
    }
}