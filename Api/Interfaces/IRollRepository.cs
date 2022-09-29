using BowlingGame.Models;

namespace BowlingGame.Interfaces;

public interface IRollRepository
{
    IEnumerable<Roll> GetRolls(int gameId, int frameId);
}