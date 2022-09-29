namespace BowlingGame.Models;

public class Frame
{
    public int Id { get; set; }
    public int FrameNumber { get; set;}
    public int Points { get; set; } = 0;
    public bool IsSpare { get; set; } = false;
    public bool IsStrike { get; set; } = false;
    public bool IsComplete { get; set; } = false;
    public IList<Roll> Rolls { get; set; } = new List<Roll>();

    public Frame(int id)
    {
        Id = id;
        FrameNumber = id + 1;
    }
}