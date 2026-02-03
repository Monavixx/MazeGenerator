namespace MazeGenerator.Domain;

public interface IMazeGenerator
{
    Maze Generate(int width, int height);
    void StartGeneration(int width, int height);
    IEnumerable<GenerationStep> GetSteps();
    Maze? Maze { get; }
}