using MazeGenerator.Domain;

namespace MazeGenerator.Application;

public class MazeGenerationController
{
    private Maze? _currentMaze = null;
    public Maze CurrentMaze => _currentMaze ?? throw new InvalidOperationException();
    public IEnumerator<GenerationStep> GenerationSteps { get; private set; }
    private IMazeGenerator _generator;
    public event Action? OnNextStep;
    public event Action? OnGenerationDone;
    public int DelayInMs { get; set; } = 500;
    
    public MazeGenerationController(IMazeGenerator generator)
    {
        _generator = generator;
    }

    public void Generate(int width, int height)
    {
        _currentMaze = _generator.Generate(width, height);
    }

    public void StartGeneration(int width, int height)
    {
        _generator.StartGeneration(width, height);
        GenerationSteps = _generator.GetSteps().GetEnumerator();
        _currentMaze = _generator.Maze;
    }

    public bool NextStep()
    {
        if (GenerationSteps.MoveNext())
        {
            OnNextStep?.Invoke();
            return true;
        }
        else
        {
            OnGenerationDone?.Invoke();
            return false;
        }
    }

    private TimeSpan _totalTime;
    private TimeSpan _lastStepTime = TimeSpan.Zero;
    private bool _generationDone = false;
    public void Update(TimeSpan deltaTime)
    {
        if (_generationDone) return;
        
        _totalTime += deltaTime;
        if (_totalTime - _lastStepTime > TimeSpan.FromMilliseconds(DelayInMs))
        {
            _lastStepTime = _totalTime;
            if (NextStep()) return;
            _generationDone = true;
        }
    }
}