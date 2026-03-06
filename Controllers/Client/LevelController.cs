using Microsoft.AspNetCore.Mvc;

namespace OpticalServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LevelController : ControllerBase
{
    [HttpGet("list")]
    public IActionResult LevelList()
    {
        // level list logic
        return Ok("LevelList");
    }
    [HttpGet("get/{id}")]
    public IActionResult LevelData()
    {
        // load the level
        return Ok("Level");
    }
    [HttpPost("post/{id}")]
    public IActionResult SetLevel()
    {
        // change level data
        return Ok("Set!");
    }
    [HttpDelete("delete/{id}")]
    public IActionResult DeleteLevel()
    {
        // delete logic
        return Ok("Deleted");
    }
    
}