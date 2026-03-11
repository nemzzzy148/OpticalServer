using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OpticalServer.Functions;
using OpticalServer.Models;

namespace OpticalServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LevelController : ControllerBase
{
    private readonly LevelFunctions _levelFunctions;
    public LevelController(DatabaseContext db)
    {
        _levelFunctions = new LevelFunctions(db);
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateLevel([FromBody] LevelDTO levelDto)
    {
        var level = await _levelFunctions.CreateLevel(levelDto.LevelName, levelDto.OwnerId);
        if (level == null)
            return BadRequest("Level with the same name already exists");
        return Ok(level);
    }
    [HttpGet("list")]
    public async Task<IActionResult> LevelList()
    {
        var levels = await _levelFunctions.GetLevelList();
        return Ok(levels);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> LevelData(long id)
    {
        var levelData = await _levelFunctions.GetLevelData(id);
        if (levelData == null)
            return NotFound("Level not found");
        return Ok(levelData);
    }
    [HttpGet("view/{id}")]
    public async Task<IActionResult> LevelViews(long id)
    {
        var levelData = await _levelFunctions.GetLevelData(id);
        if (levelData == null)
            return NotFound("Level not found");
        return Ok(levelData);
    }
    [HttpGet("views/{id}")]
    public async Task<IActionResult> GetLevelViews(long id)
    {
        var level = await _levelFunctions.GetLevelViews(id);
        return Ok(level);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> SetLevel(long id, [FromBody] JsonElement data)
    {
        var level = await _levelFunctions.EditLevel(id, data);
        if (level == null)
            return NotFound("Level not found");
        return Ok(level);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLevel(long id)
    {
        var level = await _levelFunctions.DeleteLevel(id);
        if (level == null)
            return NotFound("Level not found");
        return Ok(level);
    }
    
}