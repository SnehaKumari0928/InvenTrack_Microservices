using OrderService.Application.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Clients
{
    public class SupplierClient : ISupplierClient
    {
        private readonly HttpClient _httpClient;

        public SupplierClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<SupplierInfo?> GetSupplierAsync(Guid supplierId)
        {
            var response = await _httpClient.GetAsync($"api/Supplier/{supplierId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to get supplier '{supplierId}'. Status: {response.StatusCode}, Error: {error}");
            }

            return await response.Content.ReadFromJsonAsync<SupplierInfo>();
        }
    }
}
