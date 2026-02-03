using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;

namespace MazeGenerator.Application.Commands;

public class RestartCommand (MazeGenerationController mazeGenerationController) : ICommand
{
    public InputHandleResult? Execute()
    {
        mazeGenerationController.RestartGeneration();
        return InputHandleResult.None();
    }
}