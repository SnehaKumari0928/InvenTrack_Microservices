using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;
using ProductService.Application.DTOs;
using Shared.Common.Responses;

namespace ProductService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? category, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string? sortBy = null)
        {
            if (!string.IsNullOrWhiteSpace(search) || !string.IsNullOrWhiteSpace(category) || pageNumber != 1 || pageSize != 20 || !string.IsNullOrWhiteSpace(sortBy))
            {
                var req = new ProductService.Application.Requests.ProductQueryRequest(search, category, pageNumber, pageSize, sortBy);
                var result = await _service.QueryAsync(req);
                Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
                return Ok(result.Items);
            }

            var all = await _service.GetAllAsync();
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ProductDto>>> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            var resp = new Response<ProductDto>(true, "Product retrieved successfully", item);
            return Ok(resp);
        }

        [HttpPost]
        public async Task<ActionResult<Response<ProductDto>>> Create(CreateProductRequest request)
        {
            var created = await _service.CreateAsync(request);
            var resp = new Response<ProductDto>(true, "Product created successfully", created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, resp);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<object?>>> Update(Guid id, UpdateProductRequest request)
        {
            await _service.UpdateAsync(id, request);
            var resp = new Response<object?>(true, "Product updated successfully", null);
            return Ok(resp);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<object?>>> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            var resp = new Response<object?>(true, "Product deleted successfully", null);
            return Ok(resp);
        }

        [HttpGet("/sku/{sku}")]
        public async Task<ActionResult<Response<ProductDto>>> GetBySku(string sku)
        {
            var item = await _service.GetBySkuAsync(sku);
            var resp = new Response<ProductDto>(true, "Product retrieved successfully", item);
            return Ok(resp);
        }
    }
}
