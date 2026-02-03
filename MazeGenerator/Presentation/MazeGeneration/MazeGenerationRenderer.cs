using MazeGenerator.Application;
using MazeGenerator.Domain;

namespace MazeGenerator.Presentation.MazeGeneration;

public class MazeGenerationRenderer
{
    private bool _fullRenderRequired = true;
    private readonly MazeGenerationController _mazeGenerationController;
    private List<GenerationStep> _stepsToRender = new();
    private (int x, int y) _lastCursorPosition = (0, 0);
    private bool _cursorClearRequired = false;

    public MazeGenerationRenderer(MazeGenerationController mazeGenerationController)
    {
        _mazeGenerationController = mazeGenerationController;
        _mazeGenerationController.OnNextStep += HandleNextStep;
        _mazeGenerationController.OnGenerationDone += HandleGenerationDone;
    }

    private void HandleGenerationDone()
    {
        _cursorClearRequired = true;
    }

    private void HandleNextStep()
    {
        _stepsToRender.Add(_mazeGenerationController.GenerationSteps.Current);
    }

    private void ClearCache()
    {
        _fullRenderRequired = false;
        _cursorClearRequired = false;
    }

    public void RenderMaze()
    {
        if (_fullRenderRequired)
        {
            Console.Clear();
            RenderBorders();
            for (int i = 0; i < _mazeGenerationController.CurrentMaze.Width; i++)
            {
                for (int j = 0; j < _mazeGenerationController.CurrentMaze.Height; j++)
                {
                    RenderCell(i, j);
                }
            }
        }
        else
        {
            
        }
        
        ClearCache();
    }

    public void RenderSteps()
    {
        if (_fullRenderRequired)
        {
            Console.Clear();
            RenderFullMaze();
            RenderCursor(0, 0);
        }
        else
        {
            if (_cursorClearRequired)
            {
                ClearCursor(_lastCursorPosition.x, _lastCursorPosition.y);
            }
            foreach (var step in _stepsToRender)
            {
                if(step.UpdatedCellCoords is var (x, y))
                    RenderCell(x, y);
                ClearCursor(_lastCursorPosition.x, _lastCursorPosition.y);
                RenderCursor(step.X, step.Y);
                _lastCursorPosition = (step.X, step.Y);
            }
            _stepsToRender.Clear();
        }
        ClearCache();
        
    }

    private void RenderBorders()
    {
        int width = _mazeGenerationController.CurrentMaze.Width,
            height = _mazeGenerationController.CurrentMaze.Height;
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
        string horizontalBorder = '+' + Enumerable
            .Range(0, width)
            .Select(_ => "---+")
            .Aggregate((i, j) => i + j);
        Console.Write(horizontalBorder);
        Console.SetCursorPosition(0, height*2);
        Console.Write(horizontalBorder);
        
        
        
        Console.SetCursorPosition(4 * width, 1);
        Console.Write('|');
        for (int i = 2; i < height*2; i++)
        {
            var vertRenderSymbol = i % 2 == 0 ? '+' : '|';
            Console.SetCursorPosition(0, i);
            Console.Write(vertRenderSymbol);

            if (i != height * 2 - 1)
            {
                Console.SetCursorPosition(4 * width, i);
                Console.Write(vertRenderSymbol);
            }
        }
    }

    private void RenderFullMaze()
    {
        int width = _mazeGenerationController.CurrentMaze.Width,
            height = _mazeGenerationController.CurrentMaze.Height;
        Console.ResetColor();
        string horizontalBorder = '+' + Enumerable
            .Range(0, width)
            .Select(_ => "---+")
            .Aggregate((i, j) => i + j);
        for (int i = 0; i <= height * 2; i += 2)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(horizontalBorder);

            if (i != height * 2)
            {
                for (int j = 0; j <= width*4; j += 4)
                {
                    Console.SetCursorPosition(j, i + 1);
                    Console.Write('|');
                }
            }
        }
        Console.SetCursorPosition(0, 1);
        Console.Write(' ');
        Console.SetCursorPosition(width*4, height*2-1);
        Console.Write(' ');
    }

    private void RenderCell(int x, int y)
    {
        Console.ResetColor();
        
        var cell = _mazeGenerationController.CurrentMaze[x, y];
        Console.SetCursorPosition(1 + 4 * x, (y + 1) * 2);
        Console.Write(cell.BottomWall ? "---+" : "   ");

        Console.SetCursorPosition(4 * (x+1), (y + 1) * 2 - 1);
        Console.Write(cell.RightWall ? '|' : ' ');
    }

    private void RenderCursor(int x, int y)
    {
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(x*4+1, y*2+1);
        Console.Write("   ");
        Console.ResetColor();
    }

    private void ClearCursor(int x, int y)
    {
        Console.ResetColor();
        Console.SetCursorPosition(x*4+1, y*2+1);
        Console.Write("   ");
    }
}