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

                    // validate email format
                    try
                    {
                        var addr = new MailAddress(request.Email);

                        if (addr.Address != request.Email.Trim())
                        {
                            return BadRequest(new { error = "Email format is invalid." });
                        }
                    }
                    catch
                    {
                        return BadRequest(new { error = "Email format is invalid." });
                    }

                    // url format validation
                    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri) ||
                        (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                    {
                        return BadRequest(new { error = "Url must be a valid http/https address." });
                    }
                    // try calling the endpoint
                    try
                    {
                        using var httpClient = new HttpClient();

                        var response = await httpClient.GetAsync(request.Url);

                        return Ok(new
                        {
                            email = request.Email,
                            testedUrl = request.Url,
                            statusCode = (int)response.StatusCode,
                            isSuccess = response.IsSuccessStatusCode
                        });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new
                        {
                            email = request.Email,
                            testedUrl = request.Url,
                            error = ex.Message
                        });
                    }

                }
        }
}
