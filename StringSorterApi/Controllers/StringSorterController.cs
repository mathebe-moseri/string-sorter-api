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

                    char[] chars = request.Data!.ToCharArray();

                    Array.Sort(chars);

                    var word = chars.Select(c => c.ToString()).ToArray();

                    return Ok(new { word });
                }

                [HttpPost("validate-endpoint")]
                public async Task<IActionResult> Validate([FromBody] ValidationRequest request)
                {

                    // check null / empty / spaces
                    if (request == null ||
                        string.IsNullOrWhiteSpace(request.Email) ||
                        string.IsNullOrWhiteSpace(request.Url))
                    {
                        return BadRequest(new { error = "Email and Url are required." });
                    }

                    // passed validation, return something (for now)
                    return Ok(new { message = "Validation passed ✅" });

                }

        }
}
