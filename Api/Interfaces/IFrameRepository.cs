using BowlingGame.Models;

namespace BowlingGame.Interfaces;

public interface IFrameRepository
{
    IEnumerable<Frame> GetFrames(int gameId);
    Frame GetFrame(int gameId, int frameId);
    bool DoesFrameExist(int gameId, int frameId);
    Frame AddFrame(int gameId, int lastFrameId);
    void DeleteFrame(int gameId, Frame frame);
}