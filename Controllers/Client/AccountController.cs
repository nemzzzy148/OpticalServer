using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using OpticalServer.Models;

namespace OpticalServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDTO user)
    {
        // user login
        return Ok("okay");
    }
    [HttpPost("signup")]
    public IActionResult SingUp([FromBody] UserDTO user)
    {
        // user sign up
        return Ok("okay");
    }
    [HttpDelete("delete")]
    public IActionResult DeleteAccount([FromBody] UserDTO user)
    {
        // delete account
        return Ok("Okay");
    }
    [HttpGet("get")]
    public IActionResult GetAccountInfo([FromBody] UserDTO user)
    {
        // get account info
        return Ok("Okay");
    }
    
}