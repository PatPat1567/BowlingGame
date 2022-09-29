using BowlingGame.Interfaces;
using BowlingGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingGame.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> logger;
    private readonly IGameRepository gameRepository;

    public GameController(ILogger<GameController> logger, IGameRepository gameRepository)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
    }

    [HttpGet]
    public IActionResult GetGames()
    {
        return Ok(gameRepository.GetGames());
    }

    [HttpGet("{id}", Name = "GetGame")]
    public IActionResult GetGame(int id)
    {
        var game = gameRepository.GetGame(id);

        if(game is null)
        {
            return NotFound();   
        }

        return Ok(game);
    }

    [HttpPost]
    public IActionResult Create(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return BadRequest();
        }

        var lastGameId = gameRepository.GetGames().Any() ? gameRepository.GetGames().Max(x => x.Id) : -1;

        var game = gameRepository.AddGame(lastGameId, playerName);

        return CreatedAtRoute("GetGame",
            new {
                id = game.Id
            },
            game);
    }

    [HttpPut]
    public IActionResult UpdatePlayerName(int id, [FromBody]string playerName)
    {
        var game = gameRepository.GetGame(id);
        if (game is null)
        {
            return NotFound();
        }

        game.PlayerName = playerName;

        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var game = gameRepository.GetGame(id);

        if (game is null)
        {
            return NotFound();
        }

        gameRepository.DeleteGame(game);
        return NoContent();
    }
}
