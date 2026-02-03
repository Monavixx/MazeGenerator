using ConsoleGameFramework;
using ConsoleGameFramework.Viewport;
using MazeGenerator.Application;
using MazeGenerator.Domain;
using MazeGenerator.Presentation.MazeGeneration;

var mazeGenerationController = new MazeGenerationController(new CarvingMazeGenerator(null));

Console.Clear();
Console.WriteLine("Setting up Maze Properties");
Console.WriteLine("Maze width (default: 5): ");
int width = int.Parse(NullIfWhiteSpace(Console.ReadLine()) ?? "5");
Console.WriteLine("Maze height (default: 5): ");
int height = int.Parse(NullIfWhiteSpace(Console.ReadLine()) ?? "5");

mazeGenerationController.StartGeneration(width, height);

Console.WriteLine("Step delay in ms (default: 500): ");
mazeGenerationController.DelayInMs = int.Parse(NullIfWhiteSpace(Console.ReadLine()) ?? "500");

new Application(new ConsoleViewport())
    .RegisterScreens(sf =>
    {
        sf.Register<MazeGenerationScreen>(v=>new MazeGenerationScreen(v, mazeGenerationController));
    })
    .CurrentScreen<MazeGenerationScreen>()
    .Run();

string? NullIfWhiteSpace(string? str)
{
    if (string.IsNullOrWhiteSpace(str)) return null;
    return str;
}