using Microsoft.AspNetCore.Mvc;
using OpticalServer.Functions;

[ApiController]
[Route("api/[controller]")]
public class ReactionsController : ControllerBase
{
    private readonly LevelReactions _levelReactions;
    public ReactionsController(DatabaseContext db)
    {
        LevelReactions _levelReactions = new LevelReactions(db);
    }

    [HttpPost("like/{levelId}/{userId}")]
    public async Task<IActionResult> LikeLevel(long levelId, long userId)
    {
        var reaction = await _levelReactions.AddRating(levelId, userId, true);
        return Ok(reaction);
    }
    [HttpPost("dislike/{levelId}/{userId}")]
    public async Task<IActionResult> DislikeLevel(long levelId, long userId)
    {
        var reaction = await _levelReactions.AddRating(levelId, userId, false);
        return Ok(reaction);
    }
    [HttpGet("dislikes/{levelId}")]
    public async Task<IActionResult> GetDislikes(long levelId)
    {
        var reactions = await _levelReactions.GetReactions(levelId);
        return Ok(reactions.dislikes);
    }
    [HttpGet("likes/{levelId}")]
    public async Task<IActionResult> GetLikes(long levelId)
    {
        var reactions = await _levelReactions.GetReactions(levelId);
        return Ok(reactions.likes);
    }
    [HttpGet("all/{levelId}")]
    public async Task<IActionResult> GetAllReactions(long levelId)
    {
        var reactions = await _levelReactions.GetReactions(levelId);
        return Ok(reactions);
    }
}