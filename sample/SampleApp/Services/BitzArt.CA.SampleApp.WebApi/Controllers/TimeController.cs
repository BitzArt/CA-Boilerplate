using BitzArt.CA.SampleApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace BitzArt.CA.SampleApp;

[Route("time")]
public class TimeController(ITimeService timeService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCurrentTime()
    {
        var currentTime = timeService.GetCurrentTime();
        return Ok(currentTime);
    }
}
