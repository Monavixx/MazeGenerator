using System.Diagnostics.CodeAnalysis;
using ConsoleGameFramework.Screens;
using ConsoleGameFramework.Viewport;
using MazeGenerator.Application;
using MazeGenerator.Presentation.MazeGeneration;

namespace MazeGenerator.Presentation.Settings;

public class SettingsScreen : Screen
{
    private readonly MazeGenerationController _mazeGenerationController;
    private readonly ConsoleGameFramework.Application _app;

    [SetsRequiredMembers]
    public SettingsScreen(IViewport viewport, MazeGenerationController mazeGenerationController, ConsoleGameFramework.Application app) : base(viewport)
    {
        _mazeGenerationController = mazeGenerationController;
        _app = app;
    }
    
    public override void Render()
    {
        Console.Clear();
        Console.WriteLine("Setting up Maze Properties");
        int width = ReadIntOfDefault("Maze width (default: 5): ", 5);
        int height = ReadIntOfDefault("Maze height (default: 5): ", 5);
        _mazeGenerationController.DelayInMs = ReadIntOfDefault("Step delay in ms (default: 500): ", 500);
        _mazeGenerationController.StartGeneration(width, height);
        _app.CurrentScreen<MazeGenerationScreen>();
    }

    private static int ReadIntOfDefault(string text, int defaultValue)
    {
        Console.Write(text);
        var input = Console.ReadLine() ?? "";
        return string.IsNullOrWhiteSpace(input) ? defaultValue : int.Parse(input);
    }
}