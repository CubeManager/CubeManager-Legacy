namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase
{
    [HttpGet]
    public ActionResult<Server> GetAll()
    {
        return new Server("This Name");
    }

    public class Server
    {
        public Server(string Name)
        {
            this.Name = Name;
            this.Description = "Desc";
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

