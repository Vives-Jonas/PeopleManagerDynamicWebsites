using System.Net.Http.Json;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;


namespace PeopleManager.Sdk
{
    public class PersonClient(IHttpClientFactory httpClientFactory)
    {
        public async Task<IList<PersonResult>?> Find()
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.GetAsync("People");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<PersonResult>>();
            return result ?? new List<PersonResult>();
        }

        public async Task<PersonResult?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.GetAsync($"People/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResult>();
            return result;
        }

        public async Task<PersonResult?> Create(PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.PostAsJsonAsync("People", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResult>();
            return result;
        }

        public async Task<PersonResult?> Update(int id, PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.PutAsJsonAsync($"People/{id}", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResult>();
            return result;
        }

        public async Task Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var response = await httpClient.DeleteAsync($"People/{id}");
            response.EnsureSuccessStatusCode();
            
        }
    }
}
