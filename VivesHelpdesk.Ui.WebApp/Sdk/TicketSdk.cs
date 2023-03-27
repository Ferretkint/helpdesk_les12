using VivesHelpdesk.Model;

namespace VivesHelpdesk.Ui.WebApp.Sdk
{
    public class TicketSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TicketSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Ticket>> Find(int? assignedToId = null)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/tickets";
            if (assignedToId is not null)
            {
                route = $"{route}/{assignedToId}";
            }
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var tickets = await response.Content.ReadFromJsonAsync<IList<Ticket>>();

            if (tickets is null)
            {
                return new List<Ticket>();
            }

            return tickets;
        }

        public async Task<Ticket?> Get(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/tickets/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task<Ticket?> Create(Ticket ticket)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/tickets";
            var response = await client.PostAsJsonAsync(route, ticket);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task<Ticket?> Update(int id, Ticket ticket)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/tickets/{id}";
            var response = await client.PutAsJsonAsync(route, ticket);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/tickets/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
