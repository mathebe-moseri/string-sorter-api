using Microsoft.AspNetCore.Mvc;
using StringSorterApi.Models;
using System.Net.Mail;

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

                    char[] chars = request.Data!.ToCharArray();

                    Array.Sort(chars);

                    var word = chars.Select(c => c.ToString()).ToArray();

                    return Ok(new { word });
                }

        }
}
