using Microsoft.AspNetCore.Mvc;
using OpticalServer.Functions;

namespace OpticalServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerController : ControllerBase
{
    private readonly IHostApplicationLifetime _lifetime;
    
    public ServerController(IHostApplicationLifetime lifetime)
    {
        _lifetime = lifetime;
    }
    [HttpPost("shutdown")]
    public IActionResult ShutDown()
    {
        RuntimeFunctions.WriteLine("server shutting down...", false);

        _lifetime.StopApplication();
        
        return Ok("Server shutting down...");
    }
    [HttpGet("status")]
    // get status
    public IActionResult Status()
    {
        RuntimeFunctions.ServerRequests++;
        return Ok(
            $"Server running:\n\nClient requests:{RuntimeFunctions.Requests}\nServer requests:{RuntimeFunctions.ServerRequests}\nTotal requests:{RuntimeFunctions.ServerRequests + RuntimeFunctions.Requests}");
    }
    [HttpGet("note/{text}")]
    public IActionResult Note(string text)
    {
        RuntimeFunctions.ServerRequests++;
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        RuntimeFunctions.WriteLine("note("+text+") from("+ip+")",false);
        RuntimeFunctions.Note += ip + ": " + text + "\n";

        return Ok(RuntimeFunctions.Note);
    }
    [HttpGet("help")]
    public IActionResult Help()
    {
        RuntimeFunctions.ServerRequests++;
        return Ok("Optical Server:\n\n/note/message\n/shutdown\n/status\n/help");
    }
    [HttpGet()]
    public IActionResult Ping()
    {
        RuntimeFunctions.ServerRequests++;
        RuntimeFunctions.WriteLine("Server pinged!");
        return Ok(1);
    }
}