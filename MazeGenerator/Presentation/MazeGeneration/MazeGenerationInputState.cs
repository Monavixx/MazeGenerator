using ConsoleGameFramework.Input.InputStates;
using MazeGenerator.Application;
using MazeGenerator.Application.Commands;
using MazeGenerator.Presentation.MazeGeneration.Commands;

namespace MazeGenerator.Presentation.MazeGeneration;

public class MazeGenerationInputState : InputState
{
    public MazeGenerationInputState(MazeGenerationController mazeGenerationController)
    {
        RegisterCommand(ConsoleKey.Enter, ()=>new RestartCommand(mazeGenerationController));
        RegisterCommand(ConsoleKey.Z, ()=>new OpenSettingsCommand());
    }
}