using BowlingGame.Interfaces;
using BowlingGame.Models;

namespace BowlingGame.Repositories;

public class FrameRepository : IFrameRepository
{
    private readonly BowlingDataStore bowlingDataStore;

    public FrameRepository(BowlingDataStore bowlingDataStore)
    {
        this.bowlingDataStore = bowlingDataStore ??  throw new ArgumentNullException(nameof(bowlingDataStore));
    }

    public IEnumerable<Frame> GetFrames(int gameId)
    {
        var game = bowlingDataStore.Games.FirstOrDefault(x => x.Id == gameId);
        return game.Frames;
    }

    public Frame GetFrame(int gameId, int frameId)
    {
        var game = bowlingDataStore.Games.FirstOrDefault(x => x.Id == gameId);
        return game.Frames.FirstOrDefault(x => x.Id == frameId);
    }

    public bool DoesFrameExist(int gameId, int frameId)
    {
        
        var game = bowlingDataStore.Games.FirstOrDefault(x => x.Id == gameId);
        return (game is null) ? false : game.Frames.Any(x => x.Id == frameId);
    }

    public Frame AddFrame(int gameId, int lastFrameId)
    {
        var game = bowlingDataStore.Games.FirstOrDefault(x => x.Id == gameId);
        var frame = new Frame(lastFrameId + 1);

        game.Frames.Add(frame);

        return frame;
    }

    public void DeleteFrame(int gameId, Frame frame)
    {   
        var game = bowlingDataStore.Games.FirstOrDefault(x => x.Id == gameId);
        game.Frames.Remove(frame);
    }
}