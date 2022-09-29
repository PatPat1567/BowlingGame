using BowlingGame.Models;

namespace BowlingGame.Interfaces;

public interface IGameRepository
{
    IEnumerable<Game> GetGames();
    Game? GetGame(int gameId);
    bool DoesGameExist(int gameId);
    Game AddGame(int lastGameId, string playerName);
    void DeleteGame(Game game);
}