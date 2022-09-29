namespace BowlingGame.Models;

public class Game
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = String.Empty;
    public IList<Frame> Frames { get; set; } = new List<Frame>();
    public int Score => Frames.Sum(x => x.Points);
    public bool IsGameOver => Frames.Count == 10 && Frames.All(x => x.IsComplete);

    public Game(int id, string playerName)
    {
        Id = id;
        PlayerName = playerName;
    }
}