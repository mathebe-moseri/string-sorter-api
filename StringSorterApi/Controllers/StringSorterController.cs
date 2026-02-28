using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StringSorterApi.Models;

namespace StringSorterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringSorterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Sort([FromBody] SortRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Data))
            {
                return BadRequest(new { error = "Data field is required." });
            }

            return Ok("Endpoint working?");
        }
    }
}
