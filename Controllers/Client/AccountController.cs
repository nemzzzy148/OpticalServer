using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using OpticalServer.Models;
using OpticalServer.Functions;
using Microsoft.AspNetCore.Http.HttpResults;

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

        return Ok(authenticatedUser);
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
    [HttpPut("password/{newPassword}")]
    public async Task<IActionResult> ChangePassword([FromBody] UserDTO user, string newPassword)
    {
        var existingUser = await _userFunctions.GetUserByUsername(user.UserName);

        if (existingUser == null)
            return NotFound("User not found");

        if (existingUser.PasswordHash != user.PasswordHash) 
            return BadRequest("Incorrect password");

        var newUser = await _userFunctions.ChangePassword(existingUser.UserId, newPassword);

        if (newUser != null)
        {
            return NotFound("Failed to change password");
        }

        return Ok("Password changed");
    }
    [HttpPut("username")]
    public async Task<IActionResult> ChangeUsername([FromBody] UserDTO user)
    {
        var existingUser = await _userFunctions.GetUserByUsername(user.UserName);
        if (existingUser == null)
            return NotFound("User not found");

        var newUser = await _userFunctions.ChangeUserName(existingUser.UserId, user.UserName);
        if (newUser != null)
        {
            return NotFound("Failed to change username");
        }
        return Ok("Username changed");
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount([FromBody] UserDTO user)
    {
        var deletingUser = await _userFunctions.GetUserByUsername(user.UserName);
        if (deletingUser == null)
            return NotFound("User not found");

        await _userFunctions.DeleteUser(deletingUser.UserId);
        return Ok("Account deleted");
    }
    [HttpGet]
    public async Task<IActionResult> GetAccountInfo([FromBody] UserDTO user)
    {
        var existingUser = await _userFunctions.GetUserByUsername(user.UserName);
        if (existingUser == null)
            return NotFound("User not found");

        return Ok(existingUser);
    }
    
}