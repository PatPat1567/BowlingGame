using BowlingGame.Interfaces;
using BowlingGame.Models;

namespace BowlingGame.Repositories;

public class GameRepository : IGameRepository
{
    private readonly BowlingDataStore bowlingDataStore;

    public GameRepository(BowlingDataStore bowlingDataStore)
    {
        this.bowlingDataStore = bowlingDataStore ??  throw new ArgumentNullException(nameof(bowlingDataStore));
    }
    public IEnumerable<Game> GetGames()
    {
        return bowlingDataStore.Games;
    }
    public Game? GetGame(int gameId)
    {
        return bowlingDataStore.Games.FirstOrDefault(x => x.Id == gameId);
    }
    public bool DoesGameExist(int gameId)
    {
        return bowlingDataStore.Games.Any(x => x.Id == gameId);
    }
    public Game AddGame(int lastGameId, string playerName)
    {
        var game = new Game(++lastGameId, playerName);

        bowlingDataStore.Games.Add(game);

        return game;
    }
    public void DeleteGame(Game game)
    {
        bowlingDataStore.Games.Remove(game);
    }
}