using FrontEnd.Data;

namespace FrontEnd.Data;

public class Frame
{
    public int FrameNumber { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; set; } = false;
    public bool IsSpare { get; set; } = false;
    public bool IsStrike { get; set; } = false;
    public IList<Roll> Rolls { get; set; } = new List<Roll>();
}