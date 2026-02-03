using System.Diagnostics.CodeAnalysis;
using ConsoleGameFramework.Screens;
using ConsoleGameFramework.Viewport;
using MazeGenerator.Application;

namespace MazeGenerator.Presentation.MazeGeneration;

public class MazeGenerationScreen : Screen
{
    private readonly MazeGenerationController _mazeGenerationController;
    private readonly MazeGenerationRenderer _mazeGenerationRenderer;
    [SetsRequiredMembers]
    public MazeGenerationScreen(IViewport viewport, MazeGenerationController mazeGenerationController) : base(viewport)
    {
        _mazeGenerationController = mazeGenerationController;
        _mazeGenerationRenderer = new MazeGenerationRenderer(mazeGenerationController);
    }

    public override void Update(TimeSpan deltaTime)
    {
        base.Update(deltaTime); 
        _mazeGenerationController.Update(deltaTime);
    }

    public override void Render()
    {
        _mazeGenerationRenderer.RenderSteps();
    }
}