using Microsoft.AspNetCore.Mvc;
using Mkt.Domain.Profile.Application.Dto.Response;

namespace Mkt.Domain.Profile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IEnumerable<ProfileGetResponseDto> profiles = new List<ProfileGetResponseDto>()
        {
            new ProfileGetResponseDto(1, "Tales Henrique Bueno Rodrigues", "talesbueno@gmail.com", "67887967852"),
            new ProfileGetResponseDto(2, "João Silva", "joaosilva@gmail.com", "93461788294"),
            new ProfileGetResponseDto(3, "Marcelo Santos", "marcelosantos@gmail.com", "0983612845")
        };

        [HttpGet]
        public IActionResult ListAllAsync()
        {
            return Ok(new { Items = profiles });
        }
    }
}
