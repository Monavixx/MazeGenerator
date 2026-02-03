using ConsoleGameFramework;
using ConsoleGameFramework.Viewport;
using MazeGenerator.Application;
using MazeGenerator.Domain;
using MazeGenerator.Presentation.MazeGeneration;
using MazeGenerator.Presentation.Settings;

var mazeGenerationController = new MazeGenerationController(new CarvingMazeGenerator(null));

var app = new Application(new ConsoleViewport());
app.RegisterScreens(sf =>
    {
        sf.Register<MazeGenerationScreen>(v=>new MazeGenerationScreen(v, mazeGenerationController));
        sf.Register<SettingsScreen>(v=>new SettingsScreen(v, mazeGenerationController, app));
    })
    .CurrentScreen<SettingsScreen>()
    .Run();