namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "Hello World";
    }
}
