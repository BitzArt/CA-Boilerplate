using Microsoft.AspNetCore.Mvc;

namespace BitzArt.CA.Infrastructure.AspNetCore.TestApp.Controllers;

[Route("")]
public class TestController(ILogger<TestController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult TestGet()
    {
        logger.LogInformation("TestGet");
        return Ok("OK");
    }

    [HttpPost]
    public IActionResult TestPost([FromBody] TestRequest request)
    {
        logger.LogInformation("TestPost\nReceived value: {value}", request.Value);
        if (request.Value == "error") throw ApiException.InternalError("Test exception");
        return Ok(new { receivedData = request });
    }

    public record TestRequest(string Value);
}
