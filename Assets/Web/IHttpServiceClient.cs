using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Assets.Web
{
    public interface IHttpServiceClient
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, object content = default);
        Task PostAsync(string uri, object content = default);
        Task<T> PutAsync<T>(string uri, object content = default);
        Task PutAsync(string uri, object content = default);
        Task<T> DeleteAsync<T>(string uri, object content = default);
    }
}
