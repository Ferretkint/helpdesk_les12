using VivesHelpdesk.Model;

namespace VivesHelpdesk.Ui.WebApp.Sdk
{
    public class PersonSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Person>> Find()
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/people";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<IList<Person>>();
            
            if (people is null)
            {
                return new List<Person>();
            }

            return people;
        }

        public async Task<Person?> Get(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/people/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<Person?> Create(Person person)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/people";
            var response = await client.PostAsJsonAsync(route, person);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<Person?> Update(int id, Person person)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/people/{id}";
            var response = await client.PutAsJsonAsync(route, person);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Person>();
        }

        public async Task Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/people/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
