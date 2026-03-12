using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OpticalServer.Data;
using OpticalServer.Functions;
using OpticalServer.Models;
using System.Text.Json;
using static OpticalServer.Data.LevelObject;

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
    public async Task<IActionResult> GetLevelData(long id)
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
        LevelData levelData = null;
        try
        {
            levelData = JsonSerializer.Deserialize<LevelObject.LevelData>(data.GetRawText());
        }
        catch (Exception ex)
        {
            RuntimeFunctions.Request("Invalid level data: " + ex.Message, true);
            return BadRequest("Invalid level data: " + ex.Message);
        }
        string json = JsonSerializer.Serialize(levelData);

        JsonElement jsonElement = JsonDocument.Parse(json).RootElement;

        var level = await _levelFunctions.EditLevel(id, jsonElement);

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