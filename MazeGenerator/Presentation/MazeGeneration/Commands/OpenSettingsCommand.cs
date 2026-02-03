using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;
using MazeGenerator.Presentation.Settings;

namespace MazeGenerator.Presentation.MazeGeneration.Commands;

public class OpenSettingsCommand : ICommand
{
    public InputHandleResult? Execute()
    {
        return InputHandleResult.NavigateTo<SettingsScreen>();
    }
}