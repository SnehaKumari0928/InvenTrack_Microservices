using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using InventoryService.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Presentation.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class InventoryController : ControllerBase
    {


        private readonly IInventoryService _inventoryService;


        public InventoryController(IInventoryService inventoryService)

        {

            _inventoryService = inventoryService;

        }


        [HttpGet("{id:guid}")]

        public async Task<ActionResult<InventoryDto>> GetById(Guid id)

        {

            var result = await _inventoryService.GetByIdAsync(id);


            return Ok(result);

        }


        public async Task<ActionResult<InventoryDto>> Create(

       CreateInventoryRequest request)

        {

            var result = await _inventoryService.CreateAsync(request);


            return CreatedAtAction(

                nameof(GetById),

                new { id = result.Id },

                result);

        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAll()

        {

            var result = await _inventoryService.GetAllAsync();


            return Ok(result);

        }





        [HttpPost("product/{productId:guid}/add")]

        public async Task<IActionResult> AddStock(

       Guid productId,

        StockAdjustmentRequest request)

        {

            await _inventoryService.AddStockAsync(

                productId,

                request);


            return NoContent();

        }


        [HttpPost("product/{productId:guid}/remove")]

        public async Task<IActionResult> RemoveStock(

      Guid productId,

      StockAdjustmentRequest request)

        {

            await _inventoryService.RemoveStockAsync(

                productId,

                request);


            return NoContent();

        }


        [HttpPost("product/{productId:guid}/reserve")]

        public async Task<IActionResult> ReserveStock(

       Guid productId,

        StockAdjustmentRequest request)

        {

            await _inventoryService.ReserveStockAsync(

                productId,

                request);


            return NoContent();



        }


        [HttpPost("product/{productId:guid}/release")]

        public async Task<IActionResult> ReleaseStock(

    Guid productId,

     StockAdjustmentRequest request)

        {

            await _inventoryService.ReleaseStockAsync(

                productId,

                request);


            return NoContent();

        }


        [HttpDelete("{id:guid}")]

        public async Task<IActionResult> Delete(Guid id)

        {

            await _inventoryService.DeleteAsync(id);


            return NoContent();

        }


    }

}