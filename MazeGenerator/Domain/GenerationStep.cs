namespace MazeGenerator.Domain;

public record GenerationStep (int X, int Y, (int x, int y)? UpdatedCellCoords);