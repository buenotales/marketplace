using Microsoft.AspNetCore.Mvc;
using Mkt.Business.OrderSubmitted.Application.Dto.Request;
using Mkt.Business.OrderSubmitted.Application.Dto.Response;
using Mkt.Business.OrderSubmitted.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mkt.Business.OrderSubmitted.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersSubmittediesController : ControllerBase
    {
        private readonly ManagmentService managmentService;

        public OrdersSubmittediesController(ManagmentService managmentService)
        {
            this.managmentService = managmentService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterPostRequestDto requestDto)
        {
            var responseDto = await managmentService.RegisterAsync(requestDto);

            if (responseDto == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterPostResponseDto() { OrderId = null, Message = "Falha ao registrar pedido." });

            if (responseDto.OrderId == null)
                return BadRequest(responseDto);

            return Ok(responseDto);
        }
    }
}
