using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<TResult> GetAsync<TResult>(string path, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            var httpResponse = await SendRequestAsync(HttpMethod.Get, path, null, cancellationToken);
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public Task<TResult> PostAsync<TResult>(string path, CancellationToken cancellationToken, object request = null)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpMethod httpMethod, string path, object request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var httpRequest = new HttpRequestMessage(httpMethod, path);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

            return httpResponse;
        }

        private async Task<TResult> DeserializeJsonAsync<TResult>(HttpResponseMessage httpResponse)
        {
            var result = await DeserializeJsonAsync(httpResponse, typeof(TResult));
            return (TResult)result;
        }

        private async Task<dynamic> DeserializeJsonAsync(HttpResponseMessage httpResponse, Type resultType)
        {
            if (httpResponse.Content == null)
                return null;

            var json = await httpResponse.Content.ReadAsStringAsync();
            return json;

            // return _serializer.Deserialize(json, resultType);
        }
    }
}