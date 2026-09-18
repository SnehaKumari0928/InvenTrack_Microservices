using OrderService.Application.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Clients
{
    public class InventoryClient : IInventoryClient
    {
        private readonly HttpClient _httpClient;

        public InventoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task ReserveStockAsync(Guid productId, int quantity)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Inventory/product/{productId}/reserve", new { quantity });

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to reserve stock for product '{productId}'. Status: {response.StatusCode}, Error: {error}");
            }
        }

        public async Task ReleaseStockAsync(Guid productId, int quantity)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Inventory/product/{productId}/release", new { quantity });

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to release stock for product '{productId}'. Status: {response.StatusCode}, Error: {error}");
            }
        }
    }
}
