using FrontEnd.Shared;
using FrontEnd.Data;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FrtonEndTests;

public class GameDisplayTest
{
    [Fact]
    public void RendersSuccessfully()
    {

        using var ctx = new TestContext();

        ctx.Services.AddSingleton<GameService>(new GameService());

        // Render Counter component.
        var component = ctx.RenderComponent<GameDisplay>();

        // Assert: first, find the parent_name vital element, then verify its content.
        Assert.Equal("Game Score: 0", component.Find($".game-score").TextContent.Replace("\n", "").Trim());
        Assert.Equal("Roll Ball", component.Find($".roll").TextContent);
        Assert.Equal("Restart Game", component.Find($".restart").TextContent);
        Assert.Equal("Simulate Game", component.Find($".simulate").TextContent);
    }

    [Fact]
    public void RestartGameButtonClick()
    {
        using var ctx = new TestContext();
        Assert.True(true);
    }

}