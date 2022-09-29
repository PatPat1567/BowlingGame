using FrontEnd.Data;
namespace FrontEnd.Data;

public class GameService
{
    public readonly Random random;
    public const int RollMinimum = 0;
    public const int RollMaximum = 10;
    public Game game { get; set; }

    public GameService()
    {
        game = new Game();
        random = new Random();
    }

    public Game GetCurrentGame()
    {
        return game;
    }
    
    public Game RollBall()
    {
        if (game.IsGameOver)
        {
            return game;
        }
        var activeFrame = game.Frames.FirstOrDefault(x => !x.IsComplete);
        if (activeFrame is null)
        {
            activeFrame = new Frame()
            {
                FrameNumber = game.Frames.Count() + 1
            };
            game.Frames.Add(activeFrame);

            activeFrame.Rolls.Add(CreateRoll());   
        }
        else 
        {
            if (activeFrame.FrameNumber == 10)
            {
                if (activeFrame.IsSpare)
                {
                    activeFrame.Rolls.Add(CreateRoll());  
                }
                else if (activeFrame.IsStrike)
                {
                    if (activeFrame.Rolls.Count == 1)
                    {
                        activeFrame.Rolls.Add(CreateRoll());  
                    } 
                    else if (activeFrame.Rolls.Count == 2)
                    {
                        activeFrame.Rolls.Add(CreateRoll(activeFrame.Rolls.LastOrDefault()?.Value ?? 0));
                    }
                }
                else
                {
                    activeFrame.Rolls.Add(CreateRoll(activeFrame.Rolls.Sum(x => x.Value)));
                }
            }
            else
            {
                activeFrame.Rolls.Add(CreateRoll(activeFrame.Rolls.Sum(x => x.Value)));
            }
        }

        // Determine if frame has spare or strike
        if (activeFrame.Rolls.Sum(x => x.Value) == 10 && !activeFrame.IsSpare && !activeFrame.IsStrike)
        {
            activeFrame.IsSpare = activeFrame.Rolls.Count == 2;
            activeFrame.IsStrike = activeFrame.Rolls.Count == 1;
        }

        // Determine if frame is complete
        activeFrame.IsComplete = IsFrameComplete(activeFrame);

        // Calculate Points
        activeFrame.Points = CalculateFramePoints(activeFrame);

        if (!game.IsGameOver)
        {
            UpdateStrikeFramePoints();
            UpdateSpareFramePoints();
        }

        return game;
    }

    public Game RestartGame()
    {
        game = new Game();
        return game;
    }

    private Roll CreateRoll(int activeFrameValue = 0)
    {
        var frameRollMaximum = RollMaximum - activeFrameValue;
        var rollValue = random.Next(RollMinimum, frameRollMaximum+1);
        return new Roll(rollValue);   
    }

    private bool IsFrameComplete(Frame activeFrame)
    {
        if (activeFrame.FrameNumber != 10 && (activeFrame.IsSpare || activeFrame.IsStrike))
            return true;
        if (activeFrame.FrameNumber != 10 && activeFrame.Rolls.Count == 2 && activeFrame.Rolls.Sum(x => x.Value) != 10)
            return true;
        if (activeFrame.FrameNumber == 10 && activeFrame.Rolls.Count == 2 && !activeFrame.IsSpare && !activeFrame.IsStrike)
            return true;
        if (activeFrame.FrameNumber == 10 && activeFrame.Rolls.Count == 3)
            return true;
        return false;
    }

    private int CalculateFramePoints(Frame activeFrame)
    {
        return activeFrame.Rolls.Sum(x => x.Value);
    }

    private void UpdateStrikeFramePoints()
    {
        var strikeFrames = game.Frames.Where(x => x.IsComplete && x.IsStrike) ?? new List<Frame>();
        foreach (var frame in strikeFrames)
        {
            var nextRolls = new List<Roll>();
            foreach (var nextFrames in game.Frames.Where(x => x.FrameNumber > frame.FrameNumber))
            {
                nextRolls.AddRange(nextFrames.Rolls);
            }

            if (nextRolls.Count > 1)
            {
                frame.Points = 10 + nextRolls[0].Value + nextRolls[1].Value;
            }
            if (nextRolls.Count == 1)
            {
                frame.Points = 10 + nextRolls[0].Value;
            }
        }
    }

    private void UpdateSpareFramePoints()
    {
        var spareFrames = game.Frames.Where(x => x.IsComplete && x.IsSpare) ?? new List<Frame>();
        foreach (var frame in spareFrames)
        {
            var nextRollValue = game.Frames.Where(x => x.FrameNumber > frame.FrameNumber).FirstOrDefault()?.Rolls.FirstOrDefault()?.Value ?? 0;
            frame.Points = 10 + nextRollValue;
        }
    }
}
