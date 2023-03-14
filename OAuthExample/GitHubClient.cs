using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace OAuthExample
{
    public class GitHubClient
    {
        private readonly HttpClient _httpClient;

        public GitHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
        }

        public async Task<IEnumerable<RepoInfo>> GetRepositories(string authToken)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, "/user/repos?visibility=all");
            _httpClient.DefaultRequestHeaders.Clear();
            httpMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            httpMessage.Headers.UserAgent.Add(new ProductInfoHeaderValue("Sample1", "1.0"));
            var result = await _httpClient.SendAsync(httpMessage);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RepoInfo>>(response);
        }
    }
}
