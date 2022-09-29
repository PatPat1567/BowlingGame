using BowlingGame.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BowlingGame.Controllers;

[ApiController]
[Route("api/games/{gameId}/frames")]
public class FrameController : ControllerBase
{
    private readonly IGameRepository gameRepository;
    private readonly IFrameRepository frameRepository;

    public FrameController(IGameRepository gameRepository, IFrameRepository frameRepository)
    {
        this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        this.frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
    }

    [HttpGet]
    public IActionResult GetFrames(int gameId)
    {
        return Ok(frameRepository.GetFrames(gameId));
    }

    [HttpGet("{frameId}", Name = "GetFrame")]
    public IActionResult GetFrames(int gameId, int frameId)
    {
        if (!gameRepository.DoesGameExist(gameId))
        {
            return NotFound();
        }

        var frame = frameRepository.GetFrame(gameId, frameId);

        if (frame is null)
        {
            return NotFound();
        }

        return Ok(frame);
    }

    [HttpPost]
    public IActionResult Create(int gameId)
    {
        if (!gameRepository.DoesGameExist(gameId))
        {
            return NotFound();
        }
        
        var lastFrame = frameRepository.GetFrames(gameId).LastOrDefault();
        var lastFrameId = lastFrame?.Id ?? -1;

        Console.WriteLine(lastFrameId);

        if (lastFrame?.FrameNumber >= 10)
        {
            return BadRequest("Cannot add more than 10 frames to a game.");
        }

        var frame = frameRepository.AddFrame(gameId, lastFrameId);

        
        return CreatedAtRoute("GetFrame",
            new {
                gameId = gameId,
                frameId = frame.Id
            },
            frame);
    }

    [HttpPut]
    public IActionResult Update(int gameId, int frameId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult Delete(int gameId, int frameId)
    {
        var game = gameRepository.GetGame(gameId);
        if (game is null)
        {
            return NotFound();
        }

        var frame = frameRepository.GetFrame(gameId, frameId);

        if (frame is null)
        {
            return NotFound();
        }

        if (game.Frames.LastOrDefault() != frame)
        {
            return BadRequest("You can only delete the last frame in the game.");
        }

        frameRepository.DeleteFrame(gameId, frame);

        return NoContent();
    }
}