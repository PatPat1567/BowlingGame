using BowlingGame.Models;

namespace BowlingGame;

public class BowlingDataStore
{
    public List<Game> Games { get; set; }
    public static BowlingDataStore Current { get; } = new BowlingDataStore();

    public BowlingDataStore()
    {
        //init dummy data
        Games = new List<Game>();
    }
}