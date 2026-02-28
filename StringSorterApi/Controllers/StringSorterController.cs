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
            return Ok("Endpoint working?");
        }
    }
}
