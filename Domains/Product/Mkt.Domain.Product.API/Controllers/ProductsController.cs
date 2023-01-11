using Microsoft.AspNetCore.Mvc;
using Mkt.Domain.Product.Application.Dto.Request;
using Mkt.Domain.Product.Application.Services;
using System.Diagnostics;

namespace Mkt.Domain.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ManagementService managementService;

        public ProductsController(ManagementService managementService)
        {
            this.managementService = managementService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllAsync()
        {
            var response = await managementService.ListAllAsync();
            return Ok(response);
        }

        [HttpGet("{productId}")]
        public IActionResult GetByProductId(int productId)
        {
            var response = managementService.GetByIdAsync(productId);

            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] ProductPostRequestDto requestDto)
        {
            var response = await managementService.SaveAsync(requestDto);

            if (response)
                return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
