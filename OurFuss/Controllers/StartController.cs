using Microsoft.AspNetCore.Mvc;

namespace OurFuss.Controllers;

[ApiController]
[Route("[controller]")]
public class StartController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Есть контакт!");
    }
}