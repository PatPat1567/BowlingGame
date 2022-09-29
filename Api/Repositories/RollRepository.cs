using BowlingGame.Interfaces;
using BowlingGame.Models;

namespace BowlingGame.Repositories;

public class RollRepository : IRollRepository
{
    public IEnumerable<Roll> GetRolls(int gameId, int frameId)
    {
        throw new NotImplementedException();
    }
}