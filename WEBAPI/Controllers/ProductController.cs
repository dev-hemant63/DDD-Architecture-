using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : ControllerBase
    {
        [HttpPost(nameof(Test))]
        public async Task<IActionResult> Test()
        {
            return Ok("Chal gaya");
        }
    }
}
