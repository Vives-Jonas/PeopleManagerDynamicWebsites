using PeopleManager.Dto.Results;
using System.Net.Http.Json;
using PeopleManager.Dto.Requests;

namespace PeopleManager.Sdk
{
    public class FunctionClient(IHttpClientFactory httpClientFactory)
    {
        public async Task<IList<FunctionResult>> Find()
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.GetAsync("functions");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<FunctionResult>>();
            return result ?? new List<FunctionResult>();
        }

        public async Task<FunctionResult?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.GetAsync($"functions/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FunctionResult>();
            return result;
        }

        public async Task<FunctionResult?> Create(FunctionRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.PostAsJsonAsync("functions", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FunctionResult>();
            return result;
        }

        public async Task<FunctionResult?> Update(int id, FunctionRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.PutAsJsonAsync($"functions/{id}", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FunctionResult>();
            return result;
        }

        public async Task Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.DeleteAsync($"functions/{id}");
            response.EnsureSuccessStatusCode();

        }
    }
}
