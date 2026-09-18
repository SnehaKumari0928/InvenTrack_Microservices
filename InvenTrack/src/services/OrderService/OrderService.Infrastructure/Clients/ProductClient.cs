using OrderService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Net;

namespace OrderService.Infrastructure.Clients
{
    public class ProductClient: IProductClient
    {
        private readonly HttpClient _httpClient;
        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductInfo?> GetProductAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"api/product/{productId}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<ProductInfo>();
        }
    }
}
