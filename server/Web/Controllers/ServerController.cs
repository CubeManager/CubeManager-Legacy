namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
            Process[] localAll = Process.GetProcesses();
            Console.Write(localAll);
            // foreach (Process server in localAll) {
            // }
            // return string.Join(", ", (object[])localAll);
            return string.Join("/n", (object[])(typeof(Process).GetProperties()));
    }
}
