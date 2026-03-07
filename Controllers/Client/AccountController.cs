using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using OpticalServer.Models;
using OpticalServer.Functions;

namespace OpticalServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserFunctions _userFunctions;
    public AccountController(DatabaseContext db)
    {
        _userFunctions = new UserFunctions(db);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDTO user)
    {
        var authenticatedUser = await _userFunctions.AuthenticateUser(user);
        
        if (authenticatedUser == null)
            return Unauthorized();

        return Ok("Login successful");
    }
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] UserDTO user)
    {
        var existingUser = await _userFunctions.GetUserByUsername(user.UserName);
        if (existingUser != null)
            return BadRequest("User already exists");

        await _userFunctions.CreateUser(user);

        return Ok(user);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAccount([FromBody] UserDTO user)
    {
        var deletingUser = await _userFunctions.GetUserByUsername(user.UserName);
        if (deletingUser == null)
            return NotFound("User not found");
        await _userFunctions.DeleteUser(deletingUser.UserId);
        return Ok("Account deleted");
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetAccountInfo([FromBody] UserDTO user)
    {
        var existingUser = await _userFunctions.GetUserByUsername(user.UserName);
        if (existingUser == null)
            return NotFound("User not found");

        return Ok(existingUser);
    }
    
}