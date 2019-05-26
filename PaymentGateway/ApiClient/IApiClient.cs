using System.Threading;
using System.Threading.Tasks;

namespace ApiClient
{
    public interface IApiClient
    {
        Task<TResult> GetAsync<TResult>(string path, CancellationToken cancellationToken);

        Task<TResult> PostAsync<TResult>(string path, CancellationToken cancellationToken, object request = null);
    }
}