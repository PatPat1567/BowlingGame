using System;
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

        // Render GameDisplay component.
        var component = ctx.RenderComponent<GameDisplay>();

        // Assert
        Assert.Equal("Game Score: 0", component.Find($".game-score").TextContent.Replace("\n", "").Trim());
        Assert.Equal("Roll Ball", component.Find($".roll").TextContent);
        Assert.Equal("Restart Game", component.Find($".restart").TextContent);
        Assert.Equal("Simulate Game", component.Find($".simulate").TextContent);
    }

    [Fact]
    public void RollBallButtonClick()
    {
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<GameService>(new GameService());

        var component = ctx.RenderComponent<GameDisplay>();

        var RollValueException = Assert.Throws<Bunit.ElementNotFoundException>(() => component.Find($".roll-value"));

        
        var RollBallButton = component.Find($".roll");
        RollBallButton.Click();

        var RollValue = component.Find($".roll-value").TextContent;
        int.TryParse(RollValue, out int RollValueInt);
        Assert.InRange(RollValueInt, 0, 10);
    }

    [Fact]
    public void SimulateGameButtonClick()
    {
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<GameService>(new GameService());

        var component = ctx.RenderComponent<GameDisplay>();

        var gameScore = "0";
        Assert.Equal(gameScore, component.Find($".score").TextContent);

        var SimulateGameButton = component.Find($".simulate");
        SimulateGameButton.Click();
        gameScore = component.Find($".score").TextContent;
        int.TryParse(gameScore, out int gameScoreInt);
        Assert.InRange(gameScoreInt, 0, 300);
    }

    [Fact]
    public void RestartGameButtonClick()
    {
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<GameService>(new GameService());

        var component = ctx.RenderComponent<GameDisplay>();

        var gameScore = "0";
        Assert.Equal(gameScore, component.Find($".score").TextContent);

        var SimulateGameButton = component.Find($".simulate");
        SimulateGameButton.Click();
        gameScore = component.Find($".score").TextContent;
        int.TryParse(gameScore, out int gameScoreInt);
        Assert.InRange(gameScoreInt, 0, 300);

        var ResetGameButton = component.Find($".restart");
        ResetGameButton.Click();
        gameScore = "0";
        Assert.Equal(gameScore, component.Find($".score").TextContent);
    }

}