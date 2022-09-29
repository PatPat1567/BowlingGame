using FrontEnd.Data;

namespace FrontEnd.Data;

public class Game {
    public IList<Frame> Frames { get; set; } = new List<Frame>();
    public int Score {
        get => Frames.Sum(x => x.Points);
    }
    public bool IsGameOver 
    {
        get => Frames.Count == 10 && Frames.All(x => x.IsComplete);
    }
}