using Microsoft.AspNetCore.Mvc;
using Mkt.Domain.Order.Application.Dto.Request;
using Mkt.Domain.Order.Application.Dto.Response;

namespace Mkt.Domain.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost("Register")]
        public IActionResult Post([FromBody] RegisterPostRequestDto requestDto)
        {
            var responseDto = new RegisterPostResponseDto() { OrderId = DateTime.Now.Millisecond };
            return Ok(responseDto);
        }
    }
}
